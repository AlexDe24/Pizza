using Caliburn.Micro;
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

        private Visibility _passwordGridVisibility;
        public Visibility PasswordGridVisibility
        {
            get
            {
                return _passwordGridVisibility;
            }
            set
            {
                if (value != _passwordGridVisibility)
                {
                    _passwordGridVisibility = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private Visibility _editGridVisibility;
        public Visibility EditGridVisibility
        {
            get
            {
                return _editGridVisibility;
            }
            set
            {
                if (value != _editGridVisibility)
                {
                    _editGridVisibility = value;
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

            PasswordGridVisibility = Visibility.Hidden;
            EditGridVisibility = Visibility.Collapsed;

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

        public async Task HandleEditClick(PasswordBox PasswordOld, PasswordBox PasswordOrig, PasswordBox PasswordControl)
        {
            if (Convert.ToString(EditClientButtonText) == "Редактировать")
            {
                EditGridVisibility = Visibility.Visible;

                EditClientButtonText = "Сохранить"; 
            }
            else
            {
                Client.Surname = Surname;
                Client.Name = Name;
                Client.Middlename = Middlename;
                Client.Password = Password;
                Client.BirthDate = BirthDate;
                Client.Address = Address;
                Client.Phone = Phone;

                ClientSQLWork clientSQLWork = new ClientSQLWork();
                clientSQLWork.EditClient(Client);

                TryClose();

                /*var wm = new WindowManager();
                wm.ShowDialog(this, "Edit");*/
            }
        }

        public void HandleEditPasswordClick()
        {
            PasswordGridVisibility = Visibility.Visible;
        }

        public void HandleSeeOrders()
        {
            Execute.OnUIThread(() =>
            {
                var OrderListViewModel = new OrderListViewModel() { Client = Client };

                OrderListViewModel.ReadOrders().Wait();

                var wm = new WindowManager();
                wm.ShowDialog(OrderListViewModel);
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
