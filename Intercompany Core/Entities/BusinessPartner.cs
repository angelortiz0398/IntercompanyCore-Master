using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class BusinessPartner
    {
        public string CardCode { get; set; }    
        public int GroupCode { get; set; }
        public string CardName { get; set; }    
        public char CardType { get; set; }
        public string FederalTaxID { get; set; }
        public char CompanyPrivate { get; set; }
        public string Currency { get; set; }
        public DateTime ValidTo { get; set; }

        public BusinessPartner()
        {
        }

        public BusinessPartner(string cardCode, int groupCode, string cardName, char cardType, string federalTaxID, char companyPrivate, string currency, DateTime validTo)
        {
            CardCode = cardCode;
            GroupCode = groupCode;
            CardName = cardName;
            CardType = cardType;
            FederalTaxID = federalTaxID;
            CompanyPrivate = companyPrivate;
            Currency = currency;
            ValidTo = validTo;
        }
    }
    
}
