using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pizza.Logic.DTO;
using Pizza.Form.Total;
using Pizza.Logic.Repositories;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace Pizza.Form.ClientPart
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

        CategorySQLWork _categorySQLWork;//классы работы с БД 
        ProductSQLWork _productSQLWork;
        StatusSQLWork _statusSQLWork;
        OrderSQLWork _orderSQLWork;
        ClientSQLWork _clientSQLWork;

        Client _client;//класс клиента
        Order _order;//класс заказа
        List<Product> _products;//класс продуктов


        public Menu(Client client, ClientSQLWork clientSQLWork, Access accessForm)
        {
            InitializeComponent();

            _accessForm = accessForm;
            _clientSQLWork = clientSQLWork;
            _statusSQLWork = new StatusSQLWork();
            _orderSQLWork = new OrderSQLWork();
            _productSQLWork = new ProductSQLWork();
            _categorySQLWork = new CategorySQLWork();

            _client = client;

            _order = new Order()
            {
                OrderProducts = new List<OrderProducts>(),
                ClientId = _client.Id
            };

            _products = new List<Product>();

            OrderListParam();

            LoadProducts();

            for (int i = 0; i < _products.Count; i++)
            {
                Find.Items.Add($"{_products[i].Name}");
            }
        }

        /// <summary>
        /// Загрузка продуктов в TabControl по катеориям
        /// </summary>
        void LoadProducts()
        {
            //Чтение списка продуктов
            _products = _productSQLWork.ReadProducts();

            using (var rep = new CategorySQLWork())
            {
                rep.GetCategoriesAsync().ContinueWith(t =>
                {
                    var category = t.Result;
                    for (int i = 0; i < category.Count; i++)
                    {
                        if (category[i].ParentCategory == null)
                            CategoryTreeView.Items.Add(category[i]);
                    }
                });
               
            }
        }

        /// <summary>
        /// Добавление разметки в лист заказа
        /// </summary>
        void OrderListParam()
        {
            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
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
            /*if (CategoryTreeView.IsFocused)
            {
                if (CategoryTreeView.SelectedItem != null)
                {
                    if (CategoryTreeView.SelectedItem is Product chooseProduct)
                        if (_order.OrderProducts.Any(x => x.ProductID == chooseProduct.Id))
                            AddProduct(chooseProduct);
                        else
                            CreateProduct(chooseProduct);
                }
            }
            else if (Find.IsFocused)
            {
                if (Find.SelectedItem != null)
                {
                    Product chooseProduct = _products.Where(x => x.Name == Convert.ToString(Find.SelectedItem as string)).FirstOrDefault();
                    if (chooseProduct != null)
                        if (_order.OrderProducts.Any(x => x.ProductID == chooseProduct.Id))
                            AddProduct(chooseProduct);
                        else
                            CreateProduct(chooseProduct);
                }
            }*/

            if (Find.SelectedItem != null)
            {
                Product chooseProduct = _products.Where(x => x.Name == Convert.ToString(Find.SelectedItem as string)).FirstOrDefault();
                if (chooseProduct != null)
                    if (_order.OrderProducts.Any(x => x.ProductID == chooseProduct.Id))
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

                if (nameLabel.Content == chooseProduct.Name)
                {
                    countLabel.Content = Convert.ToInt32(countLabel.Content) + 1;
                    fullPriseLabel.Content = Convert.ToInt32(priseLabel.Content) * Convert.ToInt32(countLabel.Content);

                    OrderProducts orderProd = new OrderProducts
                    {
                        OrderID = _order.Id,
                        ProductID = chooseProduct.Id,
                        CountProducts = Convert.ToInt32(countLabel.Content)
                    };

                    for (int j = 0; j < _order.OrderProducts.Count; j++)
                    {
                        if (_order.OrderProducts[j].ProductID == chooseProduct.Id)
                            _order.OrderProducts[j] = orderProd;
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
            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            Label name = new Label()
            {
                Content = chooseProduct.Name,
                Width = 110
            };
            Label prise = new Label()
            {
                Content = chooseProduct.Price,
                Width = 50
            };
            Label count = new Label()
            {
                Content = "1",
                Width = 30
            };
            Label fullPrise = new Label()
            {
                Content = chooseProduct.Price,
                Width = 50
            };

            panel.Children.Add(name);
            panel.Children.Add(prise);
            panel.Children.Add(count);
            panel.Children.Add(fullPrise);

            OrderList.Items.Add(panel);

            OrderProducts orderProd = new OrderProducts
            {
                OrderID = _order.Id,
                ProductID = chooseProduct.Id,
                CountProducts = 1
            };

            _order.OrderProducts.Add(orderProd);
        }

        /// <summary>
        /// Удаление одной единицы продукта из заказа
        /// </summary>
        void DelItemOne()
        {
            if (OrderList.SelectedIndex > 0)
                for (int i = 0; i < _order.OrderProducts.Count; i++)
                {
                    Label nameLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[0] as Label);
                    Label priseLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[1] as Label);
                    Label countLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[2] as Label);
                    Label fullPriseLabel = ((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[3] as Label);

                    Product chooseProduct = _products.Where(x => x.Name == Convert.ToString(nameLabel.Content)).FirstOrDefault();

                    if (_order.OrderProducts[i].ProductID == chooseProduct.Id)
                    {

                        if (Convert.ToInt32(countLabel.Content) == 1)
                        {
                            _order.OrderProducts.RemoveAt(i);
                            OrderList.Items.RemoveAt(OrderList.SelectedIndex);
                        }
                        else
                        {
                            _order.OrderProducts[i].CountProducts--;
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
                Product chooseProduct = _products.Where(x => x.Name == Convert.ToString(((OrderList.Items[OrderList.SelectedIndex] as StackPanel).Children[0] as Label).Content)).FirstOrDefault();

                _order.OrderProducts.RemoveAll(x => x.ProductID == chooseProduct.Id);

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

            for (int i = 0; i < _order.OrderProducts.Count; i++)
            {
                sum += _order.OrderProducts[i].CountProducts * _products.Where(x => x.Id == _order.OrderProducts[i].ProductID).FirstOrDefault().Price;
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
                    _profile = new Profile(_client, 0)
                    {
                        Visibility = Visibility.Visible
                    };
                    break;
                case "О нас":
                    _info = new Info();
                    _info.Show();
                    break;
                case "Выход":
                    _accessForm.Close();
                    Close();
                    break;
                case "Сменить пользователя":
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
                List<Status> status = _statusSQLWork.ReadStatus();
                _order.StatusId = status[0].Id;

                _order.Date = DateTime.Now;
                _client.Discount += SumUpdate() * 0.03m;

                int lastNom = _orderSQLWork.ReadOrders().Select(x => x.Nom).LastOrDefault();

                if (lastNom == 300)
                    _order.Nom = 0;
                else
                    _order.Nom = lastNom + 1;

                try
                {
                    _orderSQLWork.AddOrder(_order);
                }

                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());

                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            MessageBox.Show(err.ErrorMessage);
                        }
                    }
                }

                OrderList.Items.Clear();
                _order = new Order()
                {
                    OrderProducts = new List<OrderProducts>(),
                    ClientId = _client.Id
                };
                SumUpdate();

                OrderListParam();

                MessageBox.Show("Ваш заказ принят. Ожидайте, пожалуйста.");
            }
        }

        private void AddOne_Click(object sender, RoutedEventArgs e)
        {
            if (OrderList.SelectedIndex > 0)
                AddProduct(_products.Where(x => x.Name == ((OrderList.SelectedItem as StackPanel).Children[0] as Label).Content).FirstOrDefault());
        }

        private void DelAll_Click(object sender, RoutedEventArgs e)
        {
            OrderList.Items.Clear();
            _order.OrderProducts.Clear();
            SumUpdate();

            OrderListParam();
        }

        private void CategoryTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                if (CategoryTreeView.SelectedItem != null)
                {
                    if (CategoryTreeView.SelectedItem is Product chooseProduct)
                        if (_order.OrderProducts.Any(x => x.ProductID == chooseProduct.Id))
                            AddProduct(chooseProduct);
                        else
                            CreateProduct(chooseProduct);

                    SumUpdate();
                }
        }
    }
}
