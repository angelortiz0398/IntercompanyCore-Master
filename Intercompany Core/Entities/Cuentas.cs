
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntercompanyCore
{
    public class Cuentas
    { 
        [Key]
        [JsonPropertyName("clave")]
        public int Clave { get; set; }
        [JsonPropertyName("codCuenta")]
        public string CodCuenta { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("tipo")]
        public char Tipo { get; set; }
        [JsonPropertyName("nivel")]
        public int Nivel { get; set; }
        [Required]
        [JsonPropertyName("moneda")]
        public string Moneda { get; set;}
        [JsonPropertyName("rPresupuesto")]
        public char RPresupuesto { get; set; }
        [JsonPropertyName("naturaleza")]
        public string Naturaleza { get; set; }

        [Required]
        [JsonPropertyName("codigoSAT")]
        public int CodigoSAT { get; set; }
        [JsonPropertyName("nivelSAT")]
        public int NivelSAT { get; set; }
        [JsonPropertyName("descSAT")]
        public string DescSAT { get; set; }
        [JsonPropertyName("cuentaOrden")]
        public string CuentaOrden { get; set; }
        [JsonPropertyName("fatherAccnt")]
        public string FatherAccnt { get; set; }

        public Cuentas(){

        }

        public Cuentas(int clave, string codCuenta, string nombre, char tipo, int nivel, string moneda, char rPresupuesto, string naturaleza, int codigoSAT, int nivelSAT, string descSAT, string cuentaOrden, string fatherAccnt)
        {
            Clave = clave;
            CodCuenta = codCuenta;
            Nombre = nombre;
            Tipo = tipo;
            Moneda = moneda;
            RPresupuesto = rPresupuesto;
            Naturaleza = naturaleza;
            CodigoSAT = codigoSAT;
            NivelSAT = nivelSAT;
            DescSAT = descSAT;
            CuentaOrden = cuentaOrden;
            FatherAccnt = fatherAccnt;
        }
    }
}