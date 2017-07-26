using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Pizza.Logic.DTO
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public List<OrderProducts> OrderProducts { get; set; }

        public Product()
        {
            Category = new Category();
        }
    }

    internal class ProductETC : EntityTypeConfiguration<Product>
    {
        public ProductETC()
        {
            HasRequired(x => x.Category)
                .WithMany(x => x.Product)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);
        }
    }
}
