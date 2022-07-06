using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore.ServiceLayer
{
    public class Account
    {
        string Cuenta { get; set; }

        public Account(string cuenta)
        {
            Cuenta = cuenta;
        }
    }
}
