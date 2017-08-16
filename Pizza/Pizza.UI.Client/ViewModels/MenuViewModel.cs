using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pizza.UI.Client.ViewModels
{
    internal class MenuViewModel : Screen
    {
        #region Properties

        public string _nameFilter;
        public string NameFilter
        {
            get
            {
                return _nameFilter;
            }
            set
            {
                if (value != _nameFilter)
                {
                    _nameFilter = value;
                    NotifyOfPropertyChange(() => Products);
                }
            }
        }

        public string _mainCategoryFilter;
        public string MainCategoryFilter
        {
            get
            {
                return _mainCategoryFilter;
            }
            set
            {
                if (value != _mainCategoryFilter)
                {
                    _mainCategoryFilter = value;
                    NotifyOfPropertyChange(() => Products);
                }
            }
        }

        public string _categoryFilter;
        public string CategoryFilter
        {
            get
            {
                return _categoryFilter;
            }
            set
            {
                if (value != _categoryFilter)
                {
                    _categoryFilter = value;
                    NotifyOfPropertyChange(() => Products);
                }
            }
        }

        public string _priceFilter;
        public string PriceFilter
        {
            get
            {
                return _priceFilter;
            }
            set
            {
                if (value != _priceFilter)
                {
                    _priceFilter = value;
                    NotifyOfPropertyChange(() => Products);
                }
            }
        }

        public TreeView CatTreeView { get; set; }

        private BindableCollection<OrderElementViewModel> _orderItems;
        public BindableCollection<OrderElementViewModel> OrderItems
        {
            get
            {
                return _orderItems;
            }
            set
            {
                if (value != _orderItems)
                {
                    _orderItems = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public decimal OrderItemsSum => OrderItems.Sum(x => x.ProductFullPrice);

        public List<Category> MainCategory { get; set; }

        private Order _order;//класс заказа

        public List<Product> _products;
        public List<Product> Products
        {
            get
            {
                return _products.Where(x => x.Name.ToLowerInvariant().Contains(NameFilter.ToLowerInvariant())
                && x.Category.ParentCategory.Name.ToLowerInvariant().Contains(MainCategoryFilter.ToLowerInvariant())
                && x.Category.Name.ToLowerInvariant().Contains(CategoryFilter.ToLowerInvariant())
                && x.Price.ToString().ToLowerInvariant().Contains(PriceFilter.ToLowerInvariant()))
                .ToList();
            }
            set
            {
                if (value != _products)
                {
                    _products = value;
                }
            }
        }

        public Product _selectedProduct;
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
                }
            }
        }

        #endregion

        #region Initialization

        internal MenuViewModel()
        {
            DisplayName = "Меню";

            NameFilter = "";
            MainCategoryFilter = "";
            CategoryFilter = "";
            PriceFilter = "";

            MainCategory = new List<Category>();
            OrderItems = new BindableCollection<OrderElementViewModel>();

            CatTreeView = new TreeView();

            _order = new Order()
            {
                OrderProducts = new List<OrderProducts>()
            };
        }

        public async Task DataLoad()
        {
            List<Category> category;

            using (var productSQLWork = new ProductSQLWork())
            {
                Products = await productSQLWork.GetProductsAsync().ConfigureAwait(false);
            }

            using (var categorySQLWork = new CategorySQLWork())
            {
                category = await categorySQLWork.GetCategoriesAsync().ConfigureAwait(false);
            }

            for (int i = 0; i < category.Count; i++)
            {
                if (category[i].ParentCategory == null)
                    MainCategory.Add(category[i]);
            }

        }

        #endregion

        #region UI Commands

        #region Menu

        public void HandleProfileClick()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowDialog(new ClientViewModel());
            });
        }

        public void HandleLogOutClick()
        {
            ClientIdentitySingleton.Instance.CurrentClient = null;
            TryClose();
        }

        public void HandleExitClick()
        {
            Application.Current.Shutdown();
            //Application.Shutdown();
        }

        #endregion

        /// <summary>
        /// Создание экзмепляра продукта
        /// </summary>
        /// <param name="chooseProduct">выбранный продукт</param>
        /// <returns></returns>
        private OrderProducts CeateOrderProducts(Product chooseProduct)
        {
            OrderProducts orderProd = new OrderProducts
            {
                OrderID = _order.Id,
                ProductID = chooseProduct.Id,
                CountProducts = 1
            };

            return orderProd;
        }

        /// <summary>
        /// Отчистка заказа
        /// </summary>
        public void HandleClearOrderList()
        {
            for (int i = 0; i < OrderItems.Count; i++)
            {
                OrderItems[i].PropertyChanged -= OrderItem_PropertyChanged;
            }

            _order.OrderProducts.Clear();
            OrderItems.Clear();
        }

        /// <summary>
        /// Добавление продукта в заказ 
        /// </summary>
        /// <param name="chooseProduct"></param>
        void CreateProduct(Product chooseProduct)
        {
            OrderElementViewModel orderItem = new OrderElementViewModel { ProductName = chooseProduct.Name, ProductPrice = chooseProduct.Price };
            orderItem.PropertyChanged += OrderItem_PropertyChanged;
            orderItem.ProductFullPrice = chooseProduct.Price;

            OrderItems.Add(orderItem);

            _order.OrderProducts.Add(CeateOrderProducts(chooseProduct));

            NotifyOfPropertyChange(() => OrderItemsSum);
        }

        /// <summary>
        /// Функция обновления значения для OrderItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Quantity")
            {
                for (int i = 0; i < OrderItems.Count; i++)
                {
                    if (OrderItems[i].Quantity == -1)
                    {
                        OrderItems[i].PropertyChanged -= OrderItem_PropertyChanged;

                        _order.OrderProducts.RemoveAll(x => x.ProductID == Products.Where(y => y.Name == OrderItems[i].ProductName).FirstOrDefault().Id);

                        OrderItems.RemoveAt(i);
                    }
                }
                NotifyOfPropertyChange(() => OrderItemsSum);
            }
        }

        /// <summary>
        /// Добавление продукта в заказ к имеющимся
        /// </summary>
        /// <param name="chooseProduct"></param>
        private void AddProduct(Product chooseProduct)
        {
            for (int i = 0; i < OrderItems.Count; i++)
            {
                if (OrderItems[i].ProductName == chooseProduct.Name)
                {
                    OrderItems[i].Quantity++;

                    for (int j = 0; j < _order.OrderProducts.Count; j++)
                    {
                        if (_order.OrderProducts[j].ProductID == chooseProduct.Id)
                            _order.OrderProducts[j].CountProducts++;
                    }
                }
            }
        }

        /// <summary>
        /// Добавление продукта в заказ
        /// </summary>
        /// <param name="CategoryTreeView"></param>
        public void HandleAddProductClick(TabControl ProductTabControl)
        {
            if (SelectedProduct != null)
            {
                if (_order.OrderProducts.Any(x => x.ProductID == SelectedProduct.Id))
                {
                    AddProduct(SelectedProduct);
                }
                else
                    CreateProduct(SelectedProduct);
            }
        }

        /// <summary>
        /// Завершнение заказа
        /// </summary>
        public void HandleCreateOrderClick()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowDialog(new OrderFinishingViewModel() {Order = _order, OrderSum = OrderItemsSum });
            });

            HandleClearOrderList();
        }

        public void HandleTreeViewSelectedItemChanged(RoutedPropertyChangedEventArgs<object> args)
        {
            SelectedProduct = args.NewValue as Product;
        }
        #endregion
    }
}
