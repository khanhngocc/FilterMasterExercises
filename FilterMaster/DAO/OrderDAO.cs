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
    class OrderDAO
    {
        DBContext dBContext = new DBContext();
        public List<Order> GetAllData1(DateTime from,DateTime to)
        {
            List<Order> lists = new List<Order>();
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select OrderID,Employees.FirstName+' '+Employees.LastName as 'Employee',Customers.CompanyName as 'Customer',Orders.OrderDate,Orders.RequiredDate,Orders.ShippedDate,Orders.ShipVia from Employees,Customers,Orders"
                      + " where"
                      + " Employees.EmployeeID = Orders.EmployeeID"
                      + " and"
                      + " Customers.CustomerID = Orders.CustomerID"
                      + " and"
                      + " Orders.OrderDate >= @val1 "
                      +" and"
                      +" Orders.RequiredDate <= @val2"
                      ;
            SqlCommand command = new SqlCommand(query, cnn);
            command.Parameters.AddWithValue("@val1",from);
            command.Parameters.AddWithValue("@val2",to);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order temp = new Order();
                if (!reader.IsDBNull(0))
                {
                    temp.OrderID = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    temp.Employee = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    temp.Customer = reader.GetString(2);
                }
                if (!reader.IsDBNull(3))
                {
                    temp.OrderDate = reader.GetDateTime(3);
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4);
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate =  reader.GetDateTime(5);
                }
                if (!reader.IsDBNull(6))
                {
                    temp.ShipVia = reader.GetInt32(6);
                }
               
                lists.Add(temp);
            }

            cnn.Close();

            return lists;
        }

        public List<Order> GetAllData2(DateTime from, DateTime to, int employee_id)
        {
            List<Order> lists = new List<Order>();
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select OrderID,Employees.FirstName+' '+Employees.LastName as 'Employee',Customers.CompanyName as 'Customer',Orders.OrderDate,Orders.RequiredDate,Orders.ShippedDate,Orders.ShipVia from Employees,Customers,Orders"
                      + " where"
                      + " Employees.EmployeeID = Orders.EmployeeID"
                      + " and"
                      + " Customers.CustomerID = Orders.CustomerID"
                      + " and"
                      + " Orders.OrderDate >= @val1 "
                      + " and"
                      + " Orders.RequiredDate <= @val2"
                      + " and"
                      + " Employees.EmployeeID = @val3"
                      ;
            SqlCommand command = new SqlCommand(query, cnn);
            command.Parameters.AddWithValue("@val1", from);
            command.Parameters.AddWithValue("@val2", to);
            command.Parameters.AddWithValue("@val3",employee_id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order temp = new Order();
                if (!reader.IsDBNull(0))
                {
                    temp.OrderID = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    temp.Employee = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    temp.Customer = reader.GetString(2);
                }
                if (!reader.IsDBNull(3))
                {
                    temp.OrderDate = reader.GetDateTime(3);
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4);
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate = reader.GetDateTime(5);
                }
                if (!reader.IsDBNull(6))
                {
                    temp.ShipVia = reader.GetInt32(6);
                }

                lists.Add(temp);
            }

            cnn.Close();

            return lists;
        }

        public List<Order> GetAllData3(DateTime from, DateTime to, String customer_id)
        {
            List<Order> lists = new List<Order>();
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select OrderID,Employees.FirstName+' '+Employees.LastName as 'Employee',Customers.CompanyName as 'Customer',Orders.OrderDate,Orders.RequiredDate,Orders.ShippedDate,Orders.ShipVia from Employees,Customers,Orders"
                      + " where"
                      + " Employees.EmployeeID = Orders.EmployeeID"
                      + " and"
                      + " Customers.CustomerID = Orders.CustomerID"
                      + " and"
                      + " Orders.OrderDate >= @val1 "
                      + " and"
                      + " Orders.RequiredDate <= @val2"
                      + " and"
                      + " Customers.CustomerID = @val3"
                      ;
            SqlCommand command = new SqlCommand(query, cnn);
            command.Parameters.AddWithValue("@val1", from);
            command.Parameters.AddWithValue("@val2", to);
            command.Parameters.AddWithValue("@val3", customer_id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order temp = new Order();
                if (!reader.IsDBNull(0))
                {
                    temp.OrderID = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    temp.Employee = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    temp.Customer = reader.GetString(2);
                }
                if (!reader.IsDBNull(3))
                {
                    temp.OrderDate = reader.GetDateTime(3);
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4);
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate = reader.GetDateTime(5);
                }
                if (!reader.IsDBNull(6))
                {
                    temp.ShipVia = reader.GetInt32(6);
                }

                lists.Add(temp);
            }

            cnn.Close();

            return lists;
        }

        public List<Order> GetAllData4(DateTime from, DateTime to, int employee_id , String customer_id)
        {
            List<Order> lists = new List<Order>();
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select OrderID,Employees.FirstName+' '+Employees.LastName as 'Employee',Customers.CompanyName as 'Customer',Orders.OrderDate,Orders.RequiredDate,Orders.ShippedDate,Orders.ShipVia from Employees,Customers,Orders"
                      + " where"
                      + " Employees.EmployeeID = Orders.EmployeeID"
                      + " and"
                      + " Customers.CustomerID = Orders.CustomerID"
                      + " and"
                      + " Orders.OrderDate >= @val1 "
                      + " and"
                      + " Orders.RequiredDate <= @val2"
                      + " and"
                      + " Employees.EmployeeID = @val3"
                      + " and"
                      + " Customers.CustomerID = @val4"
                      ;
            SqlCommand command = new SqlCommand(query, cnn);
            command.Parameters.AddWithValue("@val1", from);
            command.Parameters.AddWithValue("@val2", to);
            command.Parameters.AddWithValue("@val3", employee_id);
            command.Parameters.AddWithValue("@val4", customer_id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order temp = new Order();
                if (!reader.IsDBNull(0))
                {
                    temp.OrderID = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(1))
                {
                    temp.Employee = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    temp.Customer = reader.GetString(2);
                }
                if (!reader.IsDBNull(3))
                {
                    temp.OrderDate = reader.GetDateTime(3);
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4);
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate = reader.GetDateTime(5);
                }
                if (!reader.IsDBNull(6))
                {
                    temp.ShipVia = reader.GetInt32(6);
                }

                lists.Add(temp);
            }

            cnn.Close();

            return lists;
        }
    }
}
