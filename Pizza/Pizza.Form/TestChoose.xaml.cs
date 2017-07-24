using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pizza.Logic;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для TestChoose.xaml
    /// </summary>
    public partial class TestChoose : Window
    {
        Access _accessClient;
        OperatorMenu _operatorMenu;
  
        public TestChoose()
        {
            InitializeComponent();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            _operatorMenu = new OperatorMenu();
            _operatorMenu.Show();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            _accessClient = new Access();
            _accessClient.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
