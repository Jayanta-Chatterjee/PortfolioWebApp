using MySql.Data.MySqlClient;
using PortfolioWebApp.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;


namespace PortfolioWebApp.DataAccess
{
    public class Auth
    {
        public MySqlDataReader Login(string UserName, string password)
        {
            MySqlDataReader result = null;
            using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new MySqlDataAdapter())
                {
                    adpt.SelectCommand = new MySqlCommand();
                    adpt.SelectCommand.Connection = conn;
                    adpt.SelectCommand.CommandType = CommandType.Text;
                    adpt.SelectCommand.CommandText = DBScripts.AuthScripts.Login;
                    adpt.SelectCommand.Parameters.AddWithValue("@UserName", UserName);
                    adpt.SelectCommand.Parameters.AddWithValue("@Password", Encoding.ASCII.GetBytes(password));// Encoding.ASCII.GetString(password)
                    result = adpt.SelectCommand.ExecuteReader();
                }
            }
            return result;
        }
    }
}