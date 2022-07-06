using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntercompanyCore.Documents
{
    public class PE
    {
        [JsonPropertyName("DocEntry")]
        public int DocEntry { get; set; }
        [JsonPropertyName("DocNum")]
        public int DocNum { get; set; }
        [JsonPropertyName("CardCode")]
        public string CardCode { get; set; }
        [JsonPropertyName("CardName")]
        public string CardName { get; set; }
        [JsonPropertyName("DocDate")]
        public DateTime DocDate { get; set; }


        [JsonPropertyName("U_KAI01_Intercompany")]
        public char U_KAI01_Intercompany { get; set; }
        [JsonPropertyName("U_KAI01_Sincronizado")]
        public char U_KAI01_Sincronizado { get; set; }
        [JsonPropertyName("U_KAI01_EmpresaDestino")]
        public string U_KAI01_EmpresaDestino { get; set; }
        [JsonPropertyName("U_KAI01_SN")]
        public string U_KAI01_SN { get; set; }
        [JsonPropertyName("TransferAccount")]
        public string TransferAccount { get; set; }
        [JsonPropertyName("TransferDate")]
        public string TransferDate { get; set; }
        [JsonPropertyName("TransferSum")]
        public string TransferSum { get; set; }
        [JsonPropertyName("CheckAccount")]
        public string CheckAccount { get; set; }

        [JsonPropertyName("PaymentInvoices")]
        public IList<PaymentInvoices> PaymentInvoices { get; set; }
        [JsonPropertyName("PaymentChecks")]
        public IList<PaymentChecks> PaymentChecks { get; set; }

        [JsonPropertyName("PaymentCreditCards")]
        public IList<PaymentChecks> PaymentCreditCards { get; set; }

        public PE(string cardCode, DateTime docDate, char u_KAI01_Intercompany, char sincronizado, string empresaDestino, string u_KAI01_SN, string transferAccount, string transferSum, string checkAccount)
        {
            CardCode = cardCode;
            DocDate = docDate;
            U_KAI01_Intercompany = u_KAI01_Intercompany;
            U_KAI01_Sincronizado = sincronizado;
            U_KAI01_EmpresaDestino = empresaDestino;
            U_KAI01_SN = u_KAI01_SN;
            TransferAccount = transferAccount;
            TransferSum = transferSum;
            CheckAccount = checkAccount;
        }
    }
}