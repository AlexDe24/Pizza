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
    internal class DialogViewModel : Screen
    {
        #region Constructor

        internal DialogViewModel()
        {
            DisplayName = "Внимание!";
        }

        #endregion

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
