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

namespace Pizza.Form.ClientPart
{
    /// <summary>
    /// Логика взаимодействия для Сomment.xaml
    /// </summary>
    public partial class Сomment : Window
    {
        public Сomment()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Спасибо за ваш отзыв!\nВместе мы сделаем мир лучше!");
        }
    }
}
