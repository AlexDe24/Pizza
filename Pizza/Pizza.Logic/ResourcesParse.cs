using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class ResourcesParse
    {
        public List<Category> CategoryParse()
        {
            string[] categoriesMain = Properties.Resources.CategoryMain.Split('.');

            List<Category> categoriesList = new List<Category>();

            for (int i = 0; i < categoriesMain.Length; i++)
            {
                categoriesList.Add(new Category { Name = categoriesMain[i].Split('|')[0] });
            }

            for (int i = 0; i < categoriesMain.Length; i++)
            {
                var categories = categoriesMain[i].Split('|');

                categories = categories[1].Split(',');

                for (int j = 0; j < categories.Length; j++)
                {
                    categoriesList.Add(new Category { Name = categories[j], ParentCategory = categoriesList[i] });
                }
            }

            return categoriesList;
        }

        public List<Status> StatusParse()
        {
            string[] statuses = Properties.Resources.Status.Split(',');
            List<Status> statusList = new List<Status>();

            for (int i = 0; i < statuses.Length; i++)
            {
                statusList.Add(new Status { Name = statuses[i] });
            }

            return statusList;
        }
    }
}
