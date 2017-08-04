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
    public class OrderInfoViewModel : Screen
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

        public void Load(Order ThisOrder)
        {
            Order = ThisOrder;

            for (int i = 0; i < Order.OrderProducts.Count; i++)
            {
                OrderSum += Order.OrderProducts[i].Products.Price * Order.OrderProducts[i].CountProducts;
            }
        }
    }
}
