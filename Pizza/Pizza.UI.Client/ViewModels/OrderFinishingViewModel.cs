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
    public class OrderFinishingViewModel : Screen
    {
        #region Properties

        public Order Order;
        public decimal OrderSum;

        public Logic.DTO.Client Client;

        public string Address { get; set; }
        public string Phone { get; set; }

        public decimal ClientDiscount { get; set; }

        private decimal _orderDiscount;
        public decimal OrderDiscount
        {
            get
            {
                return _orderDiscount;
            }
            set
            {
                if (value != _orderDiscount)
                {
                    _orderDiscount = value;

                    if (_orderDiscount > OrderSum)
                        _orderDiscount = OrderSum;

                    if (_orderDiscount > Client.Discount)
                        _orderDiscount = Client.Discount;

                    NotifyOfPropertyChange();
                }
            }
        }

        #endregion

        #region Constructor

        public OrderFinishingViewModel()
        {
            DisplayName = "Подтвердите свой заказ";

            Client = ClientIdentitySingleton.Instance.CurrentClient;

            Address = Client.Address;
            Phone = Client.Phone;

            ClientDiscount = Client.Discount;
        }

        #endregion

        #region UI Commands

        public void HandleCreateOrder()
        {
            List<Status> status;
            int lastNom;

            using (var statusSQLWork = new StatusSQLWork())
            {
                status = statusSQLWork.ReadStatus();
            }

            using (var orderSQLWork = new OrderSQLWork())
            {
                lastNom = orderSQLWork.GetOrdersLastNom();
            }

            Order.Discount = Convert.ToDecimal(OrderDiscount);
            Order.Phone = Phone;
            Order.Address = Address;
            Order.StatusId = status[0].Id;
            Order.Date = DateTime.Now;
            Order.ClientId = Client.Id;

            Client.Discount -= OrderDiscount;
            Client.Discount += OrderSum * 0.03m;

            if (lastNom == 300)
                Order.Nom = 0;
            else
                Order.Nom = lastNom + 1;

            using (var clientSQLWork = new ClientSQLWork())
            {
                clientSQLWork.EditClientDiscount(Client);
            }

            using (var orderSQLWork = new OrderSQLWork())
            {
                orderSQLWork.AddOrder(Order);
            }

            TryClose();
        }

        public void HandleCancel()
        {
            TryClose();
        }

        #endregion
    }
}
