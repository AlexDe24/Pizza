using System.Data.Entity;

namespace Pizza.Logic
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("DbConnection")
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
