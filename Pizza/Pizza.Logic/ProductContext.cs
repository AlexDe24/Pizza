using System.Data.Entity;

namespace Pizza.Logic
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("DbConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
