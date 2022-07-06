using System.ComponentModel.DataAnnotations;

namespace IntercompanyCore.DTOs
{
    public class SocioNegociosCreacionDTO
    { 
        [Key]
        public int Clave { get; set; }
        public string Nombre { get; set; }
        public string CodSocioNegocios { get; set; }

        [Required]
        public int Grupo { get; set; }

        public char EsValido { get; set; }
        public string TipoSN { get; set; }
        public string TipoPersona { get; set; }
        [Required]
        public string Moneda { get; set; }

        public int Serie { get; set; }
        public string RFC { get; set; }
        public int CuentaDeudor { get; set; }

        public DateTime FechaHasta { get; set; }
        public int[] empresas { get; set; }

    }
}
