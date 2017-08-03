using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Total.ViewModels
{
    public class OrderListViewModel : Screen
    {
        public List<Order> Orders { get; set; }
        public Client Client { get; set; }

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
