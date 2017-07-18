using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class Category
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        public List<Product> product { get; set; }
    }
}
