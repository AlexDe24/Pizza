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
using System.Data.Entity.Validation;
using System.ComponentModel;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для OperatorMenu.xaml
    /// </summary>
    public partial class OperatorMenu : Window
    {
        List<Status> _status;
        OrderInfo _orderInfo;
        CreateMenu _createMenu;
        ClientList _clientList;
        FileClass _fileWork;

        List<Order> _orders;
        List<Order> _allOrders;

        public OperatorMenu()
        {
            InitializeComponent();

            _fileWork = new FileClass();
            _status = _fileWork.ReadStatus();
            _orders = new List<Logic.Order>();

            for (int i = 0; i < _status.Count; i++)
            {
                OrderCondition.Items.Add(_status[i].name);
            }

            OrdersListUpdate();
        }

        /// <summary>
        /// Обновление списка заказов
        /// </summary>
        void OrdersListUpdate()
        {
            _allOrders = _fileWork.ReadOrders();
            _orders.Clear();

            for (int i = 0; i < _allOrders.Count; i++)
            {
                _orders.Add(_allOrders[i]);
            }

            _orders.Sort((a, b) => b.date.CompareTo(a.date));

            OrdersList.Items.Clear();

            for (int i = 0; i < _orders.Count; i++)
            {
                OrdersList.Items.Add(_orders[i]);
            }
        }

        void OrdersListHeaderUpdate()
        {
            Nom.Content = "Номер";
            Address.Content = "Адрес";
            Date.Content = "Дата заказа";
            Status.Content = "Состояние заказа";
        }

        /// <summary>
        /// Сортировка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OrdersListSort(object sender,RoutedEventArgs e)
        {
            switch ((sender as GridViewColumnHeader).Content)
            {
                case "Номер":
                case "Номер↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Номер↑";
                    _orders.Sort((a, b) => a.nom.CompareTo(b.nom));
                    break;
                case "Номер↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Номер↓";
                    _orders.Sort((a, b) => b.nom.CompareTo(a.nom));
                    break;
                case "Адрес":
                case "Адрес↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↑";
                    _orders.Sort((a, b) => b.address.CompareTo(a.address));
                    break;
                case "Адрес↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↓";
                    _orders.Sort((a, b) => a.address.CompareTo(b.address));
                    break;
                case "Дата заказа":
                case "Дата заказа↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата заказа↑";
                    _orders.Sort((a, b) => b.date.CompareTo(a.date));
                    break;
                case "Дата заказа↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата заказа↓";
                    _orders.Sort((a, b) => a.date.CompareTo(b.date));
                    break;
                case "Состояние заказа":
                case "Состояние заказа↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Состояние заказа↑";
                    _orders.Sort((a, b) => b.status.id.CompareTo(a.status.id));
                    break;
                case "Состояние заказа↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Состояние заказа↓";
                    _orders.Sort((a, b) => a.status.id.CompareTo(b.status.id));
                    break;
                default:
                    break;
            }   

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
                OrderCondition.Text = (OrdersList.SelectedItem as Order).status.name;
            }
        }

        private void LoadCondition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrdersList.SelectedIndex != -1)
                {
                    Order order = _orders[OrdersList.SelectedIndex];
                    order.status = _status.Where(x => x.name == OrderCondition.Text).FirstOrDefault();

                    _fileWork.RedactOrder();
                }
            }
            catch (Exception)
            {
            }
            OrdersListUpdate();
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
                _orderInfo = new OrderInfo(_orders[OrdersList.SelectedIndex], _fileWork);
                _orderInfo.Show();
            }
        }

        private void Find_KeyUp(object sender, KeyEventArgs e)
        {
            _orders.Clear();

            var findParam = Find.Text.Split(' ');

            for (int i = 0; i < _allOrders.Count; i++)
            {
                string allParam = Convert.ToString(_allOrders[i].nom) + _allOrders[i].status.name + _allOrders[i].address + _allOrders[i].phone + _allOrders[i].date;
                bool contains = true;

                for (int j = 0; j < findParam.Length; j++)
                {
                    if (allParam.Contains(findParam[j]) != true)
                        contains = false;
                }

                if (contains == true)
                    _orders.Add(_allOrders[i]);

            }

            OrdersList.Items.Clear();

            for (int i = 0; i < _orders.Count; i++)
            {
                OrdersList.Items.Add(_orders[i]);
            }
        }
    }
}
