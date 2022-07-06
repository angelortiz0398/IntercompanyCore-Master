using System.ComponentModel.DataAnnotations;

namespace IntercompanyCore.DTOs
{
    public class ItemsCreacionDTO
    { 
        [Key]
        public int Clave { get; set; }
        public string CodItems { get; set; }
        public string Nombre { get; set; }

        [Required]
        public int Grupo { get; set; }

        public char EsInventario { get; set; }
        public char EsVentas { get; set; }
        public char EsCompras { get; set; }
        public char MetodoInv { get; set; }
        public char MetodoCosto { get; set; }
        public int Costo { get; set; }
        public char EsValido { get; set; }

        public DateTime FechaHasta { get; set; }
        public int[] empresas { get; set; }
    }
}
