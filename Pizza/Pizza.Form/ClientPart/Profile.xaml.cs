﻿using System;
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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        Client _client;
        FileClass fileWork;
        bool isEditPassword;

        /// <summary>
        /// Класс управления профилем
        /// </summary>
        /// <param name="clientNow">выбранный пользователь</param>
        public Profile(Client clientNow)
        {
            InitializeComponent();

            fileWork = new FileClass(); //класс работы с файлами
            _client = clientNow; //класс данных о пользователе

            FillingProfile();
        }

        void FillingProfile()
        {
            Name.Content += _client.name;
            Surname.Content += _client.surname;
            Middlename.Content += _client.middlename;

            BirthDate.Content += _client.birthDateDay + "." + _client.birthDateMonth + "." + _client.birthDateYear;

            Gender.Content += _client.gender;
        }

        void FillingСhange()
        {
            if (_client.gender == "М")
                GenderM.IsChecked = true;
            else
                GenderW.IsChecked = true;

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

            try
            {
                birthDay.SelectedIndex = Convert.ToInt32(_client.birthDateDay) - 1;
                birthMonth.SelectedIndex = Convert.ToInt32(_client.birthDateMonth) - 1;
                birthYear.SelectedIndex = Convert.ToInt32(_client.birthDateYear) - 1900;
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

            _client.birthDateDay = birthDay.Text;
            _client.birthDateMonth = birthMonth.Text;
            _client.birthDateYear = birthYear.Text;

            if (GenderM.IsChecked == true)
                _client.gender = "М";
            else
                _client.gender = "Ж";

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
                        fileWork.RedactClient(_client);

                        Close();
                    }
                }
            else
            {
                fileWork.RedactClient(_client);

                Close();
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
    }
}