using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Operator.ViewModels
{
    public class OperatorMenuViewModel : Screen
    {
        #region Properties

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                if (_selectedOrder != null)
                    if (value != _selectedOrder)
                    {
                        _selectedOrder = value;

                        NotifyOfPropertyChange();

                        SelectedStatus = _selectedOrder.Status;

                        NotifyOfPropertyChange(() => SelectedStatus);
                    }
            }
        }

        private Status _selectedStatus;
        public Status SelectedStatus
        {
            get
            {
                return _selectedStatus;
            }
            set
            {
                if (value != _selectedStatus)
                {
                    _selectedStatus = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private List<Status> _statuses;
        public List<Status> Statuses
        {
            get
            {
                return _statuses;
            }
            set
            {
                if (value != _statuses)
                {
                    _statuses = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private string _filter;
        public string Filter

        {
            get
            {
                return _filter;
            }
            set
            {
                if (value != _filter)
                {
                    _filter = value;
                    NotifyOfPropertyChange();
                    NotifyOfPropertyChange(() => Orders);

                }
            }
        }

        private List<Order> _orders;
        public List<Order> Orders
        {
            get
            {
                if (Filter == "")
                    return _orders.Where(x => x.Status.Name == Filter).ToList();
                else
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

        #endregion

        public OperatorMenuViewModel()
        {
            DisplayName = "Список заказов";

            LoadOrders().Wait();
        }

        #region IU Commands

        public async Task LoadOrders()
        {
            using (var orderSQlWork = new OrderSQLWork())
            {
                Orders = await orderSQlWork.GetOrdersAsync().ConfigureAwait(false);
            }

            using (var statusSQLWork = new StatusSQLWork())
            {
                Statuses = await statusSQLWork.GetStatusesAsync().ConfigureAwait(false);
            }
        }

        public void OpenOrderInfo()
        {
            if (SelectedOrder != null)
                Execute.OnUIThread(() =>
                {
                OrderInfoViewModel OrderInfoViewModel = new OrderInfoViewModel();

                OrderInfoViewModel.Load(SelectedOrder);

                var wm = new WindowManager();
                wm.ShowWindow(OrderInfoViewModel);

                });
            
        }

        public async Task OrderListUpdate()
        {
            using (var orderSQlWork = new OrderSQLWork())
            {
                Orders = await orderSQlWork.GetOrdersAsync().ConfigureAwait(false);
            }
        }

        public void OpenClientsList()
        {
            Execute.OnUIThread(() =>
            {
                var ClientListViewModel = new ClientListViewModel();

                ClientListViewModel.Load().Wait();

                var wm = new WindowManager();
                wm.ShowWindow(ClientListViewModel);

            });
        }

        public void OpenMenuList()
        {
            Execute.OnUIThread(() =>
            {
                var CreateMenuViewModel = new CreateMenuViewModel();

                CreateMenuViewModel.Load().Wait();

                var wm = new WindowManager();
                wm.ShowWindow(CreateMenuViewModel);
            });
        }

        #endregion
    }
}
