using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using PortfolioWebApp.App_Start;
using System.Data;
using System.Text;

namespace PortfolioWebApp.DataAccess
{
    public class User
    {
        public bool AddNew(Models.User user)
        {
            bool result = false;
            //System.Net.ServicePointManager.Expect100Continue = false;

            using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new MySqlDataAdapter())
                {
                    adpt.InsertCommand = new MySqlCommand();
                    adpt.InsertCommand.Connection = conn;
                    adpt.InsertCommand.CommandType = CommandType.Text;
                    adpt.InsertCommand.CommandText = DBScripts.UserScripts.AddNew;
                    adpt.InsertCommand.Parameters.AddWithValue("@FullName", user.FullName);
                    adpt.InsertCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    adpt.InsertCommand.Parameters.AddWithValue("@Password", Encoding.ASCII.GetBytes( user.Password));
                   result= adpt.InsertCommand.ExecuteNonQuery()>0;
                }
            }
            return result;
        }

        public MySqlDataReader IsWorkDetailsAdded(int userId,DateTime date)
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
                    adpt.SelectCommand.CommandText = DBScripts.UserScripts.IsWorkDetailsAdded;
                    adpt.SelectCommand.Parameters.AddWithValue("@userId",userId);
                    adpt.SelectCommand.Parameters.AddWithValue("@date", date);
                    result = adpt.SelectCommand.ExecuteReader();
                }
            }
            return result;
        }
    }
}