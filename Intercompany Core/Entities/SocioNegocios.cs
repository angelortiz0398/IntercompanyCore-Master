
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntercompanyCore
{
    public class SocioNegocios
    { 
        public SocioNegocios(int clave, string codSocioNegocios, string nombre, int grupo, char esValido, string tipoSN, string tipoPersona, string moneda, int serie, string rFC, int cuentaDeudor, DateTime fechaHasta)
        {
            Clave = clave;
            CodSocioNegocios = codSocioNegocios;
            Nombre = nombre;
            Grupo = grupo;
            EsValido = esValido;
            TipoSN = tipoSN;
            TipoPersona = tipoPersona;
            Moneda = moneda;
            Serie = serie;
            RFC = rFC;
            CuentaDeudor = cuentaDeudor;
            FechaHasta = fechaHasta;
        }

        public SocioNegocios()
        {
        }

        [Key]
        [JsonPropertyName("clave")]
        public int Clave { get; set; }
        [JsonPropertyName("codSocioNegocios")]
        public string CodSocioNegocios { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [Required]
        [JsonPropertyName("grupo")]
        public int Grupo { get; set; }
        [JsonPropertyName("esValido")]
        public char EsValido { get; set; }
        [JsonPropertyName("tipoSN")]
        public string TipoSN { get; set; }
        [JsonPropertyName("tipoPersona")]
        public string TipoPersona { get; set; }
        [Required]
        [JsonPropertyName("moneda")]
        public string Moneda { get; set; }

        [JsonPropertyName("serie")]
        public int Serie { get; set; }
        [JsonPropertyName("rfc")]
        public string RFC { get; set; }

        [JsonPropertyName("cuentaDeudor")]
        public int CuentaDeudor { get; set; }

        [JsonPropertyName("fechaHasta")]
        public DateTime FechaHasta { get; set; }

    }
    
}