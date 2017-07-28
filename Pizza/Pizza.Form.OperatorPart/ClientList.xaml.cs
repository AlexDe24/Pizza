using Pizza.Form.Total;
using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pizza.Form.OperatorPart
{
    /// <summary>
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Window
    {
        Registration _registration;
        Profile _profile;
        ClientSQLWork _clientSQLWork;
        List<Client> _allClients;
        List<Client> _clients;

        public ClientList()
        {
            InitializeComponent();

            _clientSQLWork = new ClientSQLWork();
            _clients = new List<Client>();

            ClientBoxUpdate();
        }

        void ClientBoxUpdate()
        {
            //_allClients = _clientSQLWork.ReadClients();
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
                    _clients.Sort((a, b) => a.Name.CompareTo(b.Name));
                    break;
                case "Имя↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Имя↓";
                    _clients.Sort((a, b) => b.Name.CompareTo(a.Name));
                    break;
                case "Фамилия":
                case "Фамилия↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Фамилия↑";
                    _clients.Sort((a, b) => a.Surname.CompareTo(b.Surname));
                    break;
                case "Фамилия↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Фамилия↓";
                    _clients.Sort((a, b) => b.Surname.CompareTo(a.Surname));
                    break;
                case "Отчество":
                case "Отчество↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Отчество↑";
                    _clients.Sort((a, b) => a.Middlename.CompareTo(b.Middlename));
                    break;
                case "Отчество↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Отчество↓";
                    _clients.Sort((a, b) => b.Middlename.CompareTo(a.Middlename));
                    break;
                case "Дата рождения":
                case "Дата рождения↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата рождения↑";
                    _clients.Sort((a, b) => a.BirthDate.CompareTo(b.BirthDate));
                    break;
                case "Дата рождения↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Дата рождения↓";
                    _clients.Sort((a, b) => b.BirthDate.CompareTo(a.BirthDate));
                    break;
                case "Адрес":
                case "Адрес↓":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↑";
                    _clients.Sort((a, b) => a.Address.CompareTo(b.Address));
                    break;
                case "Адрес↑":
                    OrdersListHeaderUpdate();
                    (sender as GridViewColumnHeader).Content = "Адрес↓";
                    _clients.Sort((a, b) => b.Address.CompareTo(a.Address));
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
                string allParam = _allClients[i].Name + _allClients[i].Surname + _allClients[i].Middlename + _allClients[i].Address + _allClients[i].Phone + _allClients[i].BirthDate;
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
