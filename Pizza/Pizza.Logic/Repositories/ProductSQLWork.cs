using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pizza.Logic.Repositories
{
    public class ProductSQLWork : IDisposable
    {
        BaseContext _BaseCt;

        public ProductSQLWork()
        {
            _BaseCt = new BaseContext();
        }

        public void Dispose()
        {
            _BaseCt.Dispose();
        }

        /// <summary>
        /// Редактирование продукта
        /// </summary>
        /// <param name="product">продукт</param>
        public void EditProduct(Product product)
        {
            Product ProductRedact = _BaseCt.Products.Where(c => c.Name == product.Name).FirstOrDefault();

            ProductRedact.Name = product.Name;
            ProductRedact.Price = product.Price;
            ProductRedact.Category = product.Category;

            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <param name="product">продукт</param>
        public void AddProduct(Product product)
        {
            _BaseCt.Products.Add(product);
            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка подуктов из базы данных
        /// </summary>
        /// <returns>список продуктов</returns>
        public List<Product> ReadProducts()
        {
            List<Product> products = _BaseCt.Products.Include(x => x.Category).Include(x => x.Category.ParentCategory).ToList();

            return products;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            await _BaseCt.Category.LoadAsync().ConfigureAwait(false);

            return await _BaseCt.Products.Include(x => x.Category).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Удаление продукта из базы
        /// </summary>
        /// <param name="product">продукт</param>
        public void DeleteProduct(Product product)
        {
            _BaseCt.Products.Remove(product);
            _BaseCt.SaveChanges();
        }

    }
}
