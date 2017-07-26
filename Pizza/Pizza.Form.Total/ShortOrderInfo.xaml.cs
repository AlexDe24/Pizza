using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Pizza.Form.Total
{
    /// <summary>
    /// Логика взаимодействия для ShortOrderInfo.xaml
    /// </summary>
    public partial class ShortOrderInfo : Window
    {
        OrderInfo _orderInfo;
        OrderSQLWork _orderSQLWork;

        List<Order> _orders;

        public ShortOrderInfo(Client client)
        {
            InitializeComponent();

            _orderSQLWork = new OrderSQLWork();

            OrdersListUpdate(client);
        }

        void OrdersListUpdate(Client client)
        {
            _orders = _orderSQLWork.ReadOrders().Where(x => x.ClientId == client.Id).ToList();
            _orders.Sort((a, b) => b.Date.CompareTo(a.Date));

            OrdersList.Items.Clear();

            for (int i = 0; i < _orders.Count; i++)
            {
                OrdersList.Items.Add(_orders[i]);
            }
        }

        private void OrdersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(OrdersList.SelectedIndex > -1)
            {
                _orderInfo = new OrderInfo(_orders[OrdersList.SelectedIndex]);
                _orderInfo.Show();
            }
        }
    }
}
