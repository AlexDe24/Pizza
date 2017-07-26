using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Logic
{
    public class Category
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        public Category parentCategory { get; set; }
        public ObservableCollection<Category> childCategory { get; set; }
        public ObservableCollection<Product> product { get; set; }

        public Category()
        {
            childCategory = new ObservableCollection<Category>();
        }
    }
}
