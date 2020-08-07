using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebApp.DataObjects
{
    public class Auth
    {
        public Models.User Login(string userName,string password)
        {
            Models.User user = null;
            using (var reader=new DataAccess.Auth().Login(userName,password))
            {
                while (reader.Read())
                {
                    user = new Models.User();
                    user.Id =Convert.ToInt32( reader["Id"].ToString());
                    user.FullName = reader["FullName"].ToString();
                    user.UserName = reader["UserName"].ToString();
                }
            }
            return user;
        }
    }
}