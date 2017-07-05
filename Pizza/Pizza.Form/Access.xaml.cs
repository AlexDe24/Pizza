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
using System.Windows.Threading;
using System.Threading;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для Access.xaml
    /// </summary>
    public partial class Access : Window
    {
        List<Client> _clients;
        FileClass _fileWork;
        Registration _regist;
        List<Client> _findClient;

        TimerCallback _timeCB;
        Timer _stepTimer;

        Color firstColor;
        Color secondColor;

        bool isColorDown;

        public Access()
        {
            InitializeComponent();

            _timeCB = new TimerCallback(TimerTick); //функция таймера
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

            /*_findPerson = new List<PersonInfo>(); //лист для поиска по параметру

            Update();

            try
            {
                //LoginEnter.Text = _fileWork.IsLogonRead("Remember").login;
                PasswordEnter.Focus();
            }
            catch (Exception)
            {
            }

            try
            {
                //_mainForm = new Login(_fileWork.IsLogonRead("Online"), this);

                Visibility = Visibility.Hidden;
                //_mainForm.ShowDialog();
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
            }, DispatcherPriority.Loaded);*/

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

            _clients = _fileWork.ReadProfiles(); //класс данных о пользователе

            LoginEnter.Items.Clear();

            for (int i = 0; i < _clients.Count; i++)
            {
                LoginEnter.Items.Add(_clients[i].login);
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            /*if (LoginEnter.SelectedIndex >= 0)
            {
                _mainForm = new Login(_persons[LoginEnter.SelectedIndex], this);

                try
                {
                    if (IsLog.IsChecked == true)
                    {
                        _fileWork.IsLogonWrite(_persons[LoginEnter.SelectedIndex], "Online");
                    }
                    else
                        _fileWork.IsLogonFalse("Online");

                    if (IsRemember.IsChecked == true)
                    {
                        _fileWork.IsLogonWrite(_persons[LoginEnter.SelectedIndex], "Remember");
                    }
                    else
                        _fileWork.IsLogonFalse("Remember");
                }
                catch (Exception)
                {
                }

                if (_persons[LoginEnter.SelectedIndex].password == PasswordEnter.Password)
                {
                    Visibility = Visibility.Hidden;
                    _mainForm.ShowDialog();
                    Update();
                }
                else
                    MessageBox.Show("Неверный пароль.", "Предупреждение!");

            }
            else
                MessageBox.Show("Пользователь не найден.", "Предупреждение!");
            PasswordEnter.Clear();*/
        }


        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            _regist = new Registration(_clients); //класс регистрации

            //int personsCount = _clients.Count;

            Visibility = Visibility.Hidden;
            _regist.ShowDialog();
            Visibility = Visibility.Visible;

            /*if (personsCount < _fileWork.ReadProfiles().Count)
            {
                _mainForm = new Login(_regist.newPerson, this);

                Visibility = Visibility.Hidden;
                _mainForm.ShowDialog();
                //Close();
            }*/

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
