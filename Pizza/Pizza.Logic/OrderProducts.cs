using System.Collections.Generic;

namespace Pizza.Logic
{
    public class OrderProducts
    {
        public int id { get; set; }

        public int orderID { get; set; }
        public int productID { get; set; }

        public int countProducts { get; set; }

        public Product products { get; set; }
        public Order orders { get; set; }
    }
}
