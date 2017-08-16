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
    internal class ClientListViewModel : Screen
    {
        #region Properties

        public string _nameFilter;
        public string NameFilter
        {
            get
            {
                return _nameFilter;
            }
            set
            {
                if (value != _nameFilter)
                {
                    _nameFilter = value;
                    NotifyOfPropertyChange(() => Clients);
                }
            }
        }

        public string _surnameFilter;
        public string SurnameFilter
        {
            get
            {
                return _surnameFilter;
            }
            set
            {
                if (value != _surnameFilter)
                {
                    _surnameFilter = value;
                    NotifyOfPropertyChange(() => Clients);
                }
            }
        }

        public string _middlenameFilter;
        public string MiddlenameFilter
        {
            get
            {
                return _middlenameFilter;
            }
            set
            {
                if (value != _middlenameFilter)
                {
                    _middlenameFilter = value;
                    NotifyOfPropertyChange(() => Clients);
                }
            }
        }

        public string _dateFilter;
        public string DateFilter
        {
            get
            {
                return _dateFilter;
            }
            set
            {
                if (value != _dateFilter)
                {
                    _dateFilter = value;
                    NotifyOfPropertyChange(() => Clients);
                }
            }
        }

        public string _addressFilter;
        public string AddressFilter
        {
            get
            {
                return _addressFilter;
            }
            set
            {
                if (value != _addressFilter)
                {
                    _addressFilter = value;
                    NotifyOfPropertyChange(() => Clients);
                }
            }
        }

        public string _phoneFilter;
        public string PhoneFilter
        {
            get
            {
                return _phoneFilter;
            }
            set
            {
                if (value != _phoneFilter)
                {
                    _phoneFilter = value;
                    NotifyOfPropertyChange(() => Clients);
                }
            }
        }

        public Client SelectedClient { get; set; }

        private List<Client> _clients;
        public List<Client> Clients
        {
            get
            {
                return _clients.Where(x => x.Name.ToLowerInvariant().Contains(NameFilter.ToLowerInvariant())
                && x.Surname.ToLowerInvariant().Contains(SurnameFilter.ToLowerInvariant())
                && x.Middlename.ToLowerInvariant().Contains(MiddlenameFilter.ToLowerInvariant())
                && x.BirthDate.ToString().Contains(DateFilter)
                && x.Phone.Contains(PhoneFilter)
                && x.Address.ToLowerInvariant().Contains(AddressFilter.ToLowerInvariant()))
                .ToList();
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

        #region Constructor

        internal ClientListViewModel()
        {
            DisplayName = "Список клиентов";

            NameFilter = "";
            SurnameFilter = "";
            MiddlenameFilter = "";
            DateFilter = "";
            AddressFilter = "";
            PhoneFilter = "";
        }

        #endregion

        #region Commands

        /// <summary>
        /// Загрузка клиентов из базы
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            using (var ClientSQLWork = new ClientSQLWork())
            {
                Clients = await ClientSQLWork.ReadClientsAsync().ConfigureAwait(false);
            }
        }

        #endregion

        #region UI Commands

        /// <summary>
        /// Функция обновления списка клиентов
        /// </summary>
        /// <returns></returns>
        public async Task HandleClientListUpdateClick()
        {
            await Load().ConfigureAwait(false);
        }

        /// <summary>
        /// Функция вызова окна регистрации
        /// </summary>
        public void HandleClientRegistrationClick()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowWindow(new RegistrationViewModel());
            });
        }

        /// <summary>
        /// Функция редактирвания клиента
        /// </summary>
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
