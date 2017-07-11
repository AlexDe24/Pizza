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
        FileClass _fileWork;
        Order _order;
        List<Product> _products;

        public Menu(Client client, FileClass fileWork)
        {
            InitializeComponent();

            _order = new Order();

            _order.client = client;

            _products = new List<Product>();
            _fileWork = fileWork;

            LoadProducts();
        }

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

                TextBlock Name = new TextBlock()
                {
                    Text = "Наименование",
                    Height = 20,
                    Width = 200,
                };

                TextBlock Prise = new TextBlock()
                {
                    Text = "Цена",
                    Height = 20,
                };

                StackPanel panel = new StackPanel()
                {
                    Background = new SolidColorBrush(Colors.LightSalmon),
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
            if ((CategoryTabControl.SelectedContent as ListBox).SelectedIndex != -1)
            {
                string ChooseName = (((CategoryTabControl.SelectedContent as ListBox).SelectedItem as StackPanel).Children[0] as TextBlock).Text;
                _order.products.Add(_products.Where(x => x.name == ChooseName).First());
                OrderList.Items.Add(_products.Where(x => x.name == ChooseName));
            }

            SumUpdate();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedIndex != -1)
            {
                _order.products.RemoveAt(OrderList.SelectedIndex);
                OrderList.Items.RemoveAt(OrderList.SelectedIndex);
            }

            SumUpdate();
        }

        void SumUpdate()
        {
            double sum = 0;

            for (int i = 0; i < _order.products.Count; i++)
            {
                sum += _order.products[i].price;
            }
            Sum.Content = Convert.ToString(sum);
        }
    }
}
