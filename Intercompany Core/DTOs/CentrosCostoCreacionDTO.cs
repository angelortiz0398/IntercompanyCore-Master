using System.ComponentModel.DataAnnotations;

namespace IntercompanyCore.DTOs
{
    public class CentrosCostoCreacionDTO
    {
        [Key]
        public int Clave { get; set; }
        public string CodCentrosCosto { get; set; }
        public string Nombre { get; set; }
        [Required]
        public int Dimension { get; set; }
        public char EsValido { get; set; }

        public DateTime FechaHasta { get; set; }
        public int[] empresas { get; set; }
    } 
}
