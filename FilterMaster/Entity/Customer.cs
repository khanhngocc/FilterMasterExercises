﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterMaster.Entity
{
    class Customer
    {
        private String id;
        private String name;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
