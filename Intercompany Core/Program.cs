using IntercompanyCore;
using Microsoft.AspNetCore.Builder;
using B1SLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using IntercompanyAPI.Controllers;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using IntercompanyCore.Entities;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using System.Net.Http.Headers;
using IntercompanyCore.Documents;
using IntercompanyCore.ServiceLayer;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IntercompanyCore
{
    public class Program
    {

        private static readonly ILog log = Logs.GetLogger();
        private static HttpClient client = new HttpClient();
        private static HttpClient clientDoc;
        


        [STAThread]
        static async Task Main(string[] args)
        {
            ConexionObj Conexion = null;
            FileStream openStream = null;
            FileStream openStreamCuentas = null;
            Account account = null;
            string direccionConexion = "";
            string direccionCuentas = "";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            clientDoc = new HttpClient(clientHandler);
            direccionConexion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuracion\\Conexion.json");
            openStream = File.OpenRead(direccionConexion);
            Conexion = await JsonSerializer.DeserializeAsync<ConexionObj>(openStream);


            client.BaseAddress = new Uri(Conexion.IP);
            clientDoc.BaseAddress = new Uri(Conexion.IP);
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            client.DefaultRequestHeaders.Accept.Add(mediaType);

            #region TransaccionesServiceLayer
            // Traer datos desincronizados de Transaccion 
            var streamTask = client.GetStreamAsync(Conexion.IP + "/Transaccion/ObtenerTransaccionesNS");
            var Transacciones = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Transaccion>>(await streamTask);
            var transacciones = Transacciones;

            // Traer SN, CU, CC e IT de la base de datos concentradora
            var CuentasTask = client.GetStreamAsync(Conexion.IP + "/Cuentas/ObtenerTransaccionesCU");
            var Cuentas = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Cuentas>>(await CuentasTask);

            var SociosTask = client.GetStreamAsync(Conexion.IP + "/SocioNegocios/ObtenerTransaccionesSN");
            var Socios = await System.Text.Json.JsonSerializer.DeserializeAsync<List<SocioNegocios>>(await SociosTask);

            var ItemsTask = client.GetStreamAsync(Conexion.IP + "/Items/ObtenerTransaccionesIT");
            var Items = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Items>>(await ItemsTask);

            var CentrosTask = client.GetStreamAsync(Conexion.IP + "/CentrosCosto/ObtenerTransaccionesCC");
            var Centros = await System.Text.Json.JsonSerializer.DeserializeAsync<List<CentrosCosto>>(await CentrosTask);

            // Crear contexto para usar SL 
            ConsumirServiceLayer csl = new ConsumirServiceLayer();
            GenerarConexion gc = new GenerarConexion();
            Transaccion transaccion = new Transaccion();

            for (int i = 0; i < transacciones.Count; i++)
            {
                try
                {
                    // Generar conexion 
                    SLConnection serviceLayer = await gc.ConexionSLayerAsync(transacciones[i].IdDestino);
                    switch (transacciones[i].TipodCRUD)
                    {
                        case "SN":
                            SocioNegocios socio = Socios.Find(s => s.Clave == transacciones[i].IdObjeto);
                            if (transacciones[i].TipoOperacion == 'A')
                            {
                                string body1 = await csl.CreateBusinessPartnerAsync(serviceLayer, socio);
                            }
                            else
                            {
                                string body2 = await csl.PatchBusinessPartnerAsync(serviceLayer, socio);
                            }
                            break;
                        case "CC":
                            CentrosCosto centro = Centros.Find(c => c.Clave == transacciones[i].IdObjeto);
                            if (transacciones[i].TipoOperacion == 'A')
                            {
                                string body3 = await csl.CreateCostCenterAsync(serviceLayer, centro);
                            }
                            else
                            {
                                string body4 = await csl.PatchCostCenterAsync(serviceLayer, centro);
                            }
                            break;
                        case "CU":
                            Cuentas cuenta = Cuentas.Find(s => s.Clave == transacciones[i].IdObjeto);
                            if (transacciones[i].TipoOperacion == 'A')
                            {
                                string body5 = await csl.CreateAccountsAsync(serviceLayer, cuenta);
                            }
                            else
                            {
                                string body6 = await csl.PatchAccountsAsync(serviceLayer, cuenta);
                            }
                            break;
                        case "IT":
                            Items item = Items.Find(s => s.Clave == transacciones[i].IdObjeto);
                            if (transacciones[i].TipoOperacion == 'A')
                            {
                                var body7 = await csl.CreateItemsAsync(serviceLayer, item);
                            }
                            else
                            {
                                string body8 = await csl.PatchItemsAsync(serviceLayer, item);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    log.Error(ex);
                }
                finally
                {
                    transaccion = transacciones[i];
                    transaccion.Sincronizado = 'Y';
                    transaccion.FechaSincronizacion = DateTime.Now;
                    transaccion.IdOrigen = transaccion.IdDestino;
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(transaccion);
                    transaccion.JSON = jsonString;
                    var result = await client.PutAsync($"{client.BaseAddress}/Transaccion/EditarTransaccion/{transacciones[i].Id}", new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(transaccion), Encoding.UTF8, "application/json"));

                }
            }


            #endregion

            #region DocumentosCompraVenta
            GenerarConexion gc2 = new GenerarConexion();
            /*
             * Orden de venta a orden de compra
             */
            for (int iconexion = 1; iconexion < 10; iconexion++)
            {
                try
                {
                    Transaccion t = new Transaccion();
                    Transaccion t1 = new Transaccion();
                    SLConnection serviceLayer1 = await gc2.ConexionSLayerAsync(iconexion);
                    var createdOrders = await serviceLayer1.Request("Orders").Select("*").GetAllAsync<OV>();
                    // Si existen ordenes de ventas que no esten sincronizadas y apliquen para Intercompany
                    if (createdOrders.Count > 0)
                    {
                        var orders = createdOrders.Where(CO => CO.U_KAI01_Intercompany == 'Y' && CO.U_KAI01_Sincronizado == 'N');
                        foreach (var order in orders)
                        {
                            t.TipoTransaccion = 2;
                            t.TipodCRUD = "NA";
                            t.IdOrigen = iconexion;
                            t.IdDestino = Convert.ToInt32(order.U_KAI01_EmpresaDestino);
                            t.Sincronizado = 'Y';
                            t.ErrorDesc = " ";
                            t.IdObjeto = order.DocEntry;
                            t.TipoOperacion = 'D';
                            t.TipoDocumento = "OV";
                            var jsonString = System.Text.Json.JsonSerializer.Serialize(t);
                            t.JSON = jsonString;

                            // Se crea la transaccion en la base de datos concentradora
                            var result = await clientDoc.PostAsync($"{clientDoc.BaseAddress}/Transaccion/CrearTransaccion", new StringContent(System.Text.Json.JsonSerializer.Serialize(t), Encoding.UTF8, "application/json"));
                            // Se edita la orden de venta indicandole que ya esta sincronizada
                            await serviceLayer1.Request("Orders", order.DocEntry).PatchAsync(new { U_KAI01_Sincronizado = 'Y' });
                            // Se desconecta del servidor
                            await serviceLayer1.Request("Logout").PostAsync();

                            // Se conecta a la base de datos de la empresa destino
                            serviceLayer1 = await gc2.ConexionSLayerAsync(Convert.ToInt32(order.U_KAI01_EmpresaDestino));
                            string c = "0" + Convert.ToString(iconexion);
                            OC oc = new OC(order.U_KAI01_SN, order.DocDate, order.DocDueDate, order.U_KAI01_Intercompany, 'Y', c, order.CardCode);
                            oc.U_KAI01_Referencia = order.DocEntry;
                            oc.DocumentLines = order.DocumentLines;
                            for (int i = 0; i < oc.DocumentLines.Count; i++)
                            {
                                switch (oc.DocumentLines[i].TaxCode)
                                {
                                    case "C0":
                                        oc.DocumentLines[i].TaxCode = "P0";
                                        break;
                                    case "C16":
                                        oc.DocumentLines[i].TaxCode = "P16";
                                        break;
                                    case "CE":
                                        oc.DocumentLines[i].TaxCode = "PE";
                                        break;
                                    case "S1C16":
                                        oc.DocumentLines[i].TaxCode = "I1P16";
                                        break;
                                    case "IVAA16":
                                        oc.DocumentLines[i].TaxCode = "IVAP16";
                                        break;
                                    case "IVAV16":
                                        oc.DocumentLines[i].TaxCode = "IVAC16";
                                        break;
                                    default:
                                        oc.DocumentLines[i].TaxCode = "P0";
                                        break;
                                }
                            }
                            // Crea la orden de compra
                            var response = await serviceLayer1.Request("PurchaseOrders").PostAsync<OC>(oc);

                            // Se crea la transaccion en la base de datos concentradora pero con los datos de la orden de compra
                            t1.TipoTransaccion = 2;
                            t1.TipodCRUD = "NA";
                            t1.IdOrigen = Convert.ToInt32(order.U_KAI01_EmpresaDestino);
                            t1.IdDestino = iconexion;
                            t1.Sincronizado = 'Y';
                            t1.ErrorDesc = " ";
                            t1.IdObjeto = order.DocEntry;
                            t1.TipoOperacion = 'D';
                            t1.TipoDocumento = "OC";
                            var jsonString1 = System.Text.Json.JsonSerializer.Serialize(t1);
                            t1.JSON = jsonString1;

                            await clientDoc.PostAsync($"{clientDoc.BaseAddress}/Transaccion/CrearTransaccion", new StringContent(System.Text.Json.JsonSerializer.Serialize(t1), Encoding.UTF8, "application/json"));
                            await serviceLayer1.Request("Logout").PostAsync();
                        }
                    }
                    else
                    {
                        await serviceLayer1.Request("Logout").PostAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }

                /*
                 * Factura de cliente a factura de proveedor
                 */
                try
                {
                    Transaccion t = new Transaccion();
                    Transaccion t1 = new Transaccion();
                    SLConnection serviceLayer1 = await gc2.ConexionSLayerAsync(iconexion);
                    var supplierInvoices = await serviceLayer1.Request("Invoices").Select("*").Filter("U_KAI01_Intercompany eq 'Y'").GetAllAsync<FC>();
                    

                    // Si existen facturas de clientes que no esten sincronizadas y apliquen para Intercompany
                    if (supplierInvoices.Count > 0)
                    {
                        var invoices = supplierInvoices.Where(SI => SI.U_KAI01_Intercompany == 'Y' && SI.U_KAI01_Sincronizado == 'N');
                        foreach (var invoice in invoices)
                        {
                            t.TipoTransaccion = 2;
                            t.TipodCRUD = "NA";
                            t.IdOrigen = iconexion;
                            t.IdDestino = Convert.ToInt32(invoice.U_KAI01_EmpresaDestino);
                            t.Sincronizado = 'Y';
                            t.ErrorDesc = " ";
                            t.IdObjeto = invoice.DocEntry;  
                            t.TipoOperacion = 'D';
                            t.TipoDocumento = "FC";
                            var jsonString = System.Text.Json.JsonSerializer.Serialize(t);
                            t.JSON = jsonString;


                            // Se crea la transaccion en la base de datos concentradora
                            var result = await clientDoc.PostAsync($"{clientDoc.BaseAddress}/Transaccion/CrearTransaccion", new StringContent(System.Text.Json.JsonSerializer.Serialize(t), Encoding.UTF8, "application/json"));
                            // Se edita la factura del cliente indicandole que ya esta sincronizada
                            await serviceLayer1.Request("Invoices", invoice.DocEntry).PatchAsync(new { U_KAI01_Sincronizado = 'Y' });
                            // Se desconecta del servidor
                            await serviceLayer1.Request("Logout").PostAsync();

                            // Se conecta a la base de datos de la empresa destino
                            serviceLayer1 = await gc2.ConexionSLayerAsync(Convert.ToInt32(invoice.U_KAI01_EmpresaDestino));
                            string c = "0" + Convert.ToString(iconexion);
                            FP fp = new FP(invoice.U_KAI01_SN, invoice.DocDate, invoice.DocDueDate, invoice.U_KAI01_Intercompany, 'Y', c, invoice.CardCode, invoice.DocEntry);

                            fp.DocumentLines = invoice.DocumentLines;
                            for (int i = 0; i < fp.DocumentLines.Count; i++)
                            {
                                switch (fp.DocumentLines[i].TaxCode)
                                {
                                    case "C0":
                                        fp.DocumentLines[i].TaxCode = "P0";
                                        break;
                                    case "C16":
                                        fp.DocumentLines[i].TaxCode = "P16";
                                        break;
                                    case "CE":
                                        fp.DocumentLines[i].TaxCode = "PE";
                                        break;
                                    case "S1C16":
                                        fp.DocumentLines[i].TaxCode = "I1P16";
                                        break;
                                    case "IVAA16":
                                        fp.DocumentLines[i].TaxCode = "IVAP16";
                                        break;
                                    case "IVAV16":
                                        fp.DocumentLines[i].TaxCode = "IVAC16";
                                        break;
                                    default:
                                        fp.DocumentLines[i].TaxCode = "P0";
                                        break;
                                }
                                if (fp.DocumentLines[i].BaseType == "17")
                                {
                                    fp.DocumentLines[i].BaseType = "22";
                                    // Hacer la busqueda de la orden de compra que tenga U_KAI01_Referencia == DocEntry, de la orden de venta
                                    string referencia = fp.DocumentLines[i].BaseEntry;
                                    string filter = "U_KAI01_Referencia eq " + referencia ;
                                    var purchaseOrders = await serviceLayer1.Request("PurchaseOrders").Select("*").Filter(filter).GetAllAsync<OC>();
                                    var oc = purchaseOrders.FirstOrDefault(purchaseorder => purchaseorder.U_KAI01_Referencia == Convert.ToInt32(referencia));
                                    // Cambiar el BaseEntry por el cual estara relacionado con la orden de compra usando su DocEntry de esta base
                                    fp.DocumentLines[i].BaseEntry = Convert.ToString(oc.DocEntry);
                                }

                            }

                            // Crea la factura de proveedor
                            var response = await serviceLayer1.Request("PurchaseInvoices").PostAsync<FP>(fp);

                            // Se crea la transaccion en la base de datos concentradora pero con los datos de la factura de proveedor
                            t1.TipoTransaccion = 2;
                            t1.TipodCRUD = "NA";
                            t1.IdOrigen = Convert.ToInt32(invoice.U_KAI01_EmpresaDestino);
                            t1.IdDestino = iconexion;
                            t1.Sincronizado = 'Y';
                            t1.ErrorDesc = " ";
                            t1.IdObjeto = invoice.DocEntry;
                            t1.TipoOperacion = 'D';
                            t1.TipoDocumento = "FP";
                            var jsonString1 = System.Text.Json.JsonSerializer.Serialize(t1);
                            t1.JSON = jsonString1;

                            // Se crea la transaccion en la base de datos concentradora
                            await clientDoc.PostAsync($"{clientDoc.BaseAddress}/Transaccion/CrearTransaccion", new StringContent(System.Text.Json.JsonSerializer.Serialize(t1), Encoding.UTF8, "application/json"));
                            await serviceLayer1.Request("Logout").PostAsync();
                        }
                    }
                    else
                    {
                        await serviceLayer1.Request("Logout").PostAsync();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                /*
                 * Pago recibido a pago efectuado
                 */
                try
                {
                    Transaccion t = new Transaccion();
                    Transaccion t1 = new Transaccion();
                    SLConnection serviceLayer1 = await gc2.ConexionSLayerAsync(iconexion);
                    var incomingPayments = await serviceLayer1.Request("IncomingPayments").Select("*").Filter("U_KAI01_Intercompany eq 'Y'").GetAllAsync<PR>();

                    // Si existen pagos recibidos que no esten sincronizados y apliquen para Intercompany
                    if (incomingPayments.Count > 0)
                    {
                        var pagosRecibidos = incomingPayments.Where(IPay => IPay.U_KAI01_Intercompany == 'Y' && IPay.U_KAI01_Sincronizado == 'N');
                        foreach (var pago in pagosRecibidos)
                        {
                            t.TipoTransaccion = 2;
                            t.TipodCRUD = "NA";
                            t.IdOrigen = iconexion;
                            t.IdDestino = Convert.ToInt32(pago.U_KAI01_EmpresaDestino);
                            t.Sincronizado = 'Y';
                            t.ErrorDesc = "";
                            t.IdObjeto = pago.DocEntry;
                            t.TipoOperacion = 'D';
                            t.TipoDocumento = "PR";
                            var jsonString = System.Text.Json.JsonSerializer.Serialize(t);
                            t.JSON = jsonString;

                            // Se crea la transaccion en la base de datos concentradora
                            var result = await clientDoc.PostAsync($"{clientDoc.BaseAddress}/Transaccion/CrearTransaccion", new StringContent(System.Text.Json.JsonSerializer.Serialize(t), Encoding.UTF8, "application/json"));
                            // Se edita  el pago recibido indicandole que ya esta sincronizada
                            await serviceLayer1.Request("IncomingPayments", pago.DocEntry).PatchAsync(new { U_KAI01_Sincronizado = 'Y' });
                            // Se desconecta del servidor
                            await serviceLayer1.Request("Logout").PostAsync();

                            // Se conecta a la base de datos de la empresa destino
                            serviceLayer1 = await gc2.ConexionSLayerAsync(Convert.ToInt32(pago.U_KAI01_EmpresaDestino));
                            string c = "0" + Convert.ToString(iconexion);
                            PE pe = new PE(pago.U_KAI01_SN, pago.DocDate, pago.U_KAI01_Intercompany, 'Y', c, pago.CardCode, pago.TransferAccount, pago.TransferSum, pago.CheckAccount, pago.TransferReference);
                            Console.WriteLine(pago.TransferReference);
                            // El objeto PaymentInvoices tiene la informacion del pago recibido y efectuado
                            pe.PaymentInvoices = pago.PaymentInvoices;
                            pe.PaymentChecks = pago.PaymentChecks;
                            pe.PaymentCreditCards = pago.PaymentCreditCards;

                            // Se hace una consulta para preguntar por las facturas de proveedor
                            var purchaseInvoices = await serviceLayer1.Request("PurchaseInvoices").Select("*").Filter("U_KAI01_Intercompany eq 'Y'").GetAllAsync<FP>();

                            // Se filtran las facturas de proveedor que tengan el CardCode del SN, esten en Estatus abiertas y que provengan de la empresa destino 
                            var purchase = purchaseInvoices.Where(PU => PU.CardCode == pago.U_KAI01_SN);


                            // Sustituye el DocEntry en cada linea del documento de pago por el de las facturas de proveedor
                            for (int i = 0; i < pago.PaymentInvoices.Count; i++)
                            {
                                var p = purchase.FirstOrDefault(PU => PU.U_KAI01_Referencia == pago.PaymentInvoices[i].DocEntry);
                                pe.PaymentInvoices[i].DocEntry = p.DocEntry;
                            }
                            // Cambia la cuenta donde se hizo la transferencia
                            switch (Convert.ToInt32(pago.U_KAI01_EmpresaDestino))
                            {
                                case 1:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E1.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 2:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E2.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 3:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E3.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 4:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E4.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 5:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E5.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 6:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E6.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 7:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E7.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 8:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E8.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                case 9:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E9.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                                default:
                                    direccionCuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Accounts\\E1.json");
                                    openStreamCuentas = File.OpenRead(direccionCuentas);
                                    account = await JsonSerializer.DeserializeAsync<Account>(openStreamCuentas);
                                    pe.TransferAccount = account.Cuenta;
                                    break;
                            }


                            // Crea el pago efectuado
                            var response = await serviceLayer1.Request("VendorPayments").PostAsync<PE>(pe);

                            // Se crea la transaccion en la base de datos concentradora pero con los datos del pago efectuado
                            t1.TipoTransaccion = 2;
                            t1.TipodCRUD = "NA";
                            t1.IdOrigen = Convert.ToInt32(pago.U_KAI01_EmpresaDestino);
                            t1.IdDestino = iconexion;
                            t1.Sincronizado = 'Y';
                            t1.ErrorDesc = "";
                            t1.IdObjeto = pago.DocEntry;
                            t1.TipoOperacion = 'D';
                            t1.TipoDocumento = "PE";
                            var jsonString1 = System.Text.Json.JsonSerializer.Serialize(t1);
                            t1.JSON = jsonString1;

                            await clientDoc.PostAsync($"{clientDoc.BaseAddress}/Transaccion/CrearTransaccion", new StringContent(System.Text.Json.JsonSerializer.Serialize(t1), Encoding.UTF8, "application/json"));
                            await serviceLayer1.Request("Logout").PostAsync();
                        }
                    }
                    else
                    {
                        await serviceLayer1.Request("Logout").PostAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


            }
            #endregion
        }
    }
}
