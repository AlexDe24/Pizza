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
        /// Загрузка изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageLoad_Click(object sender, RoutedEventArgs e)
        {
            /*string _addres = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            openFileDialog.InitialDirectory = _fileWork.homeDirImage;
            if (openFileDialog.ShowDialog() == true)
                _addres = openFileDialog.FileName;

            if (_addres != null)
            {
                ProfileImage.Source = new BitmapImage(new Uri(_addres));
                newPerson.avatarAddres = _addres;
            }*/
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

            if (GenderM.IsChecked == true)
                newClient.gender = "М";
            else
                newClient.gender = "Ж";

            if (Login.Text == "")
                MessageBox.Show("Введите логин!", "Предупреждение!");
            else
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
                        _fileWork.WriteProfile(newClient);

                        Close();
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                Close();
            }
        }
    }
}
