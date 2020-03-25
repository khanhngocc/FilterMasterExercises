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


        public int GetMaxOrderID()
        {
            int num = 0;
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String query = "Select MAX(OrderID) from Orders";
                            

            SqlCommand command = new SqlCommand(query, cnn);
          
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                num = reader.GetInt32(0);
            }

            cnn.Close();

            return num;
        }


        public void InsertOrders(String customer_id,int employee_id,DateTime orderdate, int shipid)
        {
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String sql = "Insert into Orders (CustomerID,EmployeeID,OrderDate,ShipVia)"+
                         " values(@val1, @val2,@val3, @val4)";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@val1", customer_id);
            command.Parameters.AddWithValue("@val2", employee_id);
            command.Parameters.AddWithValue("@val3", orderdate);
            command.Parameters.AddWithValue("@val4",shipid);
            command.ExecuteNonQuery();
        }
        public void InsertOrdersDetails(int order_id,int product_id,Decimal unit_price,int quantity)
        {
            SqlConnection cnn = dBContext.GetConnection();
            cnn.Open();
            String sql = "Insert into [Order Details] (OrderID,ProductID,UnitPrice,Quantity)"+
                    " values(@val1, @val2, @val3, @val4)";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@val1", order_id);
            command.Parameters.AddWithValue("@val2", product_id);
            command.Parameters.AddWithValue("@val3", unit_price);
            command.Parameters.AddWithValue("@val4", quantity);
           
            command.ExecuteNonQuery();
        }
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
                      + " Orders.OrderDate BETWEEN @val1 AND @val2"
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
                    temp.OrderDate = reader.GetDateTime(3).Date;
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4).Date;
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate =  reader.GetDateTime(5).Date;
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
                      + " Orders.OrderDate BETWEEN @val1 AND @val2"  
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
                    temp.OrderDate = reader.GetDateTime(3).Date;
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4).Date;
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate = reader.GetDateTime(5).Date;
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
                      + " Orders.OrderDate BETWEEN @val1 AND @val2"
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
                    temp.OrderDate = reader.GetDateTime(3).Date;
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4).Date;
                }
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate = reader.GetDateTime(5).Date;
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
                      + " Orders.OrderDate BETWEEN @val1 AND @val2"
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
                    temp.OrderDate = reader.GetDateTime(3).Date;
                }
                if (!reader.IsDBNull(4))
                {
                    temp.RequiredDate = reader.GetDateTime(4).Date;
                }
                
                if (!reader.IsDBNull(5))
                {
                    temp.ShippedDate = reader.GetDateTime(5).Date;
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
