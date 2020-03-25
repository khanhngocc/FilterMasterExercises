using FilterMaster.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterMaster.DAO
{
    class ShipperDAO
    {
        DBContext dBContext = new DBContext();

        public List<KeyValuePair<int, String>> GetAllShipper()
        {
            List<KeyValuePair<int, String>> data = new List<KeyValuePair<int, String>>();

            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select * from Shippers";
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataReader reader = command.ExecuteReader();
            data.Add(new KeyValuePair<int, string>(0, "All"));
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                String name = reader.GetString(1);
                data.Add(new KeyValuePair<int, string>(id, name));
            }

            cnn.Close();

            return data;
        }

    }
}
