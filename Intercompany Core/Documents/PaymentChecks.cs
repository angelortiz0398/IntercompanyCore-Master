using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore.Documents
{
    public class PaymentChecks
    {
        public string DueDate { get; set; }
        public string CheckNumber { get; set; }
        public string BankCode { get; set; }
        public string Branch { get; set; }
        public string AccounttNum { get; set; }
        public string Trnsfrable { get; set; }
        public string CheckSum { get; set; }
        public string Currency { get; set; }
        public string CountryCode { get; set; }
        public string CheckAbsEntry { get; set; }
        public string CheckAccount { get; set; }
        public string ManualCheck { get; set; }
        public string Endorse { get; set; }
    }
}
