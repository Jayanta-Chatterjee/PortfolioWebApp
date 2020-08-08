using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using PortfolioWebApp.App_Start;
using System.Data;
using System.Text;
using System.Data.SqlClient;


namespace PortfolioWebApp.DataAccess
{
    public class User
    {
        //public string AddNew(Models.User user)
        //{
        //    string result = string.Empty;
        //    //System.Net.ServicePointManager.Expect100Continue = false;
        //    if (!IsUserNameAvailable(user.UserName.Trim()))
        //    {
        //       return "Email is not availabe";
        //    }
        //    using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
        //    {
        //        conn.Open();
        //        using (var adpt = new MySqlDataAdapter())
        //        {
        //            adpt.InsertCommand = new MySqlCommand();
        //            adpt.InsertCommand.Connection = conn;
        //            adpt.InsertCommand.CommandType = CommandType.Text;
        //            adpt.InsertCommand.CommandText = DBScripts.UserScripts.AddNew;
        //            adpt.InsertCommand.Parameters.AddWithValue("@FullName", user.FullName);
        //            adpt.InsertCommand.Parameters.AddWithValue("@UserName", user.UserName);
        //            adpt.InsertCommand.Parameters.AddWithValue("@Password", Encoding.ASCII.GetBytes( user.Password));
        //            var res = adpt.InsertCommand.ExecuteScalar();
        //            if (res!=null)
        //            {
        //                result = "Register successfylly";
        //                user.Id = Convert.ToInt32(res.ToString());
        //                var work = new Models.Work();
        //                work.UserId = user.Id;
        //                work.Date = DateTime.Now.Date;
        //                work.Login = DateTime.Now;
        //                AddLoginDate(work);
        //            }
        //            else
        //            {
        //                result = "Something went worng";
        //            }                   
        //        }
        //    }
        //    return result;
        //}
        public string AddNew(Models.User user)
        {
            string result = string.Empty;
            //System.Net.ServicePointManager.Expect100Continue = false;
            if (!IsUserNameAvailable(user.UserName.Trim()))
            {
                return "Email is not availabe";
            }
            using (var conn = new SqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new SqlDataAdapter())
                {
                    adpt.InsertCommand = new SqlCommand();
                    adpt.InsertCommand.Connection = conn;
                    adpt.InsertCommand.CommandType = CommandType.Text;
                    adpt.InsertCommand.CommandText = DBScripts.UserScripts.AddNew;
                    adpt.InsertCommand.Parameters.AddWithValue("@FullName", user.FullName);
                    adpt.InsertCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    adpt.InsertCommand.Parameters.AddWithValue("@Password", Encoding.ASCII.GetBytes(user.Password));
                    var res = adpt.InsertCommand.ExecuteScalar();
                    if (res != null)
                    {
                        result = "Register successfylly";
                        user.Id = Convert.ToInt32(res.ToString());
                        //var work = new Models.Work();
                        //work.UserId = user.Id;
                        //work.Date = DateTime.Now.Date;
                        //work.Login = DateTime.Now;
                        //AddLoginDate(work);
                    }
                    else
                    {
                        result = "Something went worng";
                    }
                }
            }
            return result;
        }
        //public MySqlDataReader IsWorkDetailsAdded(int userId,DateTime date)
        //{
        //    MySqlDataReader result = null;
        //    using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
        //    {
        //        conn.Open();
        //        using (var adpt = new MySqlDataAdapter())
        //        {
        //            adpt.SelectCommand = new MySqlCommand();
        //            adpt.SelectCommand.Connection = conn;
        //            adpt.SelectCommand.CommandType = CommandType.Text;
        //            adpt.SelectCommand.CommandText = DBScripts.UserScripts.IsWorkDetailsAdded;
        //            adpt.SelectCommand.Parameters.AddWithValue("@userId",userId);
        //            adpt.SelectCommand.Parameters.AddWithValue("@date", date);
        //            result = adpt.SelectCommand.ExecuteReader();
        //        }
        //    }
        //    return result;
        //}
        public SqlDataReader IsWorkDetailsAdded(int userId, DateTime date)
        {
            SqlDataReader result = null;

            using (var adpt = new SqlDataAdapter())
            {
                adpt.SelectCommand = new SqlCommand();
                adpt.SelectCommand.Connection = new SqlConnection(Settings.MySQLConnectionString);
                adpt.SelectCommand.Connection.Open();
                adpt.SelectCommand.CommandType = CommandType.Text;
                adpt.SelectCommand.CommandText = DBScripts.UserScripts.IsWorkDetailsAdded;
                adpt.SelectCommand.Parameters.AddWithValue("@userId", userId);
                adpt.SelectCommand.Parameters.AddWithValue("@date", date);
                result = adpt.SelectCommand.ExecuteReader();
            }

            return result;
        }
        private bool IsUserNameAvailable(string userName)
        {
            bool result = false;
            using (var conn = new SqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new SqlDataAdapter())
                {
                    adpt.SelectCommand = new SqlCommand();
                    adpt.SelectCommand.Connection = conn;
                    adpt.SelectCommand.CommandType = CommandType.Text;
                    adpt.SelectCommand.CommandText = DBScripts.UserScripts.IsUserNameAvailable;
                    adpt.SelectCommand.Parameters.AddWithValue("@UserName", userName);
                    result = (adpt.SelectCommand.ExecuteScalar() == null);
                }
            }
            return result;
        }

        //private bool AddLoginDate(Models.Work work)
        //{
        //    bool result = false;
        //    using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
        //    {
        //        conn.Open();
        //        using (var adpt = new MySqlDataAdapter())
        //        {
        //            adpt.InsertCommand = new MySqlCommand();
        //            adpt.InsertCommand.Connection = conn;
        //            adpt.InsertCommand.CommandType = CommandType.Text;
        //            adpt.InsertCommand.CommandText = DBScripts.UserScripts.AddLoginDate;
        //            adpt.InsertCommand.Parameters.AddWithValue("@Date", work.Date);
        //            adpt.InsertCommand.Parameters.AddWithValue("@Login", work.Login);
        //            adpt.InsertCommand.Parameters.AddWithValue("@UserId", work.UserId);
        //            result = adpt.InsertCommand.ExecuteNonQuery() > 0;
        //        }
        //    }
        //    return result;
        //}

        public bool AddLoginDate(Models.Work work)
        {
            bool result = false;
            using (var conn = new SqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new SqlDataAdapter())
                {
                    adpt.InsertCommand = new SqlCommand();
                    adpt.InsertCommand.Connection = conn;
                    adpt.InsertCommand.CommandType = CommandType.Text;
                    adpt.InsertCommand.CommandText = DBScripts.UserScripts.AddLoginDate;
                    adpt.InsertCommand.Parameters.AddWithValue("@Date", work.Date);
                    adpt.InsertCommand.Parameters.AddWithValue("@Login", work.Login);
                    adpt.InsertCommand.Parameters.AddWithValue("@UserId", work.UserId);
                    result = adpt.InsertCommand.ExecuteNonQuery() > 0;
                }
            }
            return result;
        }
        //public bool AddWorkDetails(Models.Work work)
        //{
        //    bool result = false;
        //    using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
        //    {
        //        conn.Open();
        //        using (var adpt = new MySqlDataAdapter())
        //        {
        //            adpt.UpdateCommand = new MySqlCommand();
        //            adpt.UpdateCommand.Connection = conn;
        //            adpt.UpdateCommand.CommandType = CommandType.Text;
        //            adpt.UpdateCommand.CommandText = DBScripts.UserScripts.AddWorkDetails;
        //            adpt.UpdateCommand.Parameters.AddWithValue("@Details", work.Details);
        //            adpt.UpdateCommand.Parameters.AddWithValue("@userId", work.UserId);
        //            adpt.UpdateCommand.Parameters.AddWithValue("@date", work.Date);
        //            result = adpt.UpdateCommand.ExecuteNonQuery() > 0;
        //        }
        //    }
        //    return result;
        //}

        public bool AddWorkDetails(Models.Work work)
        {
            bool result = false;
            using (var conn = new SqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new SqlDataAdapter())
                {
                    adpt.UpdateCommand = new SqlCommand();
                    adpt.UpdateCommand.Connection = conn;
                    adpt.UpdateCommand.CommandType = CommandType.Text;
                    adpt.UpdateCommand.CommandText = DBScripts.UserScripts.AddWorkDetails;
                    adpt.UpdateCommand.Parameters.AddWithValue("@Details", work.Details);
                    adpt.UpdateCommand.Parameters.AddWithValue("@userId", work.UserId);
                    adpt.UpdateCommand.Parameters.AddWithValue("@date", work.Date);
                    result = adpt.UpdateCommand.ExecuteNonQuery() > 0;
                }
            }
            return result;
        }
        //public bool AddLogoutDate(Models.Work work)
        //{
        //    bool result = false;
        //    using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
        //    {
        //        conn.Open();
        //        using (var adpt = new MySqlDataAdapter())
        //        {
        //            adpt.UpdateCommand = new MySqlCommand();
        //            adpt.UpdateCommand.Connection = conn;
        //            adpt.UpdateCommand.CommandType = CommandType.Text;
        //            adpt.UpdateCommand.CommandText = DBScripts.UserScripts.AddLogoutDate;
        //            adpt.UpdateCommand.Parameters.AddWithValue("@Logout", work.Logout);
        //            adpt.UpdateCommand.Parameters.AddWithValue("@userId", work.UserId);
        //            adpt.UpdateCommand.Parameters.AddWithValue("@date", work.Date);
        //            result = adpt.UpdateCommand.ExecuteNonQuery() > 0;
        //        }
        //    }
        //    return result;
        //}

        public bool AddLogoutDate(Models.Work work)
        {
            bool result = false;
            using (var conn = new SqlConnection(Settings.MySQLConnectionString))
            {
                conn.Open();
                using (var adpt = new SqlDataAdapter())
                {
                    adpt.UpdateCommand = new SqlCommand();
                    adpt.UpdateCommand.Connection = conn;
                    adpt.UpdateCommand.CommandType = CommandType.Text;
                    adpt.UpdateCommand.CommandText = DBScripts.UserScripts.AddLogoutDate;
                    adpt.UpdateCommand.Parameters.AddWithValue("@Logout", work.Logout);
                    adpt.UpdateCommand.Parameters.AddWithValue("@userId", work.UserId);
                    adpt.UpdateCommand.Parameters.AddWithValue("@date", work.Date);
                    result = adpt.UpdateCommand.ExecuteNonQuery() > 0;
                }
            }
            return result;
        }

        //public MySqlDataReader GetHistory(string userName)
        //{
        //    MySqlDataReader result = null;
        //    using (var conn = new MySqlConnection(Settings.MySQLConnectionString))
        //    {
        //        conn.Open();
        //        using (var adpt = new MySqlDataAdapter())
        //        {
        //            adpt.SelectCommand = new MySqlCommand();
        //            adpt.SelectCommand.Connection = conn;
        //            adpt.SelectCommand.CommandType = CommandType.Text;
        //            adpt.SelectCommand.CommandText = DBScripts.UserScripts.GetHistory;
        //            adpt.SelectCommand.Parameters.AddWithValue("@username", userName);
        //            result = adpt.SelectCommand.ExecuteReader();
        //        }
        //    }
        //    return result;
        //}

        public SqlDataReader GetHistory(string userName)
        {
            SqlDataReader result = null;
            using (var adpt = new SqlDataAdapter())
            {
                adpt.SelectCommand = new SqlCommand();
                adpt.SelectCommand.Connection = new SqlConnection(Settings.MySQLConnectionString);
                adpt.SelectCommand.Connection.Open();
                adpt.SelectCommand.CommandType = CommandType.Text;
                adpt.SelectCommand.CommandText = DBScripts.UserScripts.GetHistory;
                adpt.SelectCommand.Parameters.AddWithValue("@username", userName);
                result = adpt.SelectCommand.ExecuteReader();
            }
            return result;
        }
    }
}