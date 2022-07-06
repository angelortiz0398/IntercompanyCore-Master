using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntercompanyCore
{
    public class LoginObj
    {
        public string IP { get; set; }
        public string CompanyDB { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public LoginObj(string companyDB, string userName, string password)
        {
            CompanyDB = companyDB;
            Password = password;
            UserName = userName;
        }

        public LoginObj()
        {
        }
    }
}
 