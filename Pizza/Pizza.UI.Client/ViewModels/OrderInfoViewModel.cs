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
    internal class OrderInfoViewModel : Screen
    {
        #region Properties

        private Order _order;
        public Order Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (value != _order)
                {
                    _order = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private decimal _orderSum;
        public decimal OrderSum
        {
            get
            {
                return _orderSum;
            }
            set
            {
                if (value != _orderSum)
                {
                    _orderSum = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        #endregion

        #region Constructor

        internal OrderInfoViewModel()
        {
            DisplayName = "Заказ";
        }

        #endregion

        #region Commands

        /// <summary>
        /// Загрузка данных о заказе
        /// </summary>
        /// <param name="ThisOrder"></param>
        public void LoadOrderInfo(Order ThisOrder)
        {
            Order = ThisOrder;

            Order.OrderProducts.ForEach(op => OrderSum += op.Products.Price * op.CountProducts);
        }

        #endregion
    }
}
