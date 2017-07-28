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
        private readonly PasswordClass _passwordClass; //класс кодировки пароля

        #region Constructor
        public AccessViewModel()
        {
            _passwordClass = new PasswordClass();
        }
        #endregion

        #region UI Commands

        public async Task HandleLoginClick(PasswordBox passwordBox)
        {
            var password = _passwordClass.Base64Encode(passwordBox.Password);

            using (var repository = new ClientSQLWork())
            {
                var client = await repository.GetClient(Login, password).ConfigureAwait(false);
                if (client == null)
                {
                    
                    // Неверный логин/пароль
                }
                else
                {
                    ClientIdentitySingleton.Instance.CurrentClient = client;

                    Execute.OnUIThread(() =>
                    {
                        var wm = new WindowManager();
                        wm.ShowDialog(new ClientViewModel() { Name = client.Name }, "View");
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

        #endregion

        #region Properties

        public string Login { get; set; }

        #endregion

    }
}
