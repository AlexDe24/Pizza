using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client.ViewModels
{
    internal class ClientViewModel : Screen
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region UI Commands

        public async Task HandleEditClick()
        {
            TryClose();

            var wm = new WindowManager();
            wm.ShowDialog(this, "Edit");
        }

        public async Task HandleSaveClick()
        {

        }

        #endregion
    }
}
