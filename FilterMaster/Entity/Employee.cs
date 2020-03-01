using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterMaster.Entity
{
    class Employee
    {
        private int id;
        private String first_name;
        private String last_name;
      
       
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public int Id { get => id; set => id = value; }
    }
}
