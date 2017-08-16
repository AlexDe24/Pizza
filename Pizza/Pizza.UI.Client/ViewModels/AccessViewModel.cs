using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Logic;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Pizza.UI.Client.ViewModels
{
    internal class AccessViewModel : Screen
    {
        #region Properties

        public string Login { get; set; }
        public bool IsSaveCLient { get; set; }
        private readonly PasswordClass _passwordClass; //класс кодировки пароля

        #endregion

        #region Constructor

        internal AccessViewModel()
        {
            IsSaveCLient = true;
            Login = Properties.Settings.Default.Login;

            DisplayName = "Вход";

            _passwordClass = new PasswordClass();
        }

        #endregion

        #region UI Commands

        /// <summary>
        /// Функций для конпкий "Вход"
        /// </summary>
        public async Task HandleLoginClick(PasswordBox passwordBox)
        {
            var password = _passwordClass.Base64Encode(passwordBox.Password);

            using (var clientSQLWork = new ClientSQLWork())
            {
                var client = await clientSQLWork.GetClient(Login, password).ConfigureAwait(false);

                if (client == null)
                {
                    MessageBox.Show(Properties.Resources.WrongLoginOrPassword, "Внимание!");
                }
                else
                {
                    ClientIdentitySingleton.Instance.CurrentClient = client;

                    if (IsSaveCLient)
                    {
                        Properties.Settings.Default.Login = client.Login;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Login = "";
                        Properties.Settings.Default.Save();
                    }

                    TryClose();
                }
            }
        }

        /// <summary>
        /// Функций для кнопкий "Регистрация"
        /// </summary>
        public void HandleRegistrationClick()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowDialog(new RegistrationViewModel());
            });
        }

        /// <summary>
        /// Функций для конпкий "Выйти"
        /// </summary>
        public void HandleExitClick()
        {
            TryClose();
        }

        #endregion
    }
}
