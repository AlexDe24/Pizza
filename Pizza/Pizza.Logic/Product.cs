using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public List<OrderProducts> orderProducts { get; set; }

        public Product()
        {
            category = new Category();
        }
    }
}
