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

        Order _order;//класс заказа
        public List<Product> Products { get; set; }

        #endregion

        internal MenuViewModel()
        {
            DisplayName = "Меню";

            MainCategory = new List<Category>();
            OrderItems = new BindableCollection<OrderElementViewModel>();

            CatTreeView = new TreeView();

            _order = new Order()
            {
                OrderProducts = new List<OrderProducts>()
            };

            DataLoad().Wait();
        }

        private async Task DataLoad()
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

        #region MenuClick

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

        #region UI Commands

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
        }

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
        void AddProduct(Product chooseProduct)
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

        public void HandleAddProductClick(TreeView CategoryTreeView)
        {
            /*if (Find.SelectedItem != null)
            {
                Product chooseProduct = _products.Where(x => x.Name == Convert.ToString(Find.SelectedItem as string)).FirstOrDefault();
                if (chooseProduct != null)
                    if (_order.OrderProducts.Any(x => x.ProductID == chooseProduct.Id))
                        AddProduct(chooseProduct);
                    else
                        CreateProduct(chooseProduct);
            }*/

            if (CategoryTreeView.SelectedItem is Product chooseProduct)
            {
                if (_order.OrderProducts.Any(x => x.ProductID == chooseProduct.Id))
                {
                    AddProduct(chooseProduct);
                }
                else
                    CreateProduct(chooseProduct);
            }
        }

        public void HandleCreateOrder()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowDialog(new OrderFinishingViewModel() {Order = _order, OrderSum = OrderItemsSum });
            });

            HandleClearOrderList();
        }
        #endregion
    }
}
