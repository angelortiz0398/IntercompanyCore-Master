using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntercompanyCore.Documents
{
    public class PR
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

        public PR(int docEntry, int docNum, string cardCode, string cardName, string docCurrency, DateTime docDate, DateTime docDueDate, string discountPercent, char u_KAI01_Intercompany, char u_KAI01_Sincronizado, string u_KAI01_EmpresaDestino, string transferAccount, string transferDate, string transferSum)
        {
            DocEntry = docEntry;
            DocNum = docNum;
            CardCode = cardCode;
            CardName = cardName;
            DocCurrency = docCurrency;
            DocDate = docDate;
            DocDueDate = docDueDate;
            DiscountPercent = discountPercent;
            U_KAI01_Intercompany = u_KAI01_Intercompany;
            U_KAI01_Sincronizado = u_KAI01_Sincronizado;
            U_KAI01_EmpresaDestino = u_KAI01_EmpresaDestino;
            TransferAccount = transferAccount;
            TransferDate = transferDate;
            TransferSum = transferSum;

        }
    }
}