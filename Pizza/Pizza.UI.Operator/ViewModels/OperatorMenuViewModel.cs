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

        public string _addressFilter;
        public string AddressFilter
        {
            get
            {
                return _addressFilter;
            }
            set
            {
                if (value != _addressFilter)
                {
                    _addressFilter = value;
                    NotifyOfPropertyChange(() => Orders);
                }
            }
        }

        private string _phoneFilter;
        public string PhoneFilter
        {
            get
            {
                return _phoneFilter;
            }
            set
            {
                if (value != _phoneFilter)
                {
                    _phoneFilter = value;
                    NotifyOfPropertyChange(() => Orders);
                }
            }
        }

        private string _dateFilter;
        public string DateFilter
        {
            get
            {
                return _dateFilter;
            }
            set
            {
                if (value != _dateFilter)
                {
                    _dateFilter = value;
                    NotifyOfPropertyChange(() => Orders);
                }
            }
        }

        private string _statusFilter;
        public string StatusFilter
        {
            get
            {
                return _statusFilter;
            }
            set
            {
                if (value != _statusFilter)
                {
                    _statusFilter = value;
                    NotifyOfPropertyChange(() => Orders);
                }
            }
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                if (value != _selectedOrder)
                {
                    _selectedOrder = value;
                    NotifyOfPropertyChange();

                    if (_selectedOrder != null)
                    {
                        SelectedStatus = Statuses.Single(x => x.Id == _selectedOrder.Status.Id);
                        NotifyOfPropertyChange(() => SelectedStatus);
                    }
                }
            }
        }

        private List<Order> _orders;
        public List<Order> Orders
        {
            get
            {
                return _orders.Where(x => x.Address.ToLowerInvariant().Contains(AddressFilter.ToLowerInvariant())
                && x.Status.Name.ToLowerInvariant().Contains(StatusFilter.ToLowerInvariant())
                && x.Phone.ToLowerInvariant().Contains(PhoneFilter.ToLowerInvariant())
                && x.Date.ToString().Contains(DateFilter))
                .ToList();
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

                    if (_selectedStatus != null && SelectedOrder != null)
                    {
                        SelectedOrder.Status = _selectedStatus;
                        NotifyOfPropertyChange(() => Orders);
                    }
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

        #endregion

        public OperatorMenuViewModel()
        {
            DisplayName = "Список заказов";

            AddressFilter = "";
            PhoneFilter = "";
            DateFilter = "";
            StatusFilter = "";

            Load().Wait();
        }

        #region IU Commands

        /// <summary>
        /// Загрузка заказов и статусов
        /// </summary>
        /// <returns></returns>
        public async Task Load()
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

        /// <summary>
        /// Просмотр подробной информации о заказе
        /// </summary>
        public void HandleOpenOrderInfoClick()
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

        /// <summary>
        /// Изменение состояния заказа
        /// </summary>
        /// <returns></returns>
        public async Task HandleStatusUpdateClick()
        {
            using (var orderSQLWork = new OrderSQLWork())
            {
                await orderSQLWork.EditOrderAsync(SelectedOrder).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Обновление списка заказов
        /// </summary>
        /// <returns></returns>
        public async Task HandleOrderListUpdateClick()
        {
            using (var orderSQlWork = new OrderSQLWork())
            {
                Orders = await orderSQlWork.GetOrdersAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Открытие окна меню
        /// </summary>
        public void HandleOpenMenuListClick()
        {
            Execute.OnUIThread(() =>
            {
                var CreateMenuViewModel = new CreateMenuViewModel();

                CreateMenuViewModel.Load().Wait();

                var wm = new WindowManager();
                wm.ShowWindow(CreateMenuViewModel);
            });
        }

        /// <summary>
        /// Открытие списка клиентов
        /// </summary>
        public void HandleOpenClientsListClick()
        {
            Execute.OnUIThread(() =>
            {
                var ClientListViewModel = new ClientListViewModel();

                ClientListViewModel.Load().Wait();

                var wm = new WindowManager();
                wm.ShowWindow(ClientListViewModel);

            });
        }

        #endregion
    }
}
