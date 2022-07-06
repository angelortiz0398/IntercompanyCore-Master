using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class ChartOfAccounts
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public char ActiveAccount { get; set; }
        public string AcctCurrency { get; set; }
        public char CashAccount { get; set; }
        public string FatherAccountKey { get; set; }
        public char U_natur { get; set; }
        public int U_CodAgrup { get; set; }
        public int U_CodAgrup_Nivel { get; set; }
        public string U_DescSAT { get; set; }
        public string U_CuentaOrden { get; set; }
        public int U_Nivel { get; set; }
        public ChartOfAccounts()
        {

        }

        public ChartOfAccounts(string code, string name, char activeAccount, string acctCurrency, char cashAccount, string fatherAccountKey, char u_natur, int u_CodAgrup, int u_CodAgrup_Nivel, string u_DescSAT, string u_CuentaOrden, int u_Nivel)
        {
            Code = code;
            Name = name;
            ActiveAccount = activeAccount;
            AcctCurrency = acctCurrency;
            CashAccount = cashAccount;
            U_natur = u_natur;
            U_CodAgrup = u_CodAgrup;
            U_CodAgrup_Nivel = u_CodAgrup_Nivel;
            U_DescSAT = u_DescSAT;
            U_CuentaOrden = u_CuentaOrden;
            U_Nivel = u_Nivel;
            FatherAccountKey = fatherAccountKey;
        }
    }
}
