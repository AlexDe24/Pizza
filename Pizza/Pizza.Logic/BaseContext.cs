using Pizza.Logic.DTO;
using System.Collections.Generic;
using System.Data.Entity;

namespace Pizza.Logic
{
    public class BaseContext : DbContext
    {
        public BaseContext() : base("DbConnection")
        {
            Database.SetInitializer(new SampleInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OrderETC());
            modelBuilder.Configurations.Add(new ClientETC());

            modelBuilder.Configurations.Add(new ProductETC());
            modelBuilder.Configurations.Add(new CategoryETC());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
