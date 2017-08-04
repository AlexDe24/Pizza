using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza.UI.Operator.ViewModels
{
    class DialogViewModel : Screen
    {
        #region Properties

        public Client Client;

        #endregion


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
                clientSQLWork.DeleteClient(Client);

            Client = null;

            TryClose();
        }

        #endregion

    }
}
