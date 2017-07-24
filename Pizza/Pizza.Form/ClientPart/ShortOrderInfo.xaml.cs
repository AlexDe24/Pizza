using Pizza.Logic;
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

namespace Pizza.Form.ClientPart
{
    /// <summary>
    /// Логика взаимодействия для ShortOrderInfo.xaml
    /// </summary>
    public partial class ShortOrderInfo : Window
    {
        OrderInfo _orderInfo;
        FileClass _fileWork;

        List<Order> _orders;

        public ShortOrderInfo(FileClass fileWork, Client client)
        {
            InitializeComponent();

            _fileWork = fileWork;

            OrdersListUpdate(client);
        }

        void OrdersListUpdate(Client client)
        {
            _orders = _fileWork.ReadOrders().Where(x => x.clientId == client.id).ToList();
            _orders.Sort((a, b) => b.date.CompareTo(a.date));

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
                _orderInfo = new OrderInfo(_orders[OrdersList.SelectedIndex], _fileWork);
                _orderInfo.Show();
            }
        }
    }
}
