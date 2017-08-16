using Caliburn.Micro;
using Pizza.Logic;
using Pizza.Logic.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pizza.UI.Client.ViewModels
{
    internal class ClientViewModel : Screen
    {
        #region Properties

        public Logic.DTO.Client Client;

        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; } //день рождения
        public string Address { get; set; } //номер телефона
        public string Phone { get; set; } //номер телефона
        public decimal Discount { get; set; } //скидка

        private Visibility _buttonsEditVisibility;
        public Visibility ButtonsEditVisibility
        {
            get
            {
                return _buttonsEditVisibility;
            }
            set
            {
                if (value != _buttonsEditVisibility)
                {
                    _buttonsEditVisibility = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private Visibility _passwordEditVisibility;
        public Visibility PasswordEditVisibility
        {
            get
            {
                return _passwordEditVisibility;
            }
            set
            {
                if (value != _passwordEditVisibility)
                {
                    _passwordEditVisibility = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private Visibility _textBoxesVisibility;
        public Visibility TextBoxesVisibility
        {
            get
            {
                return _textBoxesVisibility;
            }
            set
            {
                if (value != _textBoxesVisibility)
                {
                    _textBoxesVisibility = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private string _editClientButtonText;
        public string EditClientButtonText
        {
            get
            {
                return _editClientButtonText;
            }
            set
            {
                if (value != _editClientButtonText)
                {
                    _editClientButtonText = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        #endregion

        internal ClientViewModel()
        {
            DisplayName = "Профиль";
            EditClientButtonText = "Редактировать";

            ButtonsEditVisibility = Visibility.Collapsed;
            TextBoxesVisibility = Visibility.Hidden;
            PasswordEditVisibility = Visibility.Hidden;

            Client = ClientIdentitySingleton.Instance.CurrentClient;

            Surname = Client.Surname;
            Name = Client.Name;
            Middlename = Client.Middlename;
            Password = Client.Password;
            BirthDate = Client.BirthDate;
            Address = Client.Address;
            Phone = Client.Phone;

            Discount = Client.Discount;
        }

        #region UI Commands

        public void HandleVisibilityChangeClick()
        {
            ButtonsEditVisibility = Visibility.Visible;
            TextBoxesVisibility = Visibility.Visible;
        }

        public async Task HandleSaveClick(PasswordBox PasswordOld, PasswordBox PasswordOrig, PasswordBox PasswordControl)
        {
            PasswordClass passwordClass = new PasswordClass();

            if (passwordClass.Base64Encode(PasswordOld.Password) == Password)
            {
                if (passwordClass.Base64Encode(PasswordOrig.Password) == passwordClass.Base64Encode(PasswordControl.Password))
                {
                    Client.Surname = Surname;
                    Client.Name = Name;
                    Client.Middlename = Middlename;
                    Client.Password = passwordClass.Base64Encode(PasswordOrig.Password);
                    Client.BirthDate = BirthDate;
                    Client.Address = Address;
                    Client.Phone = Phone;

                    ClientSQLWork clientSQLWork = new ClientSQLWork();
                    clientSQLWork.EditClient(Client);

                    TryClose();
                }
                else
                {
                    var wm = new WindowManager();
                    wm.ShowDialog(new MessageViewModel() { ErrorMessage = Properties.Resources.NoEqualPassowrs });
                }
            }
            else
            {
                var wm = new WindowManager();
                wm.ShowDialog(new MessageViewModel() { ErrorMessage = Properties.Resources.WrongPassword });

            }
        }

        public void HandleEditPasswordClick()
        {
            PasswordEditVisibility = Visibility.Visible;
        }

        public void HandleSeeOrders()
        {
            Execute.OnUIThread(() =>
            {
                var OrderListViewModel = new OrderListViewModel() { Client = Client };

                OrderListViewModel.ReadOrders().Wait();

                var wm = new WindowManager();
                wm.ShowWindow(OrderListViewModel);
            });
        }

        public void HandleDelClient()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowDialog(new DialogViewModel());
            });
        }

        #endregion
    }
}
