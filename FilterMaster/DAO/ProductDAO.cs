using FilterMaster.Entity;
using FilterMaster.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterMaster.DAO
{
    class ProductDAO
    {
        DBContext dBContext = new DBContext();

        public List<KeyValuePair<int, String>> GetAllProducts()
        {
            List<KeyValuePair<int, String>> data_product = new List<KeyValuePair<int, String>>();
           
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select * from Products";
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataReader reader = command.ExecuteReader();
            data_product.Add(new KeyValuePair<int, string>(0, "All"));
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                String name = reader.GetString(1);
                data_product.Add(new KeyValuePair<int, string>(id,name));
            }

            cnn.Close();

            return data_product;
        }


        public Decimal GetUnitPrice(int id)
        {

            Decimal num = 0;
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select UnitPrice from Products"+
                             " where ProductID = @val1";

            SqlCommand command = new SqlCommand(query, cnn);
            command.Parameters.AddWithValue("@val1",id);
            SqlDataReader reader = command.ExecuteReader();
          
            if (reader.Read())
            {
                num = reader.GetDecimal(0);
            }

            cnn.Close();

            return num;
        }
    }
}
