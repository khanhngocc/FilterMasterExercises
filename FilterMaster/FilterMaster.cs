using FilterMaster.DAO;
using FilterMaster.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FilterMaster
{
    public partial class FilterMaster : Form
    {
        int X = 0;
        int Y = 0;
        int X1 = 200;
        List<int> list_id_product = new List<int>();
        OrderDAO orderDAO = new OrderDAO();
        EmployeeDAO employeeDAO = new EmployeeDAO();
        CustomerDAO customerDAO = new CustomerDAO();
        ProductDAO productDAO = new ProductDAO();
        ShipperDAO ShipperDAO = new ShipperDAO();
        public FilterMaster()
        {
            InitializeComponent();
            LoadDataCombobox();
            CheckboxItemDefault();
            LoadDataListBox();
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
           
        }


        public void LoadDataListBox()

        {
           
            listBox.DataSource = null;
            listBox.Items.Clear();
            listBox.DataSource = new BindingSource(productDAO.GetAllProducts(),null);
            listBox.DisplayMember = "Value";
            listBox.ValueMember = "Key";
            listBox.SelectedIndex = -1;
        
        }

        public void LoadDataCombobox()
        {
            // load for employee
            List<KeyValuePair<int, String>> data_employee = new List<KeyValuePair<int, String>>();
            List<Employee> list_employee = employeeDAO.GetAllEmployees();
            data_employee.Add(new KeyValuePair<int, String>(0, "All"));
            foreach (Employee e in list_employee)
            {
                String name = e.First_name + " " + e.Last_name;
                data_employee.Add(new KeyValuePair<int, String>(e.Id, name));
            }
            // Clear the combobox
            comboBoxEmployee.DataSource = null;
            comboBoxEmployee.Items.Clear();
            // Bind the combobox
            comboBoxEmployee.DataSource = new BindingSource(data_employee, null);
            comboBoxEmployee.DisplayMember = "Value";
            comboBoxEmployee.ValueMember = "Key";
            comboBoxEmployee.SelectedIndex = 0;
            
           
           
            // load for customer
            List<Customer> list_customer = customerDAO.GetAllCustomers();
            List<KeyValuePair<String, String>> data_customer = new List<KeyValuePair<String, String>>();
            data_customer.Add(new KeyValuePair<String, String>("All", "All"));
            foreach (Customer c in list_customer)
            {
                data_customer.Add(new KeyValuePair<String, String>(c.Id, c.Name));
            }
            // Clear the combobox
            comboBoxCustomer.DataSource = null;
            comboBoxCustomer.Items.Clear();
            // Bind the combobox
            comboBoxCustomer.DataSource = new BindingSource(data_customer, null);
            comboBoxCustomer.DisplayMember = "Value";
            comboBoxCustomer.ValueMember = "Key";
            comboBoxCustomer.SelectedIndex = 0;

            // load data shipper
            comboBox_ShipVia.DataSource = null;
            comboBox_ShipVia.Items.Clear();
            comboBox_ShipVia.DataSource = new BindingSource(ShipperDAO.GetAllShipper(), null);
            comboBox_ShipVia.DisplayMember = "Value";
            comboBox_ShipVia.ValueMember = "Key";
            comboBox_ShipVia.SelectedIndex = 0;

        }


        private void checkedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            
            String checkBoxName = (String) checkedList.Items[e.Index];
            
            // find checkbox "All"
            if (checkedList.Items.Count - 1 == e.Index)
               {
                if (checkedList.GetItemChecked(e.Index))
                {
                    ClearCheckboxItems();
                }
                else
                {
                    ClearCheckboxItems();
                    CheckboxAllItems();
                }
                    
               }
            else
            {
               
                if (dataGridView1.Rows.Count != 0)
                {
                    if (checkedList.GetItemChecked(e.Index))
                    {

                        dataGridView1.Columns[checkBoxName].Visible = false;
                      

                    }
                    else
                    {
                        dataGridView1.Columns[checkBoxName].Visible = true;
                       
                    }
                }

                
            }



        }
        
        public void CheckboxItemDefault()
        {
            for (int i = 0; i < 4; i++)
            {
                checkedList.SetItemChecked(i, true);
            }
        }

        public void ClearCheckboxItems()
        {
            for (int i = 0; i < checkedList.Items.Count - 1; i++)
            { 
                checkedList.SetItemChecked(i, false);
            }
        }

        public void CheckboxAllItems()
        {
            for (int i = 0; i < checkedList.Items.Count - 1; i++)
            {
                checkedList.SetItemChecked(i, true);
            }
        }

        public DateTime ConvertDateSQL(DateTimePicker dateTimePicker)
        {
            String date= dateTimePicker.Value.Date.ToString("yyyy-MM-dd");
            DateTime myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                   System.Globalization.CultureInfo.InvariantCulture);
            return myDate;
        }

        public void HideColumnFollowCheckBox(DataGridView dataGridView)
        {
            for (int i = 0; i < checkedList.Items.Count - 1; i++)
            {
                if(checkedList.GetItemChecked(i) == false)
                {
                    dataGridView.Columns[i].Visible = false;
                }
            }

           
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            KeyValuePair<int, String> selectedPair_employee = (KeyValuePair<int, String>)comboBoxEmployee.SelectedItem;
         
            KeyValuePair<String, String> selectedPair_customer = (KeyValuePair<String, String>)comboBoxCustomer.SelectedItem;
            // case 1 : Employee : All ; Customer : All
            if (selectedPair_employee.Value.Equals("All") && selectedPair_customer.Value.Equals("All"))
            {
                dataGridView1.DataSource = null;
                DateTime myDate_from = ConvertDateSQL(dateFrom);
                DateTime myDate_to = ConvertDateSQL(dateTo);
                dataGridView1.DataSource = orderDAO.GetAllData1(myDate_from,myDate_to);
                HideColumnFollowCheckBox(dataGridView1);
             
            } 
            else if(!selectedPair_employee.Value.Equals("All") && selectedPair_customer.Value.Equals("All")) // case 2 : Employee : Specific ; Customer : All
            {
                dataGridView1.DataSource = null;
                DateTime myDate_from = ConvertDateSQL(dateFrom);
                DateTime myDate_to = ConvertDateSQL(dateTo);
                int employee_id = selectedPair_employee.Key;
                dataGridView1.DataSource = orderDAO.GetAllData2(myDate_from, myDate_to, employee_id);
                HideColumnFollowCheckBox(dataGridView1);
            }
            else if(selectedPair_employee.Value.Equals("All") && !selectedPair_customer.Value.Equals("All")) // case 3 : Employee : All ; Customer : Specific
            {
                dataGridView1.DataSource = null;
                DateTime myDate_from = ConvertDateSQL(dateFrom);
                DateTime myDate_to = ConvertDateSQL(dateTo);
                String customer_id = selectedPair_customer.Key;
                dataGridView1.DataSource = orderDAO.GetAllData3(myDate_from, myDate_to, customer_id);
                HideColumnFollowCheckBox(dataGridView1);
            }
            else // case 4 : Employee : Specific ; Customer : Specific
            {
                dataGridView1.DataSource = null;
                DateTime myDate_from = ConvertDateSQL(dateFrom);
                DateTime myDate_to = ConvertDateSQL(dateTo);
                int employee_id = selectedPair_employee.Key;
                String customer_id = selectedPair_customer.Key;
                dataGridView1.DataSource = orderDAO.GetAllData4(myDate_from, myDate_to, employee_id ,customer_id);
                HideColumnFollowCheckBox(dataGridView1);
            }

        }

        private void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            //int[] indexes = listBox.SelectedIndices.Cast<int>().ToArray();

            if (listBox.SelectedIndex == -1)
                return;

            KeyValuePair<int, String> selectedPair_product = (KeyValuePair<int, String>)listBox.SelectedItem;
            //MessageBox.Show(selectedPair_product.Key + "");
            if(selectedPair_product.Value.Equals("All"))
            {
                
                
                    panel1.Controls.Clear();
                    Y = 0;

                    List<KeyValuePair<int, String>> data_product = productDAO.GetAllProducts();
                    foreach (KeyValuePair<int, String> a in data_product)
                    {
                        if (!a.Value.Equals("All"))
                        {

                            Label temp_label = new Label();
                            temp_label.Text = a.Value;
                            temp_label.Location = new Point(X, Y);
                            NumericUpDown temp_numeric = new NumericUpDown();
                            temp_numeric.Minimum = 1;
                            temp_numeric.Value = 1;
                            temp_numeric.ReadOnly = true;
                            temp_numeric.Location = new Point(X1, Y);
                            list_id_product.Add(a.Key);
                            panel1.Controls.Add(temp_label);
                            panel1.Controls.Add(temp_numeric);
                            Y += 40;

                        }

                    }
                
            }
            else
            {
                if(IsDuplicatedProductName(selectedPair_product.Value) == false)
                {
                    Label temp_label = new Label();
                    temp_label.Text = selectedPair_product.Value;
                    temp_label.Location = new Point(X, Y);
                    temp_label.AutoSize = true;
                    NumericUpDown temp_numeric = new NumericUpDown();
                    temp_numeric.Minimum = 1;
                    temp_numeric.Value = 1;
                    temp_numeric.ReadOnly = true;
                    temp_numeric.Location = new Point(X1, Y);
                    list_id_product.Add(selectedPair_product.Key);
                    panel1.Controls.Add(temp_label);
                    panel1.Controls.Add(temp_numeric);
                    Y += 40;
                }
               
               
            }
            
        }

        public bool IsDuplicatedProductName(String name)
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (panel1.Controls[i] is Label)
                {
                    if (name.Equals(panel1.Controls[i].Text))
                        return true;
                }
            }

            return false;
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Y = 0;
            comboBoxCustomer.SelectedIndex = 0;
            comboBoxEmployee.SelectedIndex = 0;
            comboBox_ShipVia.SelectedIndex = 0;
            listBox.SelectedIndex = -1;
           
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            KeyValuePair<int, String> selectedPair_shipvia = (KeyValuePair<int, String>)comboBox_ShipVia.SelectedItem;
            KeyValuePair<int, String> selectedPair_employee = (KeyValuePair<int, String>)comboBoxEmployee.SelectedItem;
            KeyValuePair<String, String> selectedPair_customer = (KeyValuePair<String, String>)comboBoxCustomer.SelectedItem;
            DateTime nowdateTime = DateTime.Now;
            String date = nowdateTime.Date.ToString("yyyy-MM-dd");
            DateTime myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                   System.Globalization.CultureInfo.InvariantCulture);
            int product_id_index = 0;
            
            for (int i = 0; i < panel1.Controls.Count; i+=2 )
            {
                String customer_id = selectedPair_customer.Key;
                int employee_id = selectedPair_employee.Key;
                int shipvia_id = selectedPair_shipvia.Key;
                int product_id = list_id_product.ElementAt(product_id_index);
                Decimal unit_price = productDAO.GetUnitPrice(product_id);
                int quantiy = Int32.Parse(panel1.Controls[i+1].Text);
                orderDAO.InsertOrders(customer_id,employee_id,myDate,shipvia_id);
                int order_id = orderDAO.GetMaxOrderID();
                orderDAO.InsertOrdersDetails(order_id,product_id, unit_price, quantiy);
                product_id_index++;
            }
           
            MessageBox.Show("Add Order successfully");
        }
    }
}
