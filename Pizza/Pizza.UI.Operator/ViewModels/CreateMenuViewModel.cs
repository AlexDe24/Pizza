using Caliburn.Micro;
using NLog;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza.UI.Operator.ViewModels
{
    internal class CreateMenuViewModel : Screen
    {

        private readonly ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        #region Properties

        public string Name { get; set; }
        public decimal Price { get; set; }

        public Category _selectedMainCategory;
        public Category SelectedMainCategory
        {
            get
            {
                return _selectedMainCategory;
            }
            set
            {
                if (value != _selectedMainCategory)
                {
                    _selectedMainCategory = value;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(() => Categories);
                }
            }
        }

        public Category SelectedCategory { get; set; }

        private List<Category> _allCategories;
        public List<Category> MainCategories
        {
            get
            {
                return _allCategories.Where(x => x.ParentCategory == null).ToList();
            }
            set
            {
                if (value != _allCategories)
                {
                    _allCategories = value;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(() => Categories);
                }
            }
        }
        public List<Category> Categories
        {
            get
            {
                if (SelectedMainCategory != null)
                    return _allCategories.Where(x => x.ParentCategory == SelectedMainCategory).ToList();
                else
                    return null;
            }
            set
            {
                if (value != _allCategories)
                {
                    _allCategories = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public ObservableCollection<Product> Products { get; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                if (value != _selectedProduct)
                {
                    _selectedProduct = value;
                    NotifyOfPropertyChange();

                    if (_selectedProduct != null)
                    {
                        Name = _selectedProduct.Name;
                        Price = _selectedProduct.Price;

                        SelectedMainCategory = MainCategories.Single(x => x.Id == _selectedProduct.Category.ParentCategory.Id);
                        SelectedCategory = Categories.Single(x => x.Id == _selectedProduct.Category.Id);

                        NotifyOfPropertyChange(() => Name);
                        NotifyOfPropertyChange(() => Price);

                        NotifyOfPropertyChange(() => SelectedMainCategory);
                        NotifyOfPropertyChange(() => SelectedCategory);
                    }
                }
            }
        }

        #endregion

        #region Constructor

        internal CreateMenuViewModel()
        {
            DisplayName = "Составление меню";

            Products = new ObservableCollection<Product>();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Загрузка данных из базы
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            using (var ProductSQLWork = new ProductSQLWork())
            {
                var products = await ProductSQLWork.GetProductsAsync().ConfigureAwait(false);
                products.ForEach(x => Products.Add(x));
            }

            using (var CategorySQLWork = new CategorySQLWork())
            {
                MainCategories = await CategorySQLWork.GetCategoriesAsync().ConfigureAwait(false);
            }
        }

        #endregion

        #region UI Commands

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <returns></returns>
        public void HandleProductAddClick()
        {
            try
            {
                Product product = new Product()
                {
                    Name = Name,
                    Price = Price,
                    CategoryId = Categories.Single(x => x.Id == SelectedCategory.Id).Id
                };

                using (var productSQLWork = new ProductSQLWork())
                {
                    productSQLWork.AddProduct(product);

                    Products.Add(productSQLWork.GetOneProducts(product.Id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.RequiredParameters, "Внимание!");
                _logger.Error(ex, "gagasasg");
            }
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <returns></returns>
        public void HandleProductDelClick()
        {
            using (var productSQLWork = new ProductSQLWork())
            {
                productSQLWork.DeleteProduct(SelectedProduct);
            }

            Products.Remove(SelectedProduct);
        }

        #endregion
    }
}
