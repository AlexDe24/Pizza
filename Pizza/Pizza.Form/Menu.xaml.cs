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
using System.Data.Entity;
using Pizza.Logic;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        ClientContext db;

        public Menu()
        {
            InitializeComponent();

            db = new ClientContext();

                // создаем два объекта User
                /* Clients user1 = new Clients { Name = "Tom", Age = 33 };
                 Clients user2 = new Clients { Name = "Sam", Age = 26 };

                 // добавляем их в бд
                 db.Clients.Add(user1);
                 db.Clients.Add(user2);
                 db.SaveChanges();*/

            // получаем объекты из бд и выводим на консоль


           /* var clients = db.Clients;
            foreach (Clients u in clients)
            {
                ListBox.Items.Add(u.Id + "." + u.Name + " - " + u.Age);
            }*/

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*if (ListBox.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ListBox.SelectedItems.Count; i++)
                {
                    Clients phone = ListBox.SelectedItems[i] as Clients;

                    if (phone != null)
                    {
                        db.Clients.Remove(phone);
                    }
                }
            }*/

         

            db.Clients.Remove(db.Clients.Local[ListBox.SelectedIndex]);

            db.SaveChanges();
        }
    }
}
