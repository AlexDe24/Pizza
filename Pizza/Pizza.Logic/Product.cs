using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }

        public List<Order> order { get; set; }
    }
}
