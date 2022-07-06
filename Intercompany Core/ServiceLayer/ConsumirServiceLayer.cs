using B1SLayer;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class ConsumirServiceLayer
    {

        public async Task<string> CreateBusinessPartnerAsync(SLConnection serviceLayer, SocioNegocios socio)
        {

            string response = "";
            BusinessPartner bp = new BusinessPartner();
            bp.CardCode = socio.CodSocioNegocios;
            bp.CardName = socio.Nombre;
            bp.GroupCode = socio.Grupo;
            // Cambiar el resultado de CardType por su traduccion en ingles C: Customer = Cliente, S: Supplier = Poveedor
            switch (socio.TipoSN)
            {
                case "Cliente":
                    bp.CardType = 'C';
                    break;
                case "Proveedor":
                    bp.CardType = 'S';
                    break;
            };
            //Cambiar el resultado de CmpPrivate por su traduccion en ingles C: Sociedades, I: Privado
            switch (socio.TipoPersona)
            {
                case "Moral":
                    bp.CompanyPrivate = 'C';
                    break;
                case "Fisica":
                    bp.CompanyPrivate = 'I';
                    break;
            };
            bp.FederalTaxID = socio.RFC;
            bp.Currency = socio.Moneda;
            bp.ValidTo = socio.FechaHasta;
            await serviceLayer.Request("BusinessPartners").PostAsync<BusinessPartner>(bp);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;
            });
            return response;
        }
        public async Task<string> PatchBusinessPartnerAsync(SLConnection serviceLayer, SocioNegocios socio)
        {
            string response = "";
            BusinessPartner bp = new BusinessPartner();
            bp.CardCode = socio.CodSocioNegocios;
            bp.CardName = socio.Nombre;
            bp.GroupCode = socio.Grupo;
            // Cambiar el resultado de CardType por su traduccion en ingles C: Customer = Cliente, S: Supplier = Poveedor
            switch (socio.TipoSN)
            {
                case "Cliente":
                    bp.CardType = 'C';
                    break;
                case "Proveedor":
                    bp.CardType = 'S';
                    break;
            };
            //Cambiar el resultado de CmpPrivate por su traduccion en ingles C: Sociedades, I: Privado
            switch (socio.TipoPersona)
            {
                case "Moral":
                    bp.CompanyPrivate = 'C';
                    break;
                case "Fisica":
                    bp.CompanyPrivate = 'I';
                    break;
            };
            bp.FederalTaxID = socio.RFC;
            bp.Currency = socio.Moneda;
            //await serviceLayer.Request("BusinessPartners").PostAsync<BusinessPartner>(bp);
            await serviceLayer.Request("BusinessPartners", bp.CardCode).PatchAsync(bp);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;
            });
            return response;
        }
        public async Task<string> CreateItemsAsync(SLConnection serviceLayer, Items item)
        {
            string response = "";
            Items_SL it = new Items_SL();
            it.ItemCode = item.CodItems;
            it.ItemName = item.Nombre;
            it.ItemType = "I";
            it.ItemsGroupCode = item.Grupo;
            it.InventoryItem = item.EsInventario;
            it.SalesItem = item.EsVentas;
            it.PurchaseItem = item.EsCompras;
            // Cambiar el resultado de GLMethod por su traduccion en ingles W: Warehouse = A, C: Item Group = G, L: Item Level=N  
            switch (item.MetodoInv)
            {
                case 'A':
                    it.GLMethod = 'W';
                    break;
                case 'G':
                    it.GLMethod = 'C';
                    break;
                case 'N':
                    it.GLMethod = 'L';
                    break;
            };
            // Cambiar el resultado de CostAccountingMethod por su traduccion en ingles A:  Moving Average = P, S:  S) Standard = C  
            switch (item.MetodoCosto)
            {
                case 'P':
                    it.CostAccountingMethod = 'A';
                    break;
                case 'C':
                    it.CostAccountingMethod = 'S';
                    break;
            }
            it.AvgStdPrice = item.Costo;
            it.Valid = item.EsValido;
            it.ValidTo = item.FechaHasta;
            it.BarCode = item.CodBarras;
            if (item.GesItems == "Ninguno")
            {
                it.ManageBatchNumbers = 'N';
                it.ManageSerialNumbers = 'N';
            }
            else if (item.GesItems == "Números de serie")
            {
                it.ManageBatchNumbers = 'N';
                it.ManageSerialNumbers = 'Y';
            }
            else 
            {
                it.ManageBatchNumbers = 'Y';
                it.ManageSerialNumbers = 'N';
            }
            await serviceLayer.Request("Items").PostAsync<Items_SL>(it);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;

            });
            return response;
        }
        public async Task<string> PatchItemsAsync(SLConnection serviceLayer, Items item)
        {
            string response = "";
            Items_SL it = new Items_SL();
            it.ItemCode = item.CodItems;
            it.ItemName = item.Nombre;
            it.ItemType = "I";
            it.ItemsGroupCode = item.Grupo;
            it.InventoryItem = item.EsInventario;
            it.SalesItem = item.EsVentas;
            it.PurchaseItem = item.EsCompras;
            // Cambiar el resultado de GLMethod por su traduccion en ingles W: Warehouse = A, C: Item Group = G, L: Item Level=N  
            switch (item.MetodoInv)
            {
                case 'A':
                    it.GLMethod = 'W';
                    break;
                case 'G':
                    it.GLMethod = 'C';
                    break;
                case 'N':
                    it.GLMethod = 'L';
                    break;
            };
            // Cambiar el resultado de CostAccountingMethod por su traduccion en ingles A:  Moving Average = P, S:  S) Standard = C  
            switch (item.MetodoCosto)
            {
                case 'P':
                    it.CostAccountingMethod = 'A';
                    break;
                case 'C':
                    it.CostAccountingMethod = 'S';
                    break;
            };
            it.AvgStdPrice = item.Costo;
            it.Valid = item.EsValido;
            it.ValidTo = item.FechaHasta;
            it.BarCode = item.CodBarras;
            if (item.GesItems == "Ninguno")
            {
                it.ManageBatchNumbers = 'N';
                it.ManageSerialNumbers = 'N';
            }
            else if (item.GesItems == "Números de serie")
            {
                it.ManageBatchNumbers = 'N';
                it.ManageSerialNumbers = 'Y';
            }
            else
            {
                it.ManageBatchNumbers = 'Y';
                it.ManageSerialNumbers = 'N';
            }
            await serviceLayer.Request("Items", it.ItemCode).PatchAsync(it);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;

            });
            return response;
        }
        public async Task<string> CreateAccountsAsync(SLConnection serviceLayer, Cuentas cuenta)
        {
            string response = "";
            ChartOfAccounts cu = new ChartOfAccounts();
            cu.Code = cuenta.CodCuenta;
            cu.Name = cuenta.Nombre;
            switch (cuenta.Tipo)
            {
                case 'C':
                    cu.ActiveAccount = 'Y';
                    break;
                case 'T':
                    cu.ActiveAccount = 'N';
                    break;
            };
            cu.AcctCurrency = cuenta.Moneda;
            cu.CashAccount = cuenta.RPresupuesto;
            switch (cuenta.Naturaleza)
            {
                case "Deudor":
                    cu.U_natur = 'D';
                    break;
                case "Acreedor":
                    cu.U_natur = 'A';
                    break;
            };
            cu.U_CodAgrup = cuenta.CodigoSAT;
            cu.U_CodAgrup_Nivel = cuenta.NivelSAT;
            cu.U_DescSAT = cuenta.DescSAT;
            cu.U_CuentaOrden = cuenta.CuentaOrden;
            cu.U_Nivel = cuenta.Nivel;
            cu.FatherAccountKey = cuenta.FatherAccnt;
            var loginConexion = await serviceLayer.Request("ChartOfAccounts").PostAsync<ChartOfAccounts>(cu);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;

            });
            return response;
        }
        public async Task<string> PatchAccountsAsync(SLConnection serviceLayer, Cuentas cuenta)
        {
            string response = "";
            ChartOfAccounts cu = new ChartOfAccounts();
            cu.Code = cuenta.CodCuenta;
            cu.Name = cuenta.Nombre;
            switch (cuenta.Tipo)
            {
                case 'C':
                    cu.ActiveAccount = 'Y';
                    break;
                case 'T':
                    cu.ActiveAccount = 'N';
                    break;
            };
            cu.AcctCurrency = cuenta.Moneda;
            cu.CashAccount = cuenta.RPresupuesto;
            switch (cuenta.Naturaleza)
            {
                case "Deudor":
                    cu.U_natur = 'D';
                    break;
                case "Acreedor":
                    cu.U_natur = 'A';
                    break;
            };
            cu.U_CodAgrup = cuenta.CodigoSAT;
            cu.U_CodAgrup_Nivel = cuenta.NivelSAT;
            cu.U_DescSAT = cuenta.DescSAT;
            cu.U_CuentaOrden = cuenta.CuentaOrden;
            cu.U_Nivel = cuenta.Nivel;
            cu.FatherAccountKey = cuenta.FatherAccnt;
            await serviceLayer.Request("ChartOfAccounts", cu.Code).PatchAsync(cu);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;

            });
            return response;
        }
        public async Task<string> CreateCostCenterAsync(SLConnection serviceLayer, CentrosCosto centro)
        {
            string response = "";
            ProfitCenters cc = new ProfitCenters();
            cc.CenterCode = centro.CodCentrosCosto;
            cc.CenterName = centro.Nombre;
            cc.InWhichDimension = centro.Dimension;
            cc.Active = centro.EsValido;
            cc.EffectiveFrom = DateTime.Now;
            cc.EffectiveTo = centro.FechaHasta;
            var loginConexion = await serviceLayer.Request("ProfitCenters").PostAsync<ProfitCenters>(cc);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;

            });
            return response;
        }
        public async Task<string> PatchCostCenterAsync(SLConnection serviceLayer, CentrosCosto centro)
        {
            string response = "";
            ProfitCenters cc = new ProfitCenters();
            cc.CenterCode = centro.CodCentrosCosto;
            cc.CenterName = centro.Nombre;
            cc.InWhichDimension = centro.Dimension;
            cc.Active = centro.EsValido;
            cc.EffectiveFrom = DateTime.Now;
            cc.EffectiveTo = centro.FechaHasta;
            await serviceLayer.Request("ProfitCenters", cc.CenterCode).PatchAsync(cc);
            serviceLayer.AfterCall(async call =>
            {
                response = call.RequestBody;

            });
            return response;
        }
      
    }
}
