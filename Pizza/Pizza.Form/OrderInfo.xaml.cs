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

        public OrderInfo(Order order)
        {
            InitializeComponent();

            _order = order;

            OrderListParam();
            LoadOrder();
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
                Width = 110
            };
            Label prise = new Label()
            {
                Content = "Цена",
                Width = 50
            };
            Label count = new Label()
            {
                Content = "Кол-во",
                Width = 30
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

        void LoadOrder()
        {
            for (int i = 0; i < _order.products.Count; i++)
            {
                if (_order.products.Any(x => x.name == OrderList.Items[i]))
                    AddProduct(_order.products[i]);
                else
                    CreateProduct(_order.products[i]);
            }
        }

        /// <summary>
        /// Добавление продукта в заказ к уже имеющимся
        /// </summary>
        /// <param name="chooseProduct"></param>
        void AddProduct(Product chooseProduct)
        {
            for (int i = 0; i < OrderList.Items.Count; i++)
            {
                Label nameLabel = ((OrderList.Items[i] as StackPanel).Children[0] as Label);
                Label priseLabel = ((OrderList.Items[i] as StackPanel).Children[1] as Label);
                Label countLabel = ((OrderList.Items[i] as StackPanel).Children[2] as Label);
                Label fullPriseLabel = ((OrderList.Items[i] as StackPanel).Children[3] as Label);

                if (nameLabel.Content == chooseProduct.name)
                {
                    countLabel.Content = Convert.ToInt32(countLabel.Content) + 1;
                    fullPriseLabel.Content = Convert.ToInt32(priseLabel.Content) * Convert.ToInt32(countLabel.Content);
                }
            }
        }

        /// <summary>
        /// Добавление продукта в заказ 
        /// </summary>
        /// <param name="chooseProduct"></param>
        void CreateProduct(Product chooseProduct)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            Label name = new Label()
            {
                Content = chooseProduct.name,
                Width = 110
            };
            Label prise = new Label()
            {
                Content = chooseProduct.price,
                Width = 50
            };
            Label count = new Label()
            {
                Content = "1",
                Width = 30
            };
            Label fullPrise = new Label()
            {
                Content = chooseProduct.price,
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
