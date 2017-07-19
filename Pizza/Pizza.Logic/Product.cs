using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public decimal price { get; set; }

        public Category category { get; set; }
        public List<Order> order { get; set; }
        //public List<OrderProducts> orderProducts { get; set; }

        public Product()
        {
            category = new Category();
            order = new List<Order>();
        }
    }
}
