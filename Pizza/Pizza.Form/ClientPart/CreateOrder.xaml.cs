using Pizza.Logic;
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

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        Client _client;
        Order _order;
        decimal _sum;

        public CreateOrder(Order order, decimal sum, Client client)
        {
            InitializeComponent();

            Confirm.Focus();

            _order = order;
            _sum = sum;
            _client = client;

            ClientDiscount.Text += Convert.ToString(_client.discount);
            Address.Text = _client.address;
            Phone.Text = _client.phone;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Discount.Text != "")
                _order.discount = Convert.ToDecimal(Discount.Text);
            else
                _order.discount = 0;

            _order.phone = Phone.Text;
            _order.address = Address.Text;
            _client.discount -= _order.discount;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Discount_KeyUp(object sender, KeyEventArgs e)
        {
            if (Discount.Text != "")
            {
                if (Convert.ToDecimal(Discount.Text) > _client.discount)
                    Discount.Text = Convert.ToString(_client.discount);

                if (Convert.ToDecimal(Discount.Text) > _sum)
                    Discount.Text = Convert.ToString(_sum);
            }
        }
    }
}
