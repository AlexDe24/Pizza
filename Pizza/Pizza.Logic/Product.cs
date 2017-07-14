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
        public string category { get; set; }
        [Required]
        public double price { get; set; }

        public List<Order> order { get; set; }

        public Product()
        {
            order = new List<Order>();
        }
    }
}
