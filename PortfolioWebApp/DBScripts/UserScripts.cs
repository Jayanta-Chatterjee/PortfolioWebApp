using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebApp.DBScripts
{
    public class UserScripts
    {
        public static string AddNew = "insert into Users(FullName,UserName,Password) values(@FullName,@UserName,@Password); SELECT @@IDENTITY;";
        public static string AddWorkDetails = "update works set Details=@Details where userId=@userId and date=@date";
        public static string AddLogoutDate = "update works set LogoutDate=@Logout where userId=@userId and date=@date";
        public static string AddLoginDate = "insert into Works([Date],LoginDate,UserId) values(@Date,@Login,@UserId)";

        public static string Update = "upadte users set name=@name where id=@id";

        public static string IsWorkDetailsAdded = "Select id,Details from works where userId=@userId and date=CONVERT(VARCHAR(10), @date, 111)";
        public static string IsUserNameAvailable = "select id from users where UserName=@UserName";
        public static string GetHistory = "select users.userName,CONVERT(VARCHAR(10), works.date, 111) as Date,"
            +" works.LoginDate,works.logoutDate,works.details,"
            + " CAST( DATEDIFF(hh, LoginDate, LogoutDate) as nvarchar(100)) +':'+ CAST(DATEDIFF(mi, DATEADD(hh, DATEDIFF(hh, LoginDate, LogoutDate), LoginDate),LogoutDate) as nvarchar(100)) as WorkingHours"
            +" from users inner join works on works.UserId=users.Id where users.username=@username";

    }
}