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

namespace Pizza.Form
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

        FileClass _fileWork;

        public Client newClient;

        /// <summary>
        /// Класс управления регистрацией
        /// </summary>
        /// <param name="allPersons">список всех пользователей для проверки уникальности логина</param>
        public Registration(List<Client> allClients)
        {
            InitializeComponent();

            _allClients = allClients; //список всех пользователей для проверки логина*/
            _fileWork = new FileClass(); //класс работы с файлами

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
            newClient.name = Name.Text;
            newClient.surname = Surname.Text;
            newClient.middlename = Middlename.Text;

            newClient.birthDateDay = birthdayDay.Text;
            newClient.birthDateMonth = birthdayMonth.Text;
            newClient.birthDateYear = birthdayYear.Text;

            newClient.address = Address.Text;
            newClient.phone = Phone.Text;

            try
            {
                if (_allClients.Any(x => x.login == Login.Text))
                    MessageBox.Show("Логин уже занят!", "Предупреждение!");
                else
                {
                    newClient.login = Login.Text;
                    if (PasswordOrig.Password != PasswordControl.Password)
                    {
                        MessageBox.Show("Пароли не совпадают!", "Предупреждение!");
                    }
                    else
                    {
                        newClient.password = PasswordOrig.Password;
                        _fileWork.AddClient(newClient);

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
