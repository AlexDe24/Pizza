using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pizza.Logic;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        Order _order;
        List<Product> _products;

        public OrderInfo(Order order, FileClass _fileWork)
        {
            InitializeComponent();

            _order = order;
            _products = _fileWork.ReadProducts();

            LoadOrder();
        }

        void LoadOrder()
        {
            OrderListParam();

            for (int i = 0; i < _order.orderProducts.Count; i++)
            {
                CreateProducts(_order.orderProducts[i]);
            }

            decimal sum = 0;

            for (int i = 0; i < _order.orderProducts.Count; i++)
            {
                sum += _order.orderProducts[i].countProducts * _products.Where(x => x.id == _order.orderProducts[i].productID).FirstOrDefault().price;
            }

            Price.Text += sum;
            Discount.Text += _order.discount;
            FullPrice.Text += sum - _order.discount;
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
                Content = _products.Where(x => x.id == orderProd.productID).FirstOrDefault().name,
                Width = 130
            };
            Label prise = new Label()
            {
                Content = _products.Where(x => x.id == orderProd.productID).FirstOrDefault().price,
                Width = 50
            };
            Label count = new Label()
            {
                Content = orderProd.countProducts,
                Width = 50
            };
            Label fullPrise = new Label()
            {
                Content = _products.Where(x => x.id == orderProd.productID).FirstOrDefault().price * orderProd.countProducts,
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
