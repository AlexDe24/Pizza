using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Operator.ViewModels
{
    public class MessageViewModel : Screen
    {
        #region Properties

        public string ErrorMessage { get; set; }

        #endregion

        public MessageViewModel()
        {
            DisplayName = "Внимание!";
        }

        #region UI Commands

        public void HandleCloseClick()
        {
            TryClose();
        }

        #endregion

    }
}
