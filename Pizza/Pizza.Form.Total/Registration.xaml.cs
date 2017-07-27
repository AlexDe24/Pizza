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
using Microsoft.Win32;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;

namespace Pizza.Form.Total
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        List<Client> _allClients;

        ClientSQLWork _clientSQLWork;

        public Client newClient;

        /// <summary>
        /// Класс управления регистрацией
        /// </summary>
        /// <param name="allPersons">список всех пользователей для проверки уникальности логина</param>
        public Registration(List<Client> allClients)
        {
            InitializeComponent();

            _allClients = allClients; //список всех пользователей для проверки логина*/
            _clientSQLWork = new ClientSQLWork(); //класс работы с файлами

            newClient = new Client(); //класс данных о пользователе

            for (int i = 1; i <= 31; i++)
            {
                birthdayDay.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                birthdayMonth.Items.Add(i);
            }
            for (int i = 0; i < 120; i++)
            {
                birthdayYear.Items.Add(i + 1900);
            }
        }
        /// <summary>
        /// При нажатии кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            newClient.Name = Name.Text;
            newClient.Surname = Surname.Text;
            newClient.Middlename = Middlename.Text;

            try
            {
                DateTime date = new DateTime(Convert.ToInt32(birthdayYear.Text), Convert.ToInt32(birthdayMonth.Text), Convert.ToInt32(birthdayDay.Text));
                newClient.BirthDate = date;
            }
            catch (Exception)
            {

            }

            newClient.Address = Address.Text;
            newClient.Phone = Phone.Text;

            try
            {
                if (_allClients.Any(x => x.Login == Login.Text))
                    MessageBox.Show("Логин уже занят!", "Предупреждение!");
                else
                {
                    newClient.Login = Login.Text;
                    if (Convert.ToString(PasswordOrig.SecurePassword) != Convert.ToString(PasswordControl.SecurePassword))
                    {
                        MessageBox.Show("Пароли не совпадают!", "Предупреждение!");
                    }
                    else
                    {
                        newClient.Password = Convert.ToString(PasswordOrig.SecurePassword);
                        _clientSQLWork.AddClient(newClient);

                        Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Обязательные поля не заполнены!", "Предупреждение!");
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
