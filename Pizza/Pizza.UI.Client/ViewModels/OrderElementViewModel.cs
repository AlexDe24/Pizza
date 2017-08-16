using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pizza.UI.Client.ViewModels
{
    internal class OrderElementViewModel : Screen
    {
        #region Properties

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        private decimal _productFullPrice;
        public decimal ProductFullPrice
        {
            get
            {
                return _productFullPrice;
            }
            set
            {
                _productFullPrice = value;
                NotifyOfPropertyChange();
            }
        }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;

                if (_quantity == 0)
                    _quantity = 1;

                ProductFullPrice = ProductPrice * Quantity;

                NotifyOfPropertyChange();               
            }
        }

        #endregion

        #region Constructor

        internal OrderElementViewModel()
        {
            Quantity = 1;
        }

        #endregion

        #region UI Commands

        public void HandleIncreaseQuantity()
        {
            Quantity++;
        }

        public void HandleDecreaseQuantity()
        {
            Quantity--;
        }

        public void HandleDel()
        {
            Quantity = -1;
        }

        #endregion
    }
}
