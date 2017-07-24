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
using System.Data.Entity.Validation;
using Pizza.Form.ClientPart;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        ShortOrderInfo shortOrderInfo;
        Confirmation _confirmation; //форма для подтвержения удаления 
        Client _client; //клиент
        FileClass _fileWork; //класс работы с файлами
        bool isEditPassword; //меняется ли пароль
        int _clearance;

        /// <summary>
        /// Класс управления профилем
        /// </summary>
        /// <param name="clientNow">выбранный пользователь</param>
        public Profile(Client clientNow, int clearance)
        {
            InitializeComponent();

            _clearance = clearance;
            _fileWork = new FileClass(); //класс работы с файлами
            _client = clientNow; //класс данных о пользователе

            FillingProfile();
        }

        /// <summary>
        /// Заполнение полей информации
        /// </summary>
        void FillingProfile()
        {
            Name.Content += _client.name;
            Surname.Content += _client.surname;
            Middlename.Content += _client.middlename;

            BirthDate.Content += Convert.ToString($"{_client.birthDate.Day}.{_client.birthDate.Month}.{_client.birthDate.Year}");

            Address.Content += _client.address;
            Phone.Content += _client.phone;

            Discount.Content += Convert.ToString(_client.discount);
        }

        /// <summary>
        /// Заполнение полей реактирования
        /// </summary>
        void FillingСhange()
        {
            for (int i = 1; i <= 31; i++)
                {
                    birthDay.Items.Add(i);
                }
            for (int i = 1; i <= 12; i++)
            {
                birthMonth.Items.Add(i);
            }
            for (int i = 0; i < 120; i++)
            {
                birthYear.Items.Add(i + 1900);
            }

            NameNew.Text = _client.name;
            SurnameNew.Text = _client.surname;
            MiddlenameNew.Text = _client.middlename;

            AddressNew.Text = _client.address;
            PhoneNew.Text = _client.phone;

            try
            {
                birthDay.SelectedIndex = Convert.ToInt32(_client.birthDate.Day) - 1;
                birthMonth.SelectedIndex = Convert.ToInt32(_client.birthDate.Month) - 1;
                birthYear.SelectedIndex = Convert.ToInt32(_client.birthDate.Year) - 1900;
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Срабатывает при нажатии кнопки "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit.Content = "Сохранить";

            FillingСhange();
            ProfilePanel.Visibility = Visibility.Visible;
            EditPassword.Visibility = Visibility.Visible;

            Edit.Click -= Edit_Click;
            Edit.Click += Save_Click;
        }

        /// <summary>
        /// Срабатывает при нажатии кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _client.name = NameNew.Text;
            _client.surname = SurnameNew.Text;
            _client.middlename = MiddlenameNew.Text;

            _client.birthDate.AddDays(Convert.ToDouble(birthDay.Text));
            _client.birthDate.AddMonths(Convert.ToInt32(birthMonth.Text));
            _client.birthDate.AddYears(Convert.ToInt32(birthYear.Text));

            _client.address = AddressNew.Text;
            _client.phone = PhoneNew.Text;

            try
            {
                if (isEditPassword)
                    if (PasswordOld.Password != _client.password)
                    {
                        MessageBox.Show("Пароль неверный!", "Предупреждение!");
                    }
                    else
                    {
                        if (PasswordControl.Password != PasswordOrig.Password)
                        {
                            MessageBox.Show("Пароли не совпадают!", "Предупреждение!");
                        }
                        else
                        {
                            _client.password = PasswordOrig.Password;
                            _fileWork.RedactClient(_client);

                            Close();
                        }
                    }
                else
                {
                    _fileWork.RedactClient(_client);

                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Обязательные поля не заполнены!", "Предупреждение!");
            }

        }

        /// <summary>
        /// Срабатывает при нажатии кнопки "Сменить пароль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditPassword)
            {
                isEditPassword = true;

                PasswordPanel.Visibility = Visibility.Visible;
                PasswordPanelLabel.Visibility = Visibility.Visible;
            }
            else
            {
                isEditPassword = false;

                PasswordPanel.Visibility = Visibility.Hidden;
                PasswordPanelLabel.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Срабатывает при нажатии кнопки Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            _confirmation = new Confirmation();

            if (_confirmation.ShowDialog() == true)
            {
                _fileWork.DelClient(_client);
                Close();
            }

            if (_clearance == 0)
                Application.Current.Shutdown();
            else
                Close();
        }

        private void MyOrders_Click(object sender, RoutedEventArgs e)
        {
            shortOrderInfo = new ShortOrderInfo(_fileWork, _client);
            shortOrderInfo.Show();
        }
    }
}
