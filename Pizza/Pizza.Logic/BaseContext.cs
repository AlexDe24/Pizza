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
                //Категории
                string[] categoriesMain = Properties.Resources.CategoryMain.Split('.');
                
                List<Category> categoriesList = new List<Category>();

                for (int i = 0; i < categoriesMain.Length; i++)
                {
                    categoriesList.Add(new Category { Name = categoriesMain[i].Split('|')[0] });
                }

                for (int i = 0; i < categoriesMain.Length; i++)
                {
                    var categories = categoriesMain[i].Split('|');

                    //categoriesList.Add(new Category { name = categories[0] });

                    categories = categories[1].Split(',');

                    for (int j = 0; j < categories.Length; j++)
                    {
                        categoriesList.Add(new Category { Name = categories[j], parentCategory = categoriesList[i]});
                    }
                }

                foreach (Category category in categoriesList)
                    context.Category.Add(category);

                //Состояния
                string[] statusList = Properties.Resources.Status.Split(',');
                List<Status> statuses = new List<Status>();

                for (int i = 0; i < statusList.Length; i++)
                {
                    statuses.Add(new Status { name = statusList[i] });
                }

                foreach (Status status in statuses)
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

        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
