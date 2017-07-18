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

        List<Client> clients;

        public ClientList()
        {
            InitializeComponent();

            _fileWork = new FileClass();

            ClientBoxUpdate();
        }

        void ClientBoxUpdate()
        {
            clients = _fileWork.ReadClients();

            ClientBox.Items.Clear();

            for (int i = 0; i < clients.Count; i++)
            {
                ClientBox.Items.Add(clients[i]);
            }
        }

        private void RedactClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientBox.SelectedIndex != -1)
            {
                _profile = new Profile(clients[ClientBox.SelectedIndex], 1);
                _profile.Show();
            }
        }

        private void RegistClient_Click(object sender, RoutedEventArgs e)
        {
            _registration = new Registration(clients);
            _registration.Show();
        }

        private void UpdateClients_Click(object sender, RoutedEventArgs e)
        {
            ClientBoxUpdate();
        }
    }
}
