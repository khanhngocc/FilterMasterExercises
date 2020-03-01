using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterMaster.Model
{
    class DBContext
    {
        SqlConnection connection;
        public SqlConnection GetConnection()
        {

            String connect_text = ConfigurationManager.ConnectionStrings["FilterMaster.Properties.Settings.Setting"].ConnectionString;
            connection = new SqlConnection(connect_text);
            return connection;
        }
    }
}
