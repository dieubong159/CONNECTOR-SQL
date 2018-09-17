using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify
{
    static class DBConnect
    {
       public static SqlConnection sqlcon = new SqlConnection(Connectify.Properties.Settings.Default.ConnectionStr);

        public static DataTable getData(string query)
        {
            var dt = new DataTable();
            var da = new SqlDataAdapter(query, sqlcon);
            //sqlcon.Open();
            da.Fill(dt);
            sqlcon.Close();
            return dt;
        }
        static public IList<string> ListTables()
        {
            sqlcon.Open();
            List<string> tables = new List<string>();
            DataTable dt = sqlcon.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            sqlcon.Close();
            return tables;
        }
        //static public List<string> GetDatabaseList()
        //{
        //    List<string> list = new List<string>();

        //    // Open connection to the database
        //    sqlcon.Open();
        //        using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", sqlcon))
        //        {
        //            using (IDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    list.Add(dr[0].ToString());
        //                }
        //            }
        //        }
        //    sqlcon.Close();
        //    return list;
        //}
    }
}
