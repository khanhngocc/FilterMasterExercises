using FilterMaster.Entity;
using FilterMaster.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace FilterMaster.DAO
{
    class EmployeeDAO
    {
        DBContext dBContext = new DBContext();
        public List<Employee> GetAllEmployees()
        {
            List<Employee> lists = new List<Employee>();
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select * from Employees";
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                String last_name = reader.GetString(1);
                String first_name = reader.GetString(2);
                Employee temp = new Employee();
                temp.Id = id;
                temp.Last_name = last_name;
                temp.First_name = first_name;
                lists.Add(temp);
            }

            cnn.Close();
          
            return lists;
        }
    }
}
