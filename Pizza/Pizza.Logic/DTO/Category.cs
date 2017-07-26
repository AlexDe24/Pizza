using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Pizza.Logic.DTO
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Category ParentCategory { get; set; }
        public ObservableCollection<Category> ChildCategory { get; set; }
        public ObservableCollection<Product> Product { get; set; }

        public Category()
        {
            ChildCategory = new ObservableCollection<Category>();
        }
    }

    internal class CategoryETC : EntityTypeConfiguration<Category>
    {
        public CategoryETC()
        {
            HasMany(x => x.Product)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);
        }
    }
}
