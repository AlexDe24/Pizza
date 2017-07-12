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
        FileClass _fileWork;

        List<Client> clients;

        public ClientList()
        {
            InitializeComponent();

            _fileWork = new FileClass();

            clients = _fileWork.ReadClients();

            for (int i = 0; i < clients.Count; i++)
            {
                ClientBox.Items.Add(clients[i]);
            }
        }
    }
}
