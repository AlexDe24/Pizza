using System.Data.Entity;

namespace Pizza.Logic
{
    public class ClientContext : DbContext
    {
        public ClientContext() : base("DbConnection")
        {

        }

        public DbSet<Client> Clients { get; set; }
    }
}
