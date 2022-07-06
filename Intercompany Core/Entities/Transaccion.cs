using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IntercompanyCore.Entities
{
    public class Transaccion
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("fechaTransaccion")]
        public DateTime FechaTransaccion { get; set; }
        [JsonPropertyName("fechaSincronizacion")]
        public DateTime FechaSincronizacion { get; set; }
        [JsonPropertyName("tipoTransaccion")]
        public int TipoTransaccion { get; set; }
        [JsonPropertyName("tipoCRUD")]
        public string TipodCRUD { get; set; }
        [JsonPropertyName("idOrigen")]
        public int IdOrigen { get; set; }
        [JsonPropertyName("idDestino")]
        public int IdDestino { get; set; }
        [JsonPropertyName("sincronizado")]
        public char Sincronizado { get; set; }
        [JsonPropertyName("errorDesc")]
        public string ErrorDesc { get; set; }
        [JsonPropertyName("json")]
        public string JSON { get; set; }
        [JsonPropertyName("idObjeto")]
        public int IdObjeto { get; set; }
        [JsonPropertyName("tipoOperacion")]
        public char TipoOperacion { get; set; }
        [JsonPropertyName("tipoDocumento")]
        public string TipoDocumento { get; set; }
    }
}
