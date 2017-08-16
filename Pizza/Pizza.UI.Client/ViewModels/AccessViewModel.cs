using Caliburn.Micro;
using Pizza.Logic.DTO;
using Pizza.Form.Total;
using Pizza.Logic;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        public AccessViewModel()
        {
            DisplayName = "Вход";

            _passwordClass = new PasswordClass();
        }
        #endregion

        #region UI Commands

        public async Task HandleLoginClick(PasswordBox passwordBox)
        {
            var password = _passwordClass.Base64Encode(passwordBox.Password);

            using (var clientSQLWork = new ClientSQLWork())
            {
                var client = await clientSQLWork.GetClient(Login, password).ConfigureAwait(false);

                if (client == null)
                {
                    Execute.OnUIThread(() =>
                    {
                        var wm = new WindowManager();
                        wm.ShowDialog(new MessageViewModel() { ErrorMessage = Properties.Resources.WrongLoginOrPassword });
                    });
                }
                else
                {
                    ClientIdentitySingleton.Instance.CurrentClient = client;

                    Execute.OnUIThread(() =>
                    {
                        MenuViewModel menuViewModel = new MenuViewModel();
                        menuViewModel.DataLoad().Wait();

                        var wm = new WindowManager();
                        wm.ShowWindow(menuViewModel);
                    });
                }
            }
        }

        public void HandleRegistrationClick()
        {
            Execute.OnUIThread(() =>
            {
                var wm = new WindowManager();
                wm.ShowDialog(new RegistrationViewModel());
            });
        }

        public void HandleExitClick()
        {
            TryClose();
        }
        #endregion
    }
}
