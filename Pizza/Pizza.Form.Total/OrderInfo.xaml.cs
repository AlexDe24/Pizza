using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;

namespace Pizza.Form.Total
{
    /// <summary>
    /// Логика взаимодействия для OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        Order _order;
        List<Product> _products;
        ProductSQLWork _productSQLWork;

        public OrderInfo(Order order)
        {
            InitializeComponent();

            _order = order;
            _products = _productSQLWork.ReadProducts();

            LoadOrder();
        }

        void LoadOrder()
        {
            OrderListParam();

            for (int i = 0; i < _order.OrderProducts.Count; i++)
            {
                CreateProducts(_order.OrderProducts[i]);
            }

            decimal sum = 0;

            for (int i = 0; i < _order.OrderProducts.Count; i++)
            {
                sum += _order.OrderProducts[i].CountProducts * _products.Where(x => x.Id == _order.OrderProducts[i].ProductID).FirstOrDefault().Price;
            }

            Price.Text += sum;
            Discount.Text += _order.Discount;
            FullPrice.Text += sum - _order.Discount;
        }

        /// <summary>
        /// Добавление разметки в лист заказа
        /// </summary>
        void OrderListParam()
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            Label name = new Label()
            {
                Content = "Название",
                Width = 130
            };
            Label prise = new Label()
            {
                Content = "Цена",
                Width = 50
            };
            Label count = new Label()
            {
                Content = "Кол-во",
                Width = 50
            };
            Label fullPrise = new Label()
            {
                Content = "Сумма",
                Width = 50
            };

            panel.Children.Add(name);
            panel.Children.Add(prise);
            panel.Children.Add(count);
            panel.Children.Add(fullPrise);

            OrderList.Items.Add(panel);
        }

        /// <summary>
        /// Добавление продукта в заказ 
        /// </summary>
        /// <param name="chooseProduct"></param>
        void CreateProducts(OrderProducts orderProd)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            Label name = new Label()
            {
                Content = _products.Where(x => x.Id == orderProd.ProductID).FirstOrDefault().Name,
                Width = 130
            };
            Label prise = new Label()
            {
                Content = _products.Where(x => x.Id == orderProd.ProductID).FirstOrDefault().Price,
                Width = 50
            };
            Label count = new Label()
            {
                Content = orderProd.CountProducts,
                Width = 50
            };
            Label fullPrise = new Label()
            {
                Content = _products.Where(x => x.Id == orderProd.ProductID).FirstOrDefault().Price * orderProd.CountProducts,
                Width = 50
            };

            panel.Children.Add(name);
            panel.Children.Add(prise);
            panel.Children.Add(count);
            panel.Children.Add(fullPrise);

            OrderList.Items.Add(panel);
        }
    }
}
