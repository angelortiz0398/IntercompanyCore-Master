using System;
using System.Collections.Generic;
using System.Text;

namespace IntercompanyCore.DTOs
{ 
    public class TransaccionCreacionDTO
    {
        public DateTime FechaTransaccion { get; set; }
        public DateTime FechaSincronizacion { get; set; }
        public int TipoTransaccion { get; set; }
        public string TipoCRUD { get; set; }
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public char Sincronizado { get; set; }
        public string ErrorDesc { get; set; }
        public string JSON { get; set; }
        public int IdObjeto { get; set; }
    }
}
