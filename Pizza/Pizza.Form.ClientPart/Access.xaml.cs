using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;
using Pizza.Logic.DTO;
using Pizza.Form.Total;
using Pizza.Logic.Repositories;
using Pizza.Form.ClientPart.Properties;

namespace Pizza.Form.ClientPart
{
    /// <summary>
    /// Логика взаимодействия для Access.xaml
    /// </summary>
    public partial class Access : Window
    {
        Menu _chooseMenu;
        List<Client> _clients;
        ClientSQLWork _clientSQLWork;
        Registration _regist;

        Timer _stepTimer;

        Color _firstColor;
        Color _secondColor;

        bool _isColorDown;

        public Access()
        {
            InitializeComponent();

            _stepTimer = new Timer(TimerTick, null, 0, 10); //таймер

            _clients = new List<Client>();

            _clientSQLWork = new ClientSQLWork(); //класс работы с файлами

            _isColorDown = false;

            _firstColor = new Color()
            {
                A = 255,
                R = 255,
                G = 235,
                B = 110
            };

            _secondColor = new Color()
            {
                A = 255,
                R = 255,
                G = 195,
                B = 160
            };

            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop()
            {
                Color = _firstColor,
                Offset = 0.0
            });
            gsc.Add(new GradientStop()
            {
                Color = _secondColor,
                Offset = 1
            });

            Background = new LinearGradientBrush(gsc, 0)
            {
                StartPoint = new Point(0.5, 0),
                EndPoint = new Point(0.5, 1)
            };

            LoginEnter.Text = Settings.Default.LoginData;

            Update();

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
                if (_secondColor.B >= 235)
                {
                    _isColorDown = true;
                }
                else if (_secondColor.B <= 145)
                {
                    _isColorDown = false;
                }

                if (_isColorDown == false)
                {
                    _firstColor.B++;
                    _secondColor.B++;
                }
                else if (_isColorDown == true)
                {
                    _firstColor.B--;
                    _secondColor.B--;
                }

                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop()
                {
                    Color = _firstColor,
                    Offset = 0.0
                });
                gsc.Add(new GradientStop()
                {
                    Color = _secondColor,
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
            _clients = _clientSQLWork.ReadClients(); //класс данных о пользователе

            LoginEnter.Items.Clear();

            for (int i = 0; i < _clients.Count; i++)
            {
                LoginEnter.Items.Add(_clients[i].Login);
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (LoginEnter.SelectedIndex >= 0)
            {
                _chooseMenu = new Menu(_clients[LoginEnter.SelectedIndex], _clientSQLWork, this);

                if (IsRemember.IsChecked == true)
                {
                    Settings.Default.LoginData = LoginEnter.Text;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.LoginData = "";
                    Settings.Default.Save();
                }

                if (_clients[LoginEnter.SelectedIndex].Password == Convert.ToString(PasswordEnter.SecurePassword))
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
