using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntercompanyCore
{
    public class CentrosCosto
    {
        [Key]
        [JsonPropertyName("clave")]
        public int Clave { get; set; }
        [JsonPropertyName("codCentrosCosto")]
        public string CodCentrosCosto { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [Required]
        [JsonPropertyName("dimension")]
        public int Dimension { get; set; }
        [JsonPropertyName("esValido")]
        public char EsValido { get; set; }
        [JsonPropertyName("fechaHasta")]
        public DateTime FechaHasta { get; set; }

        public CentrosCosto(){

        }

        public CentrosCosto(int clave, string codCentrosCosto, string nombre, int dimension, char esValido, DateTime fechaHasta)
        {
            Clave = clave;
            CodCentrosCosto = codCentrosCosto;
            Nombre = nombre;
            Dimension = dimension;
            EsValido = esValido;
            FechaHasta = fechaHasta;
        }
    }
}