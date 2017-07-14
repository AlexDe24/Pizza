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
using System.Collections;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для OperatorMenu.xaml
    /// </summary>
    public partial class OperatorMenu : Window
    {
        OrderInfo _orderInfo;
        CreateMenu _createMenu;
        ClientList _clientList;
        FileClass _fileWork;

        List<Order> _orders;

        public OperatorMenu()
        {
            InitializeComponent();

            _fileWork = new FileClass();

            string[] category = Properties.Resources.OrderCondition.Split(',');

            for (int i = 0; i < category.Length; i++)
            {
                OrderCondition.Items.Add(category[i]);
            }

            OrdersListUpdate();
        }

        void OrdersListUpdate()
        {
            _orders = _fileWork.ReadOrders();
            _orders.Sort((a, b) => b.date.CompareTo(a.date));

            OrdersList.Items.Clear();

            for (int i = 0; i < _orders.Count; i++)
            {
                OrdersList.Items.Add(_orders[i]);
            }
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            _clientList = new ClientList();
            _clientList.Show();
        }

        private void MenuCreate_Click(object sender, RoutedEventArgs e)
        {
            _createMenu = new CreateMenu();
            _createMenu.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            OrdersListUpdate();
        }

        private void OrdersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersList.SelectedIndex != -1)
            {
                OrderCondition.Text = (OrdersList.SelectedItem as Order).condition;
            }
        }

        private void LoadCondition_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersList.SelectedIndex != -1)
            {
                Order order = _orders[OrdersList.SelectedIndex];
                order.condition = OrderCondition.Text;
                _fileWork.RedactOrder(order);

                OrdersListUpdate();
            }
        }

        private void SeeOrder_Click(object sender, RoutedEventArgs e)
        {
            Order();
        }

        private void OrdersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Order();
        }

        void Order()
        {
            if (OrdersList.SelectedIndex > -1)
            {
                _orderInfo = new OrderInfo(_orders[OrdersList.SelectedIndex]);
                _orderInfo.Show();
            }
        }
    }
}
