using Caliburn.Micro;
using Pizza.Logic.Factory;
using Pizza.Logic.Repositories;
using Pizza.UI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza.UI.Client
{
    class ApplicationBootstrapper : BootstrapperBase
    {
        public ApplicationBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MenuViewModel>();
        }

    }
}
