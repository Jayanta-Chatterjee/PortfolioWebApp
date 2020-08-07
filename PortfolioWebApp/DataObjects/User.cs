using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;

namespace PortfolioWebApp.DataObjects
{
    public class User
    {
        public string AddNew(Models.User user)
        {
            return new DataAccess.User().AddNew(user);
        }
        public Models.Work GetWorkDetailsAdded(int userId, DateTime date)
        {
            Models.Work work = null;
            using (var reader = new DataAccess.User().IsWorkDetailsAdded(userId, date))
            {
                while (reader.Read())
                {
                    work = new Models.Work();
                    work.Date = date;
                    work.Id = Convert.ToInt32(reader["id"].ToString());
                    work.Details = reader["Details"].ToString();
                }
            }
            return work;
        }
        public bool AddWorkDetails(Models.Work work)
        {
            return new DataAccess.User().AddWorkDetails(work);
        }
        public bool AddLogoutDate(Models.Work work)
        {
            return new DataAccess.User().AddLogoutDate(work);
        }
        public string GetHistoryFilePath(string userName)
        {
            string fileContent = null;
            using (var reader = new DataAccess.User().GetHistory(userName))
            {
                var dtHistory = new DataTable("UserHistory");
                dtHistory.Load(reader);
                fileContent = GetCVSFilePath(dtHistory);
            }
            return fileContent;
        }
        private string GetCVSFilePath(DataTable dtDataTable)
        {
            StringBuilder sw = new StringBuilder();
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Append(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Append(",");
                }
            }
            sw.AppendLine();
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Append(value);
                        }
                        else
                        {
                            sw.Append(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Append(",");
                    }
                }
                sw.AppendLine();
            }
            return sw.ToString();
        }
    }
}