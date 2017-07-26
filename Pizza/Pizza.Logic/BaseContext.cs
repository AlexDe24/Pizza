using Pizza.Logic.DTO;
using System.Collections.Generic;
using System.Data.Entity;

namespace Pizza.Logic
{
    public class BaseContext : DbContext
    {
        public class SampleInitializer
       : DropCreateDatabaseIfModelChanges<BaseContext>
        {
            // В этом методе можно заполнить таблицу по умолчанию
            protected override void Seed(BaseContext context)
            {
                ResourcesParse resourseParse = new ResourcesParse();

                var categoriesList = resourseParse.CategoryParse();
                foreach (Category category in categoriesList)
                    context.Category.Add(category);

                var statusList = resourseParse.StatusParse();
                foreach (Status status in statusList)
                    context.Statuses.Add(status);

                context.SaveChanges();
                base.Seed(context);
            }
        }

        public BaseContext() : base("DbConnection")
        {
            /*Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<BaseContext>());*/

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
