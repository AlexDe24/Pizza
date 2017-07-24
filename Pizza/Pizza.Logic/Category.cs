using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Logic
{
    public class Category
    {
        public int id { get; set; }

        public string ParentId { get; set; }

        [Required]
        public string name { get; set; }

        public List<Product> product { get; set; }
    }
}
