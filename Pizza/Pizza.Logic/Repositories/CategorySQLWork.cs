using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pizza.Logic.Repositories
{
    public class CategorySQLWork : IDisposable
    {
        BaseContext _BaseCt;

        public CategorySQLWork()
        {
            _BaseCt = new BaseContext();
        }

        public void Dispose()
        {
            _BaseCt.Dispose();
        }

        /// <summary>
        /// Чтение списка категорий
        /// </summary>
        public List<Category> ReadCategory()
        {
                List<Category> category = _BaseCt.Category.Include(x => x.Product).ToList();
            return category;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _BaseCt.Category.ToListAsync().ConfigureAwait(false);
        }
    }
}
