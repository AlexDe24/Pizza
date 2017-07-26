using Pizza.Logic;
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

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Window
    {
        Registration _registration;
        Profile _profile;
        FileClass _fileWork;
        List<Client> _allClients;
        List<Client> _clients;

        public ClientList()
        {
            InitializeComponent();

            _fileWork = new FileClass();
            _clients = new List<Client>();

            ClientBoxUpdate();
        }

        void ClientBoxUpdate()
        {
            _allClients = _fileWork.ReadClients();
            _clients.Clear();

            for (int i = 0; i < _allClients.Count; i++)
            {
                _clients.Add(_allClients[i]);
            }

            ClientListView.Items.Clear();

            for (int i = 0; i < _clients.Count; i++)
            {
                ClientListView.Items.Add(_clients[i]);
            }
        }

        private void RedactClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientListView.SelectedIndex != -1)
            {
                _profile = new Profile(_clients[ClientListView.SelectedIndex], 1);
                _profile.Show();
            }
        }

        private void RegistClient_Click(object sender, RoutedEventArgs e)
        {
            _registration = new Registration(_clients);
            _registration.Show();
        }

        private void UpdateClients_Click(object sender, RoutedEventArgs e)
        {
            ClientBoxUpdate();
        }


        void OrdersListHeaderUpdate()
        {
            Name.Content = "Имя";
            Surname.Content = "Фамилия";
            Middlename.Content = "Отчество";

            Date.Content = "Дата рождения";
            Address.Content = "Адрес";

        }

        private void ClientListSort(object sender, RoutedEventArgs e)
        {
            switch ((sender as GridViewColumnHeader).Content)
            {
                case "Имя":
                case "Имя↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Имя↑";
                    _clients.Sort((a, b) => a.name.CompareTo(b.name));
                    break;
                case "Имя↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Имя↓";
                    _clients.Sort((a, b) => b.name.CompareTo(a.name));
                    break;
                case "Фамилия":
                case "Фамилия↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Фамилия↑";
                    _clients.Sort((a, b) => a.surname.CompareTo(b.surname));
                    break;
                case "Фамилия↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Фамилия↓";
                    _clients.Sort((a, b) => b.surname.CompareTo(a.surname));
                    break;
                case "Отчество":
                case "Отчество↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Отчество↑";
                    _clients.Sort((a, b) => a.middlename.CompareTo(b.middlename));
                    break;
                case "Отчество↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Отчество↓";
                    _clients.Sort((a, b) => b.middlename.CompareTo(a.middlename));
                    break;
                case "Дата рождения":
                case "Дата рождения↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата рождения↑";
                    _clients.Sort((a, b) => a.birthDate.CompareTo(b.birthDate));
                    break;
                case "Дата рождения↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата рождения↓";
                    _clients.Sort((a, b) => b.birthDate.CompareTo(a.birthDate));
                    break;
                case "Адрес":
                case "Адрес↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↑";
                    _clients.Sort((a, b) => a.address.CompareTo(b.address));
                    break;
                case "Адрес↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↓";
                    _clients.Sort((a, b) => b.address.CompareTo(a.address));
                    break;
                default:
                    break;
            }

            ClientListView.Items.Clear();

            for (int i = 0; i < _clients.Count; i++)
            {
                ClientListView.Items.Add(_clients[i]);
            }
        }

        private void Find_KeyUp(object sender, KeyEventArgs e)
        {
            _clients.Clear();

            var findParam = Find.Text.Split(' ');

            for (int i = 0; i < _allClients.Count; i++)
            {
                string allParam = _allClients[i].name + _allClients[i].surname + _allClients[i].middlename + _allClients[i].address + _allClients[i].phone + _allClients[i].birthDate;
                bool contains = true;

                for (int j = 0; j < findParam.Length; j++)
                {
                    if (allParam.Contains(findParam[j]) != true)
                        contains = false;
                }

                if (contains == true)
                    _clients.Add(_allClients[i]);

            }

            ClientListView.Items.Clear();

            for (int i = 0; i < _clients.Count; i++)
            {
                ClientListView.Items.Add(_clients[i]);
            }
        }
    }
}
