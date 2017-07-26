using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pizza.Logic.DTO;
using Pizza.Form.Total;
using Pizza.Logic.Repositories;

namespace Pizza.Form.OperatorPart
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

        StatusSQLWork _statusSQLWork;
        OrderSQLWork _orderSQLWork;

        List<Order> _orders;
        List<Order> _allOrders;

        public OperatorMenu()
        {
            InitializeComponent();

            _orderSQLWork = new OrderSQLWork();
            _statusSQLWork = new StatusSQLWork();

            _status = _statusSQLWork.ReadStatus();
            _orders = new List<Order>();

            for (int i = 0; i < _status.Count; i++)
            {
                OrderCondition.Items.Add(_status[i].Name);
            }

            OrdersListUpdate();
        }

        /// <summary>
        /// Обновление списка заказов
        /// </summary>
        void OrdersListUpdate()
        {
            _allOrders = _orderSQLWork.ReadOrders();
            _orders.Clear();

            for (int i = 0; i < _allOrders.Count; i++)
            {
                _orders.Add(_allOrders[i]);
            }

            _orders.Sort((a, b) => b.Date.CompareTo(a.Date));

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
                    _orders.Sort((a, b) => a.Nom.CompareTo(b.Nom));
                    break;
                case "Номер↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Номер↓";
                    _orders.Sort((a, b) => b.Nom.CompareTo(a.Nom));
                    break;
                case "Адрес":
                case "Адрес↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↑";
                    _orders.Sort((a, b) => b.Address.CompareTo(a.Address));
                    break;
                case "Адрес↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↓";
                    _orders.Sort((a, b) => a.Address.CompareTo(b.Address));
                    break;
                case "Дата заказа":
                case "Дата заказа↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата заказа↑";
                    _orders.Sort((a, b) => b.Date.CompareTo(a.Date));
                    break;
                case "Дата заказа↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата заказа↓";
                    _orders.Sort((a, b) => a.Date.CompareTo(b.Date));
                    break;
                case "Состояние заказа":
                case "Состояние заказа↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Состояние заказа↑";
                    _orders.Sort((a, b) => b.Status.Id.CompareTo(a.Status.Id));
                    break;
                case "Состояние заказа↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Состояние заказа↓";
                    _orders.Sort((a, b) => a.Status.Id.CompareTo(b.Status.Id));
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
                OrderCondition.Text = (OrdersList.SelectedItem as Order).Status.Name;
            }
        }

        private void LoadCondition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OrdersList.SelectedIndex != -1)
                {
                    Order order = _orders[OrdersList.SelectedIndex];
                    order.Status = _status.Where(x => x.Name == OrderCondition.Text).FirstOrDefault();

                    _orderSQLWork.EditOrder();
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
                _orderInfo = new OrderInfo(_orders[OrdersList.SelectedIndex]);
                _orderInfo.Show();
            }
        }

        private void Find_KeyUp(object sender, KeyEventArgs e)
        {
            _orders.Clear();

            var findParam = Find.Text.Split(' ');

            for (int i = 0; i < _allOrders.Count; i++)
            {
                string allParam = Convert.ToString(_allOrders[i].Nom) + _allOrders[i].Status.Name + _allOrders[i].Address + _allOrders[i].Phone + _allOrders[i].Date;
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
