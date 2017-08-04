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
        private int _selectedOrderIndex;
        public int SelectedOrderIndex

        {
            get
            {
                return _selectedOrderIndex;
            }
            set
            {
                if (value != _selectedOrderIndex)
                {
                    _selectedOrderIndex = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Order SelectedOrder { get; set; }

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

        public async Task ReadOrders()
        {
            SelectedOrderIndex = -1;

            using (var orderSQLWork = new OrderSQLWork())
                Orders = await orderSQLWork.GetClientOrdersAsync(Client).ConfigureAwait(false);
        }


        public void OpenOrderInfo()
        {
            Execute.OnUIThread(() =>
            {
                OrderInfoViewModel OrderInfoViewModel = new OrderInfoViewModel();

                OrderInfoViewModel.LoadOrderInfo(SelectedOrder);

                var wm = new WindowManager();
                wm.ShowWindow(OrderInfoViewModel);

            });

            SelectedOrderIndex = -1;

        }
    }
}
