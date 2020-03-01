using FilterMaster.DAO;
using FilterMaster.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FilterMaster
{
    public partial class FilterMaster : Form
    {
        OrderDAO orderDAO = new OrderDAO();
        EmployeeDAO employeeDAO = new EmployeeDAO();
        CustomerDAO customerDAO = new CustomerDAO();
        public FilterMaster()
        {
            InitializeComponent();
            LoadDataCombobox();
            CheckboxItemDefault();
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



        }


        private void checkedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // dataGridView1.Columns["Id"].Visible = false;
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
                if(dataGridView1.Rows.Count != 0)
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
    }
}
