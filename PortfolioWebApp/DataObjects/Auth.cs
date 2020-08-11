using System;

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
          
            if (user!=null )
            {
                var workDetails = new DataAccess.User().IsWorkDetailsAdded(user.Id, DateTime.Now.Date);

                if (workDetails==null || !workDetails.HasRows)
                {
                    var work = new Models.Work();
                    work.UserId = user.Id;
                    work.Date = DateTime.Now.Date;
                    work.Login = DateTime.Now;
                    new DataAccess.User().AddLoginDate(work); 
                }
            }
            return user;
        }
    }
}