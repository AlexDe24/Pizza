using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client.ViewModels
{
    public class OrderListViewModel : Screen
    {
        private List<Order> _orders;
        public List<Order> Orders
        {
            get
            {
                return _orders;
            }
            set
            {
                if (value != _orders)
                {
                    _orders = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Logic.DTO.Client Client { get; set; }

        public OrderListViewModel()
        {
        }

        public async Task ReadOrders()
        {
            using (var orderSQLWork = new OrderSQLWork())
                Orders = await orderSQLWork.GetClientOrdersAsync(Client).ConfigureAwait(false);
        }
    }
}
