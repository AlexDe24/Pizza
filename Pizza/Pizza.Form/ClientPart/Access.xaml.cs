﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Pizza.Logic;
using System.Windows.Threading;
using System.Threading;

namespace Pizza.Form.ClientPart
{
    /// <summary>
    /// Логика взаимодействия для Access.xaml
    /// </summary>
    public partial class Access : Window
    {
        Menu _chooseMenu;
        List<Client> _clients;
        FileClass _fileWork;
        Registration _regist;

        Timer _stepTimer;

        Color firstColor;
        Color secondColor;

        bool isColorDown;

        public Access()
        {
            InitializeComponent();

            _stepTimer = new Timer(TimerTick, null, 0, 10); //таймер

            _clients = new List<Client>();

            _fileWork = new FileClass(); //класс работы с файлами

            isColorDown = false;

            firstColor = new Color()
            {
                A = 255,
                R = 255,
                G = 235,
                B = 110
            };

            secondColor = new Color()
            {
                A = 255,
                R = 255,
                G = 195,
                B = 160
            };

            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop()
            {
                Color = firstColor,
                Offset = 0.0
            });
            gsc.Add(new GradientStop()
            {
                Color = secondColor,
                Offset = 1
            });

            Background = new LinearGradientBrush(gsc, 0)
            {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1)
            };

            Update();

            try
            {
                LoginEnter.Text = _fileWork.IsLogonRead("Remember").login;
                PasswordEnter.Focus();
            }
            catch (Exception)
            {
            }

            try
            {
                _chooseMenu = new Menu(_fileWork.ReadClients().Where(x => x.login == _fileWork.IsLogonRead("Online").login).First(), _fileWork, this);

                Visibility = Visibility.Hidden;
                _chooseMenu.Show();
            }
            catch (Exception)
            {
            }

            Dispatcher.BeginInvoke((Action)delegate
            {
                TextBox textBox = GetChildFromVisualTree(LoginEnter, typeof(TextBox)) as TextBox;
                if (textBox != null)
                {
                    textBox.Focus();
                }
            }, DispatcherPriority.Loaded);
        }

        /// <summary>
        /// Страшная магия устанвливающая фокус на комбо бокс
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public DependencyObject GetChildFromVisualTree(DependencyObject parent, Type objectType)
        {
            if (parent == null)
                return null;

            DependencyObject returnObject = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject visualElement = VisualTreeHelper.GetChild(parent, i);
                if (objectType.IsInstanceOfType(visualElement))
                {
                    return visualElement;
                }
                else
                {
                    returnObject = GetChildFromVisualTree(visualElement, objectType);
                    if (returnObject != null)
                    {
                        return returnObject;
                    }
                }
            }
            return null;
        }

        void TimerTick(object state)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                if (secondColor.B >= 235)
                {
                    isColorDown = true;
                }
                else if (secondColor.B <= 145)
                {
                    isColorDown = false;
                }

                if (isColorDown == false)
                {
                    firstColor.B++;
                    secondColor.B++;
                }
                else if (isColorDown == true)
                {
                    firstColor.B--;
                    secondColor.B--;
                }

                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop()
                {
                    Color = firstColor,
                    Offset = 0.0
                });
                gsc.Add(new GradientStop()
                {
                    Color = secondColor,
                    Offset = 1
                });

                Background = new LinearGradientBrush(gsc, 0)
                {
                    StartPoint = new Point(0.5, 0),
                    EndPoint = new Point(0.5, 1)
                };
            }));

        }

        void Update()
        {
            _clients = _fileWork.ReadClients(); //класс данных о пользователе

            LoginEnter.Items.Clear();

            for (int i = 0; i < _clients.Count; i++)
            {
                LoginEnter.Items.Add(_clients[i].login);
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (LoginEnter.SelectedIndex >= 0)
            {
                _chooseMenu = new Menu(_clients[LoginEnter.SelectedIndex], _fileWork, this);

                if (IsLog.IsChecked == true)
                {
                    _fileWork.IsLogonWrite(_clients[LoginEnter.SelectedIndex], "Online");
                }
                else
                    _fileWork.IsLogonFalse("Online");

                if (IsRemember.IsChecked == true)
                {
                    _fileWork.IsLogonWrite(_clients[LoginEnter.SelectedIndex], "Remember");
                }
                else
                    _fileWork.IsLogonFalse("Remember");

                try
                {
                    
                }
                catch (Exception)
                {
                }

                if (_clients[LoginEnter.SelectedIndex].password == PasswordEnter.Password)
                {
                    Visibility = Visibility.Hidden;
                    _chooseMenu.Show();
                    Update();
                }
                else
                    MessageBox.Show("Неверный пароль.", "Предупреждение!");
            }
            else
                MessageBox.Show("Пользователь не найден.", "Предупреждение!");
            PasswordEnter.Clear();
        }


        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            _regist = new Registration(_clients); //класс регистрации

            Visibility = Visibility.Hidden;
            _regist.ShowDialog();
            Visibility = Visibility.Visible;

            Update();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoginEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                PasswordEnter.Focus();
        }

        private void PasswordEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Enter_Click(null, null);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
