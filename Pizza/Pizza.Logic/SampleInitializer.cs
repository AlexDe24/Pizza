using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class SampleInitializer : DropCreateDatabaseIfModelChanges<BaseContext>
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
}
