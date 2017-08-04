using Caliburn.Micro;
using Pizza.UI.Operator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pizza.UI.Operator
{
    class ApplicationBootstrapper : BootstrapperBase
    {
        public ApplicationBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<OperatorMenuViewModel>();
        }

    }
}
