using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterMaster.Entity
{
    class Product
    {
        private int id;
        private String name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
