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

        public DateTime date { get; set; }

        public string address { get; set; }
        public string phone { get; set; }
        public decimal discount { get; set; }

        public Status status { get; set; }
        public Client client { get; set; }
        public List<Product> products { get; set; }
        //public List<OrderProducts> orderProducts { get; set; }

        public Order()
        {
            status = new Status();
            client = new Client();
            products = new List<Product>();
        }
    }
}
