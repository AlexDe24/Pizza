using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client.ViewModels
{
    internal class OrderViewModel : Screen
    {

        #region Properties

        public BindableCollection<OrderElementViewModel> Elements { get; set; }

        #endregion


        public OrderViewModel()
        {
            Elements = new BindableCollection<OrderElementViewModel>();
        }

    }
}
