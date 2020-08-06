using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioWebApp.DataObjects
{
    public class User
    {
        public bool AddNew(Models.User user)
        {
            return new DataAccess.User().AddNew(user);
        }
    }
}