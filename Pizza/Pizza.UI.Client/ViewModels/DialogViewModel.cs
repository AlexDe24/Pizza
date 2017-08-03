using Caliburn.Micro;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza.UI.Client.ViewModels
{
    class DialogViewModel : Screen
    {
        public DialogViewModel()
        {
            DisplayName = "Внимание!";
        }

        #region UI Commands

        public void HandleCloseClick()
        {
            TryClose();
        }

        public void HandleDelClientClick()
        {
            using (var clientSQLWork = new ClientSQLWork())
                clientSQLWork.DeleteClient(ClientIdentitySingleton.Instance.CurrentClient);
            
            Application.Current.Shutdown();

        }

        #endregion

    }
}
