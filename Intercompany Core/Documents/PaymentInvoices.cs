using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore.Documents
{
    public class PaymentInvoices
    {
        public int DocEntry { get; set; }
        public string SumApplied { get; set; }

        public PaymentInvoices(int docEntry, string sumApplied)
        {
            DocEntry = docEntry;
            SumApplied = sumApplied;
        }
    }
}
