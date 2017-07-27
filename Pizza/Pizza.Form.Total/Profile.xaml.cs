using System;
using System.Windows;
using Pizza.Logic;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;

namespace Pizza.Form.Total
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        ShortOrderInfo shortOrderInfo;
        Confirmation _confirmation; //форма для подтвержения удаления 
        Client _client; //клиент
        ClientSQLWork _clientSQLWork; //класс работы с файлами данными клиентов
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
            _clientSQLWork = new ClientSQLWork(); //класс работы с файлами
            _client = clientNow; //класс данных о пользователе

            FillingProfile();
        }

        /// <summary>
        /// Заполнение полей информации
        /// </summary>
        void FillingProfile()
        {
            Name.Content += _client.Name;
            Surname.Content += _client.Surname;
            Middlename.Content += _client.Middlename;

            BirthDate.Content += Convert.ToString($"{_client.BirthDate.Day}.{_client.BirthDate.Month}.{_client.BirthDate.Year}");

            Address.Content += _client.Address;
            Phone.Content += _client.Phone;

            Discount.Content += Convert.ToString(_client.Discount);
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

            NameNew.Text = _client.Name;
            SurnameNew.Text = _client.Surname;
            MiddlenameNew.Text = _client.Middlename;

            AddressNew.Text = _client.Address;
            PhoneNew.Text = _client.Phone;

            try
            {
                birthDay.SelectedIndex = Convert.ToInt32(_client.BirthDate.Day) - 1;
                birthMonth.SelectedIndex = Convert.ToInt32(_client.BirthDate.Month) - 1;
                birthYear.SelectedIndex = Convert.ToInt32(_client.BirthDate.Year) - 1900;
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
            _client.Name = NameNew.Text;
            _client.Surname = SurnameNew.Text;
            _client.Middlename = MiddlenameNew.Text;

            DateTime date = new DateTime(Convert.ToInt32(birthYear.Text), Convert.ToInt32(birthMonth.Text), Convert.ToInt32(birthDay.Text));
            _client.BirthDate = date;

            _client.Address = AddressNew.Text;
            _client.Phone = PhoneNew.Text;

            try
            {
                if (isEditPassword)
                    if (Convert.ToString(PasswordOld.SecurePassword) != _client.Password)
                    {
                        MessageBox.Show("Пароль неверный!", "Предупреждение!");
                    }
                    else
                    {
                        if (Convert.ToString(PasswordControl.SecurePassword) != Convert.ToString(PasswordOrig.SecurePassword))
                        {
                            MessageBox.Show("Пароли не совпадают!", "Предупреждение!");
                        }
                        else
                        {
                            _client.Password = Convert.ToString(PasswordOrig.SecurePassword);
                            _clientSQLWork.EditClient(_client);

                            Close();
                        }
                    }
                else
                {
                    _clientSQLWork.EditClient(_client);

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
                _clientSQLWork.DeleteClient(_client);
                Close();
            }

            if (_clearance == 0)
                Application.Current.Shutdown();
            else
                Close();
        }

        private void MyOrders_Click(object sender, RoutedEventArgs e)
        {
            shortOrderInfo = new ShortOrderInfo(_client);
            shortOrderInfo.Show();
        }
    }
}
