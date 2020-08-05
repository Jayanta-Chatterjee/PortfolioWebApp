using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebApp.DBScripts
{
    public class AuthScripts
    {
        public static string Login = "select * from User where UserName=@UserName and Password=@Password";
        public static string Logout = "";

    }
}