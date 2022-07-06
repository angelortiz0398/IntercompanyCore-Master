using B1SLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class GenerarConexion
    {
        public async Task<SLConnection> ConexionSLayerAsync(int idDestino)
        {
            SLConnection serviceLayer = null;
            LoginObj login = null;
            FileStream openStream = null;
            string direccionBases = "";
            string direccion = "";
            switch (idDestino)
            {
                case 1:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\EFD-Mexico.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\EFD-Mexico.json");
                    break;
                case 2:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\Talleres-Cinematograficos.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\Talleres-Cinematograficos.json");
                    break;
                case 3:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\EFD-Technology.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\EFD-Technology.json");
                    break;
                case 4:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\EFD-Digital.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\EFD-Digital.json");
                    break;
                case 5:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\EFD-Cine.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\EFD-Cine.json");
                    break;
                case 6:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\EFD-International.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\EFD-International.json");
                    break;
                case 7:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\Servicios-Especializados.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\Servicios-Especializados.json");
                    break;
                case 8:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\DEMO_EFD.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\DEMO_EFD.json");
                    break;
                case 9:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\TESTING_FUSION.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\TESTING_FUSION.json");
                    break;
                default:
                    direccionBases = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login-JSON\\DEMO_EFD.json");
                    openStream = File.OpenRead(direccionBases);
                    login = await JsonSerializer.DeserializeAsync<LoginObj>(openStream);
                    serviceLayer = new SLConnection(login.IP, login.CompanyDB, login.UserName, login.Password);
                    await serviceLayer.Request("Login").PostAsync<LoginObj>(login);
                    direccion = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Session-JSON\\DEMO_EFD.json");
                    break;
            }

            serviceLayer.AfterCall(async call =>
            {
                Console.WriteLine($"Response: { call.HttpResponseMessage?.StatusCode}");
                string contenido = await call.HttpResponseMessage?.Content?.ReadAsStringAsync();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(direccion, false);
                sw.WriteLine(contenido);
                sw.Close();

            });

            return serviceLayer;
        }
    }
}

