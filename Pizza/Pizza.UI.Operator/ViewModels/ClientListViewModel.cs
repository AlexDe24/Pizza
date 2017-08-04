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
    class ClientListViewModel : Screen
    {
        #region Properties

        public Client SelectedClient { get; set; }

        private List<Client> _clients;
        public List<Client> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                if (value != _clients)
                {
                    _clients = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        #endregion

        public ClientListViewModel()
        {
            DisplayName = "Список клиентов";
        }

        #region IU Commands

        public async Task Load()
        {
            using(var ClientSQLWork = new ClientSQLWork())
            {
                Clients = await ClientSQLWork.ReadClientsAsync().ConfigureAwait(false);
            }
        }

        public async Task HandleClientListUpdateClick()
        {
            using (var ClientSQLWork = new ClientSQLWork())
            {
                Clients = await ClientSQLWork.ReadClientsAsync().ConfigureAwait(false);
            }
        }

        public void HandleClientRegistrationClick()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowWindow(new RegistrationViewModel());
            });
        }

        public void HandleClientEditClick()
        {
            if (SelectedClient != null)
                Execute.OnUIThread(() =>
                {
                    var ClientViewModel = new ClientViewModel() { Client = SelectedClient };
                    ClientViewModel.Load();

                    var wm = new WindowManager();
                    wm.ShowWindow(ClientViewModel);
                });
        }

        #endregion
    }
}
