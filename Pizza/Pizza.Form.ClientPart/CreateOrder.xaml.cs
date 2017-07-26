using System;
using System.Windows;
using System.Windows.Input;
using Pizza.Logic.DTO;

namespace Pizza.Form.ClientPart
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

            ClientDiscount.Text += Convert.ToString(_client.Discount);
            Address.Text = _client.Address;
            Phone.Text = _client.Phone;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Discount.Text != "")
                _order.Discount = Convert.ToDecimal(Discount.Text);
            else
                _order.Discount = 0;

            _order.Phone = Phone.Text;
            _order.Address = Address.Text;
            _client.Discount -= _order.Discount;
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
                if (Convert.ToDecimal(Discount.Text) > _client.Discount)
                    Discount.Text = Convert.ToString(_client.Discount);

                if (Convert.ToDecimal(Discount.Text) > _sum)
                    Discount.Text = Convert.ToString(_sum);
            }
        }
    }
}
