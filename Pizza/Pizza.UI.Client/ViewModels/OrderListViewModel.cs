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
    internal class OrderListViewModel : Screen
    {
        #region Properties

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

        #endregion

        #region Construcor
        
        internal OrderListViewModel()
        {
            DisplayName = "Списак заказов";
        }

        #endregion

        #region  Commands

        /// <summary>
        /// Загрузка списка заказов(вызывается извне)
        /// </summary>
        /// <returns></returns>
        public async Task ReadOrders()
        {
            using (var orderSQLWork = new OrderSQLWork())
                Orders = await orderSQLWork.GetClientOrdersAsync(Client).ConfigureAwait(false);
        }

        #endregion

        #region UI Commands

        /// <summary>
        /// Переход в форму подробной информации о заказе
        /// </summary>
        public void OpenOrderInfo()
        {
            Execute.OnUIThread(() =>
            {
                OrderInfoViewModel OrderInfoViewModel = new OrderInfoViewModel();

                OrderInfoViewModel.LoadOrderInfo(SelectedOrder);

                var wm = new WindowManager();
                wm.ShowWindow(OrderInfoViewModel);
            });
        }

        #endregion
    }
}
