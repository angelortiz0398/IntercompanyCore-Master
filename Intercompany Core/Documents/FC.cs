using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntercompanyCore.Documents
{
    public class FC
    {
        [JsonPropertyName("DocEntry")]
        public int DocEntry { get; set; }
        [JsonPropertyName("DocNum")]
        public int DocNum { get; set; }
        [JsonPropertyName("CardCode")]
        public string CardCode { get; set; }
        [JsonPropertyName("CardName")]
        public string CardName { get; set; }
        [JsonPropertyName("DocCurrency")]
        public string DocCurrency { get; set; }
        [JsonPropertyName("DocDate")]
        public DateTime DocDate { get; set; }
        [JsonPropertyName("DocDueDate")]
        public DateTime DocDueDate { get; set; }
        [JsonPropertyName("DiscountPercent")]
        public string DiscountPercent { get; set; }
        [JsonPropertyName("U_KAI01_Intercompany")]
        public char U_KAI01_Intercompany { get; set; }
        [JsonPropertyName("U_KAI01_Sincronizado")]
        public char U_KAI01_Sincronizado { get; set; }
        [JsonPropertyName("U_KAI01_EmpresaDestino")]
        public string U_KAI01_EmpresaDestino { get; set; }
        [JsonPropertyName("U_KAI01_SN")]
        public string U_KAI01_SN { get; set; }
        [JsonPropertyName("U_KAI01_Referencia")]
        public int? U_KAI01_Referencia { get; set; }

        [JsonPropertyName("BaseType")]
        public string BaseType { get; set; }
        [JsonPropertyName("BaseEntry")]
        public string BaseEntry { get; set; }
        [JsonPropertyName("BaseLine")]
        public string BaseLine { get; set; }

        // Quitar esto si no funciona
        [JsonPropertyName("DocTotal")]
        public string DocTotal { get; set; }
        [JsonPropertyName("DocumentStatus")]
        public string DocumentStatus { get; set; }

        [JsonPropertyName("DocumentLines")]
        public IList<DocumentLines> DocumentLines { get; set; }

        public FC(string cardCode, DateTime docDate, DateTime docDueDate, char u_KAI01_Intercompany, char u_KAI01_Sincronizado, string u_KAI01_EmpresaDestino, string u_KAI01_SN, int u_KAI01_Referencia)
        {
            CardCode = cardCode;
            DocDate = docDate;
            DocDueDate = docDueDate;
            U_KAI01_Intercompany = u_KAI01_Intercompany;
            U_KAI01_Sincronizado = u_KAI01_Sincronizado;
            U_KAI01_EmpresaDestino = u_KAI01_EmpresaDestino;
            U_KAI01_SN = u_KAI01_SN;
            DocumentStatus = "bost_Open";
            U_KAI01_Referencia = u_KAI01_Referencia;
        }
    }
}