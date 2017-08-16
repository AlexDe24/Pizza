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
            return _BaseCt.Products.Include(x => x.Category).Include(x => x.Category.ParentCategory).ToList();
        }

        /// <summary>
        /// Чтение списка подуктов из базы данных асинхронно
        /// </summary>
        /// <returns>список продуктов</returns>
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _BaseCt.Products.Include(x => x.Category.ParentCategory).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Чтение подукта из базы данных
        /// </summary>
        /// <returns>список продуктов</returns>
        public Product GetOneProducts(int Id)
        {
            return _BaseCt.Products.Include(x => x.Category.ParentCategory).Single(x => x.Id == Id);
        }

        /// <summary>
        /// Удаление продукта из базы
        /// </summary>
        /// <param name="product">продукт</param>
        public void DeleteProduct(Product product)
        {
            _BaseCt.Products.Load();

            _BaseCt.Products.Remove(_BaseCt.Products.Where(x => x.Id == product.Id).FirstOrDefault());
            _BaseCt.SaveChanges();
        }

    }
}
