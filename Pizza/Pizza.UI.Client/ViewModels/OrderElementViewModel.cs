using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client.ViewModels
{
    class OrderElementViewModel : Screen
    {

        #region Properties

        public string Product { get; set; }

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
                NotifyOfPropertyChange(() => Quantity);
            }
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

        #endregion

    }
}
