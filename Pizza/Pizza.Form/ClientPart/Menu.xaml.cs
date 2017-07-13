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
        Сomment _comment;
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
            _fileWork = fileWork;

            _order = new Order();

            //_order.client = client;
            _products = new List<Product>();

            OrderListParam();

            LoadProducts();
        }

        /// <summary>
        /// Загрузка продуктов в TabControl по катеориям
        /// </summary>
        void LoadProducts()
        {
            //Чтение списка продуктов
            _products = _fileWork.ReadProducts();

            //Заполнение категорий
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

        /// <summary>
        /// Добавление разметки в лист каказа
        /// </summary>
        void OrderListParam()
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            Label name = new Label()
            {
                Content = "Название",
                Width = 110
            };
            Label prise = new Label()
            {
                Content = "Цена",
                Width = 50
            };
            Label count = new Label()
            {
                Content = "Кол-во",
                Width = 30
            };
            Label fullPrise = new Label()
            {
                Content = "Сумма",
                Width = 50
            };

            panel.Children.Add(name);
            panel.Children.Add(prise);
            panel.Children.Add(count);
            panel.Children.Add(fullPrise);

            OrderList.Items.Add(panel);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if ((CategoryTabControl.SelectedContent as ListBox).SelectedIndex > 0)
            {
                string ChooseName = (((CategoryTabControl.SelectedContent as ListBox).SelectedItem as StackPanel).Children[0] as TextBlock).Text;

                Product chooseProduct;
                chooseProduct = _products.Where(x => x.name == ChooseName).First();

                if (_order.products.Any(x => x.name == chooseProduct.name))
                    AddProduct(chooseProduct);
                else
                    CreateProduct(chooseProduct);

                _order.products.Add(chooseProduct);
            }

            SumUpdate();
        }

        /// <summary>
        /// Добавление продукта в заказ к уже имеющимся
        /// </summary>
        /// <param name="chooseProduct"></param>
        void AddProduct(Product chooseProduct)
        {
            for (int i = 0; i < OrderList.Items.Count; i++)
            {
                Label nameLabel = ((OrderList.Items[i] as StackPanel).Children[0] as Label);
                Label priseLabel = ((OrderList.Items[i] as StackPanel).Children[1] as Label);
                Label countLabel = ((OrderList.Items[i] as StackPanel).Children[2] as Label);
                Label fullPriseLabel = ((OrderList.Items[i] as StackPanel).Children[3] as Label);

                if (nameLabel.Content == chooseProduct.name)
                {
                    countLabel.Content = Convert.ToInt32(countLabel.Content) + 1;
                    fullPriseLabel.Content = Convert.ToInt32(priseLabel.Content) * Convert.ToInt32(countLabel.Content);
                }
            }
        }

        /// <summary>
        /// Добавление продукта в заказ 
        /// </summary>
        /// <param name="chooseProduct"></param>
        void CreateProduct(Product chooseProduct)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            Label name = new Label()
            {
                Content = chooseProduct.name,
                Width = 110
            };
            Label prise = new Label()
            {
                Content = chooseProduct.price,
                Width = 50
            };
            Label count = new Label()
            {
                Content = "1",
                Width = 30
            };
            Label fullPrise = new Label()
            {
                Content = chooseProduct.price,
                Width = 50
            };

            panel.Children.Add(name);
            panel.Children.Add(prise);
            panel.Children.Add(count);
            panel.Children.Add(fullPrise);

            OrderList.Items.Add(panel);
        }

        /// <summary>
        /// Удаление одной единицы продукта из заказа
        /// </summary>
        void DelItemOne()
        {
            if (OrderList.SelectedIndex > 0)
                for (int i = 0; i < _order.products.Count; i++)
                {
                    Label nameLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[0] as Label);
                    Label priseLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[1] as Label);
                    Label countLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[2] as Label);
                    Label fullPriseLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[3] as Label);

                    if (_order.products[i].name == nameLabel.Content)
                    {
                        _order.products.RemoveAt(i);

                        if (Convert.ToInt32(countLabel.Content) == 1)
                            OrderList.Items.RemoveAt(OrderList.SelectedIndex);
                        else
                        {
                            countLabel.Content = Convert.ToInt32(countLabel.Content) - 1;
                            fullPriseLabel.Content = Convert.ToInt32(priseLabel.Content) * Convert.ToInt32(countLabel.Content);
                        }
                        break;
                    }
                }

            SumUpdate();
        }

        /// <summary>
        /// Удаление продукта из заказа
        /// </summary>
        void DelItemFew()
        {
            if (OrderList.SelectedIndex > 0)
            {
                _order.products.RemoveAll(x => x.name == ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[0] as Label).Content);
                OrderList.Items.RemoveAt(OrderList.SelectedIndex);
            }

            SumUpdate();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Name == "DelOne")
                DelItemOne();
            else
                DelItemFew();
        }

        private void OrderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DelItemOne();
            else
                DelItemFew();
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

            _order.fullPrice = sum;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Header)
            {
                case "Оставить отзыв":
                    _comment = new Сomment();
                    _comment.Show();
                    break;
                case "Инстаграм":
                    _browser = new Browser(Properties.Resources.BrowserInstagram);
                    _browser.Show();
                    break;
                case "Группа Вконтакте":
                    _browser = new Browser(Properties.Resources.BrowserVK);
                    _browser.Show();
                    break;
                case "Профиль":
                    //_profile = new Profile(_order.client);
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

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            _order.date = DateTime.Now;
            _order.condition = "Поступил";
            _fileWork.AddOrder(_order);

            MessageBox.Show("Ваш заказ принят. Ожидайте, пожалуйста.");
        }
    }
}
