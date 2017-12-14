using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebH2O.Utils
{
    public class ConexaoSql
    {
        public static SqlConnection Open()
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConexaoSql"].ConnectionString);
            conn.Open();
            return conn;
        }
    }
}