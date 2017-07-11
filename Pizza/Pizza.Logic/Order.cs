using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class Order
    {
        public int id { get; set; }

        public double fullPrice { get; set; }
        public DateTime date { get; set; }

        public string condition { get; set; } //состояние

        public Client client;
        public List<Product> products;

        public Order()
        {
            client = new Client();
            products = new List<Product>();
        }
    }
}
