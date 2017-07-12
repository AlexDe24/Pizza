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
using Pizza.Form.ClientPart;

namespace Pizza.Form
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        Info _info;
        Browser _browser;
        Profile _profile;
        Access _accessForm;
        FileClass _fileWork;
        Order _order;
        List<Product> _products;

        public Menu(Client client, FileClass fileWork, Access accessForm)
        {
            InitializeComponent();

            _accessForm = accessForm;
            _order = new Order();

            _order.client = client;

            _products = new List<Product>();
            _fileWork = fileWork;

            LoadProducts();
        }

        /// <summary>
        /// Загрузка продуктов в TabControl по катеориям
        /// </summary>
        void LoadProducts()
        {
            _products = _fileWork.ReadProducts();

            List<string> category = new List<string>();

            for (int i = 0; i < _products.Count; i++)
            {
                if (!category.Any(x => x == _products[i].category))
                    category.Add(_products[i].category);
            }

            for (int i = 0; i < category.Count; i++)
            {
                TabItem categoryTab = new TabItem();
                categoryTab.Header = category[i];

                CategoryTabControl.Items.Add(categoryTab);

                ListBox productsList = new ListBox();

                productsList.MouseDoubleClick += Add_Click;

                TextBlock Name = new TextBlock()
                {
                    Foreground = new SolidColorBrush(Colors.DarkSalmon),
                    Text = "Наименование",
                    Height = 20,
                    Width = 200,
                };

                TextBlock Prise = new TextBlock()
                {
                    Foreground = new SolidColorBrush(Colors.DarkSalmon),
                    Text = "Цена",
                    Height = 20,
                };

                StackPanel panel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Width = 300,
                };

                panel.Children.Add(Name);
                panel.Children.Add(Prise);

                productsList.Items.Add(panel);

                for (int j = 0; j < _products.Count; j++)
                {
                    if (category[i] == _products[j].category)
                    {
                        TextBlock productName = new TextBlock()
                        {
                            Text = _products[j].name,
                             Height = 20,
                             Width = 200,
                        };

                        TextBlock productPrise = new TextBlock()
                        {
                            Text = Convert.ToString(_products[j].price),
                            Height = 20,
                        };

                        StackPanel product = new StackPanel()
                        {
                            Orientation = Orientation.Horizontal,
                            Width = 300,
                        };

                        product.Children.Add(productName);
                        product.Children.Add(productPrise);

                        productsList.Items.Add(product);
                    }
                }

                categoryTab.Content = productsList;
            }           
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if ((CategoryTabControl.SelectedContent as ListBox).SelectedIndex > 0)
            {
                string ChooseName = (((CategoryTabControl.SelectedContent as ListBox).SelectedItem as StackPanel).Children[0] as TextBlock).Text;
                _order.products.Add(_products.Where(x => x.name == ChooseName).First());
                OrderList.Items.Add(_products.Where(x => x.name == ChooseName));
            }

            SumUpdate();
        }

        /// <summary>
        /// Удаление продукта из заказа
        /// </summary>
        void DelItem()
        {
            if (OrderList.SelectedIndex != -1)
            {
                _order.products.RemoveAt(OrderList.SelectedIndex);
                OrderList.Items.RemoveAt(OrderList.SelectedIndex);
            }

            SumUpdate();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            DelItem();
        }

        private void OrderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DelItem();
        }

        /// <summary>
        /// Обновление значения общей суммы
        /// </summary>
        void SumUpdate()
        {
            double sum = 0;

            for (int i = 0; i < _order.products.Count; i++)
            {
                sum += _order.products[i].price;
            }
            Sum.Content = "Сумма заказа: " + Convert.ToString(sum);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Header)
            {
                case "Инстаграм":
                    _browser = new Browser(Properties.Resources.BrowserInstagram);
                    _browser.Show();
                    break;
                case "Группа Вконтакте":
                    _browser = new Browser(Properties.Resources.BrowserVK);
                    _browser.Show();
                    break;
                case "Профиль":
                    _profile = new Profile(_order.client);
                    _profile.Visibility = Visibility.Visible;
                    break;
                case "О нас":
                    _info = new Info();
                    _info.Show();
                    break;
                case "Закрыть":
                    _accessForm.Close();
                    Close();
                    break;
                case "Выйти":
                    _fileWork.IsLogonFalse("Online");
                    _accessForm.Visibility = Visibility.Visible;
                    Close();
                    break;
                default:
                    break;
            }
            
        }
    }
}
