using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebApp.DBScripts
{
    public class UserScripts
    {
        public static string AddNew = "insert into Users(FullName,UserName,Password) values(@FullName,@UserName,@Password)";
        public static string AddWorkDetails = "update work set Details=@Details where userId=@userId and date=@date";
        public static string AddLogoutDate = "update work set Logout=@Logout where userId=@userId and date=@date";
        public static string AddLoginDate = "insert into Work([Date],Login,UserId) values(@Date,@Login,@UserId)";

        public static string Update = "upadte users set name=@name where id=@id";        

        public static string IsWorkDetailsAdded = "Select id,Details from work where userId=@userId and date=@date";

    }
}