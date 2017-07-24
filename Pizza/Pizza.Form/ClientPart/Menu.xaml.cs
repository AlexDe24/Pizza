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
        CreateOrder _createOrder;//форма завершения оформления заказа
        Сomment _comment;//форма отзыва
        Info _info;//форма информации
        Browser _browser;//форма браузера
        Profile _profile;//форма профиля
        Access _accessForm;//форма входа
        FileClass _fileWork;//класс работы с файлами
        Client _client;//класс клиента
        Order _order;//класс заказа
        List<Product> _products;//класс продуктов


        public Menu(Client client, FileClass fileWork, Access accessForm)
        {
            InitializeComponent();

            _accessForm = accessForm;
            _fileWork = fileWork;

            _client = client;

            _order = new Order();
            _order.orderProducts = new List<OrderProducts>();
            _order.clientId = _client.id;

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

            List<Category> _category = _fileWork.ReadCategory();

            CategoryTreeView.ItemsSource = _category;

           /* //Заполнение категорий
            List<string> category = new List<string>();

            for (int i = 0; i < _products.Count; i++)
            {
                if (!category.Any(x => x == _products[i].category.name))
                    category.Add(_products[i].category.name);
            }

            

            for (int i = 0; i < category.Count; i++)
            {
                TreeViewItem categoryTab = new TreeViewItem();
                categoryTab.Header = category[i];
                
                CategoryTreeView.Items.Add(categoryTab);

                for (int j = 0; j < _products.Count; j++)
                {
                    if (category[i] == _products[j].category.name)
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
                            Width = 260,
                        };

                        product.Children.Add(productName);
                        product.Children.Add(productPrise);

                        categoryTab.Items.Add(product);
                    }
                }
            }    */
        }

        /// <summary>
        /// Добавление разметки в лист заказа
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
            if (CategoryTreeView.SelectedItem != null)
            {
                Product chooseProduct = CategoryTreeView.SelectedItem as Product;

                if (_order.orderProducts.Any(x => x.productID == chooseProduct.id))
                    AddProduct(chooseProduct);
                else
                    CreateProduct(chooseProduct);
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

                    OrderProducts orderProd = new OrderProducts
                    {
                        orderID = _order.id,
                        productID = chooseProduct.id,
                        countProducts = Convert.ToInt32(countLabel.Content)
                    };

                    for (int j = 0; j < _order.orderProducts.Count; j++)
                    {
                        if (_order.orderProducts[j].productID == chooseProduct.id)
                            _order.orderProducts[j] = orderProd;
                    }
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

            OrderProducts orderProd = new OrderProducts
            {
                orderID = _order.id,
                productID = chooseProduct.id,
                countProducts = 1
            };

            _order.orderProducts.Add(orderProd);
        }

        /// <summary>
        /// Удаление одной единицы продукта из заказа
        /// </summary>
        void DelItemOne()
        {
            if (OrderList.SelectedIndex > 0)
                for (int i = 0; i < _order.orderProducts.Count; i++)
                {
                    Label nameLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[0] as Label);
                    Label priseLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[1] as Label);
                    Label countLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[2] as Label);
                    Label fullPriseLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[3] as Label);

                    Product chooseProduct = _products.Where(x => x.name == nameLabel.Content).First();

                    if (_order.orderProducts[i].productID == chooseProduct.id)
                    {

                        if (Convert.ToInt32(countLabel.Content) == 1)
                        {
                            _order.orderProducts.RemoveAt(i);
                            OrderList.Items.RemoveAt(OrderList.SelectedIndex);
                        }
                        else
                        {
                            _order.orderProducts[i].countProducts--;
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
                Product chooseProduct = _products.Where(x => x.name == ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[0] as Label).Content).First();

                _order.orderProducts.RemoveAll(x => x.productID == chooseProduct.id);

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
        decimal SumUpdate()
        {
            decimal sum = 0;

            for (int i = 0; i < _order.orderProducts.Count; i++)
            {
                sum += _order.orderProducts[i].countProducts * _products.Where(x => x.id == _order.orderProducts[i].productID).FirstOrDefault().price;
            }
            Sum.Content = "Сумма заказа: " + Convert.ToString(sum);

            return sum;
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
                    _profile = new Profile(_client, 0);
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
            _createOrder = new CreateOrder(_order, SumUpdate(), _client);

            if (_createOrder.ShowDialog() == true)
            {
                List<Status> status = _fileWork.ReadStatus();
                _order.status = status[0];

                _order.date = DateTime.Now;
                _client.discount += SumUpdate() * 0.03m;

                int lastNom = _fileWork.ReadOrders().Select(x => x.nom).LastOrDefault();

                if (lastNom == 300)
                    _order.nom = 0;
                else
                    _order.nom = lastNom + 1;

                _fileWork.AddOrder(_order);

                OrderList.Items.Clear();
                _order.orderProducts.Clear();
                SumUpdate();

                OrderListParam();

                MessageBox.Show("Ваш заказ принят. Ожидайте, пожалуйста.");
            }
        }

        private void AddOne_Click(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedIndex > 0)
                AddProduct(_products.Where(x => x.name == ((OrderList.SelectedItem as StackPanel).Children[0] as Label).Content).FirstOrDefault());
        }

        private void DelAll_Click(object sender, RoutedEventArgs e)
        {
            OrderList.Items.Clear();
            _order.orderProducts.Clear();
            SumUpdate();

            OrderListParam();
        }
    }
}
