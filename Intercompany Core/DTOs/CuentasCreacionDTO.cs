using System.ComponentModel.DataAnnotations;

namespace IntercompanyCore.DTOs
{
    public class CuentasCreacionDTO
    { 
        [Key]
        public int Clave { get; set; }
        public string CodCuenta { get; set; }
        public string Nombre { get; set; }
        public char Tipo { get; set; }
        public int Nivel { get; set; }
        [Required]
        public string Moneda { get; set;}
        public char RPresupuesto { get; set; }
        public string Naturaleza { get; set; }

        [Required]
        public int CodigoSAT { get; set; }
        public int NivelSAT { get; set; }
        public string DescSAT { get; set; }
        public string CuentaOrden { get; set; }
        public int[] empresas { get; set; }
    }
}
