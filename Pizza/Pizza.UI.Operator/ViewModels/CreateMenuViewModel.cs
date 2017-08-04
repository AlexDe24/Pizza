using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Operator.ViewModels
{
    class CreateMenuViewModel : Screen
    {
        #region Properties

        public Category SelectedMainCategory { get; set; }
        public Category SelectedCategory { get; set; }

        private List<Category> _mainCategories;
        public List<Category> MainCategories
        {
            get
            {
                return _mainCategories.Where(x => x.ParentCategory == null).ToList();
            }
            set
            {
                if (value != _mainCategories)
                {
                    _mainCategories = value;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(() => Categories);

                }
            }
        }
        public List<Category> Categories
        {
            get
            {
                return _mainCategories.Where(x => x.ParentCategory == SelectedMainCategory).ToList();
            }
            set
            {
                if (value != _mainCategories)
                {
                    _mainCategories = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                if (value != _products)
                {
                    _products = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        #endregion

        public async Task Load()
        {
            using (var ProductSQLWork = new ProductSQLWork())
            {
                Products = await ProductSQLWork.GetProductsAsync().ConfigureAwait(false);
            }

            using (var CategorySQLWork = new CategorySQLWork())
            {
                MainCategories = await CategorySQLWork.GetCategoriesAsync().ConfigureAwait(false);
            }
        }

    }
}
