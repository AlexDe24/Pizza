using System.Data.Entity;

namespace Pizza.Logic
{
    public class BaseContext : DbContext
    {
        public BaseContext() : base("DbConnection")
        {
 
        }

        //public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
