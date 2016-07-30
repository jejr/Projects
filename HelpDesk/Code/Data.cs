using System;
using System.Data;
using System.Data.SqlClient;

namespace HelpDesk.Code
{
    public class Data
    {
        public static DataTable SQL (string query)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB_HD.Properties.Settings.HelpdeskConnectionString"].ConnectionString);
            SqlDataAdapter adap = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();

            adap.Fill(dt);

            return dt;
        }
    }
}