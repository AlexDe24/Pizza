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

namespace Pizza.Form.Total
{
    /// <summary>
    /// Логика взаимодействия для Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        public Confirmation()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
