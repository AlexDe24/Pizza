using System;
using System.Collections.Generic;

namespace Pizza.Logic
{
    public class Order
    {
        public int id { get; set; }
        public int nom { get; set; }

        public DateTime date { get; set; }

        public string address { get; set; }
        public string phone { get; set; }

        public decimal discount { get; set; }

        public int clientId { get; set; }

        public Status status { get; set; }
        public List<OrderProducts> orderProducts { get; set; }

        public Order()
        {
            status = new Status();
        }
    }
}
