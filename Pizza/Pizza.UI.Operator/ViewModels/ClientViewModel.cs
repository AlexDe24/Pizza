using Caliburn.Micro;
using Pizza.Logic;
using Pizza.Logic.Repositories;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pizza.UI.Operator.ViewModels
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

        private Visibility _buttonsVisibility;
        public Visibility ButtonsVisibility
        {
            get
            {
                return _buttonsVisibility;
            }
            set
            {
                if (value != _buttonsVisibility)
                {
                    _buttonsVisibility = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        private Visibility _buttonEditVisibility;
        public Visibility ButtonEditVisibility
        {
            get
            {
                return _buttonEditVisibility;
            }
            set
            {
                if (value != _buttonEditVisibility)
                {
                    _buttonEditVisibility = value;
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

        #region Constructor

        internal ClientViewModel()
        {
            DisplayName = "Профиль";
            EditClientButtonText = "Редактировать";

            ButtonsVisibility = Visibility.Collapsed;
            ButtonEditVisibility = Visibility.Visible;
            TextBoxesVisibility = Visibility.Hidden;
            PasswordEditVisibility = Visibility.Hidden;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Загрузка данных о клиенте
        /// </summary>
        public void Load()
        {
            Surname = Client.Surname;
            Name = Client.Name;
            Middlename = Client.Middlename;
            Password = Client.Password;
            BirthDate = Client.BirthDate;
            Address = Client.Address;
            Phone = Client.Phone;

            Discount = Client.Discount;
        }

        #endregion

        #region UI Commands

        /// <summary>
        /// Функция для конпки "Редактировать"
        /// </summary>
        public void HandleVisibilityChangeClick()
        {
            ButtonsVisibility = Visibility.Visible;
            ButtonEditVisibility = Visibility.Collapsed;
            TextBoxesVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Функция для конпки "Сохранить"
        /// </summary>
        public void HandleSaveClick(PasswordBox PasswordOrig, PasswordBox PasswordControl)
        {
            PasswordClass passwordClass = new PasswordClass();

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
                MessageBox.Show(Properties.Resources.NoEqualPassowrs, "Внимание!");
            }
        }

        /// <summary>
        /// Функция для конпки "Сменить пароль"
        /// </summary>
        public void HandleEditPasswordClick()
        {
            PasswordEditVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Функция для конпки "Удалить профиль"
        /// </summary>
        public void HandleDelClient()
        {
            Execute.OnUIThread(() =>
            {
                var DialogViewModel = new DialogViewModel() { Client = Client };
                var wm = new WindowManager();
                wm.ShowDialog(DialogViewModel);

                if (DialogViewModel.Client == null)
                {
                    TryClose();
                }

            });
        }

        #endregion
    }
}
