using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IntercompanyCore
{
    public class Items
    { 
        [Key]
        [JsonPropertyName("clave")]
        public int Clave { get; set; }
        [JsonPropertyName("codItems")]
        public string CodItems { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [Required]
        [JsonPropertyName("grupo")]
        public int Grupo { get; set; }

        [JsonPropertyName("esInventario")]
        public char EsInventario { get; set; }
        [JsonPropertyName("esVentas")]
        public char EsVentas { get; set; }
        [JsonPropertyName("esCompras")]
        public char EsCompras { get; set; }
        [JsonPropertyName("metodoInv")]
        public char MetodoInv { get; set; }
        [JsonPropertyName("metodoCosto")]
        public char MetodoCosto { get; set; }
        [JsonPropertyName("costo")]
        public double Costo { get; set; }
        [JsonPropertyName("esValido")]
        public char EsValido { get; set; }
        [JsonPropertyName("fechaHasta")]
        public DateTime FechaHasta { get; set; }
        [JsonPropertyName("codBarras")]
        public string CodBarras { get; set; }
        [JsonPropertyName("gesItems")]
        public string GesItems { get; set; }

        public Items(int clave, string codItems, string nombre, int grupo, char esInventario, char esVentas, char esCompras, char metodoInv, char metodoCosto, double costo, char esValido, DateTime fechaHasta, string codBarras, string gesItems)
        {
            Clave = clave;
            CodItems = codItems;
            Nombre = nombre;
            Grupo = grupo;
            EsInventario = esInventario;
            EsVentas = esVentas;
            EsCompras = esCompras;
            MetodoInv = metodoInv;
            MetodoCosto = metodoCosto;
            Costo = costo;
            EsValido = esValido;
            FechaHasta = fechaHasta;
            CodBarras = codBarras;
            GesItems = gesItems;
        }
    }
}