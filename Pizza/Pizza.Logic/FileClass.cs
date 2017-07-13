using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class FileClass
    {
        ClientContext _clientCt;
        ProductContext _productCt;
        OrderContext _orderCt;

        string _cacheDir;

        public FileClass()
        {
            _clientCt = new ClientContext();
            _productCt = new ProductContext();
            _orderCt = new OrderContext();

            _cacheDir = @"Cache\";
        }

        //
        //Работа с кэшем
        //
        /// <summary>
        /// Удаление сохранённого профиля
        /// </summary>
        /// <param name="dir">название рабочего файла</param>
        public void IsLogonFalse(string dir)
        {
            File.Delete(_cacheDir + dir + ".txt");
        }

        /// <summary>
        /// Чтение сохранённого/запомненного профиля
        /// </summary>
        /// <param name="dir">название рабочего файла</param>
        /// <returns></returns>
        public Client IsLogonRead(string dir)
        {
            StreamReader read = new StreamReader(_cacheDir + dir + ".txt");

            Client client = new Client();

            while (!read.EndOfStream)
            {
                client.id = Convert.ToInt32(read.ReadLine());
                client.login = read.ReadLine();
                client.name = read.ReadLine();
                client.surname = read.ReadLine();
                client.middlename = read.ReadLine();
                client.password = read.ReadLine();
                client.birthDateDay = read.ReadLine();
                client.birthDateMonth = read.ReadLine();
                client.birthDateYear = read.ReadLine();
                client.address = read.ReadLine();
                client.phone = read.ReadLine();
            }

            read.Close();

            return client;
        }

        /// <summary>
        /// Запись сохранённого/запомненного профиля
        /// </summary>
        /// <param name="dir">название рабочего файла</param>
        public void IsLogonWrite(Client client, string dir)
        {
            StreamWriter write = new StreamWriter(_cacheDir + dir + ".txt");

            write.WriteLine(client.id);
            write.WriteLine(client.login);
            write.WriteLine(client.name);
            write.WriteLine(client.surname);
            write.WriteLine(client.middlename);
            write.WriteLine(client.password);
            write.WriteLine(client.birthDateDay);
            write.WriteLine(client.birthDateMonth);
            write.WriteLine(client.birthDateYear);
            write.WriteLine(client.address);
            write.WriteLine(client.phone);

            write.Close();
        }
        //
        //Работа с клиентами
        //
        /// <summary>
        /// Редактирование клиента
        /// </summary>
        /// <param name="client">клиент</param>
        public void RedactClient(Client client)
        {
            _clientCt.Clients.Load();

            for (int i = 0; i < _clientCt.Clients.Local.Count; i++)
            {
                if (_clientCt.Clients.Local[i].login == client.login)
                _clientCt.Clients.Local[i] = client;
            }
            
            _clientCt.SaveChanges();
        }

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="client">клиент</param>
        public void AddClient(Client client)
        {
             _clientCt.Clients.Add(client);
             _clientCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка клиентов из базы данных
        /// </summary>
        /// <returns>список клиентов</returns>
        public List<Client> ReadClients()
        {
            List<Client> clients = new List<Client>();

            _clientCt.Clients.Load();

            for (int i = 0; i < _clientCt.Clients.Local.Count; i++)
            {
                clients.Add(_clientCt.Clients.Local[i]);
            }

            return clients;
        }

        /// <summary>
        /// Удаление клиента из базы
        /// </summary>
        /// <param name="client">клиент</param>
        public void DelClient(Client client)
        {
            _clientCt.Clients.Remove(client);
            _clientCt.SaveChanges();
        }

        //
        //Работа с меню
        //
        /// <summary>
        /// Редактирование продукта
        /// </summary>
        /// <param name="product">продукт</param>
        public void RedactProduct(Product product)
        {
            _productCt.Products.Load();

            for (int i = 0; i < _productCt.Products.Local.Count; i++)
            {
                if (_productCt.Products.Local[i].name == product.name)
                    _productCt.Products.Local[i] = product;
            }

            _productCt.SaveChanges();
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <param name="product">продукт</param>
        public void AddProduct(Product product)
        {
            _productCt.Products.Add(product);
            _productCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка подуктов из базы данных
        /// </summary>
        /// <returns>список продуктов</returns>
        public List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();

            _productCt.Products.Load();

            for (int i = 0; i < _productCt.Products.Local.Count; i++)
            {
                products.Add(_productCt.Products.Local[i]);
            }

            return products;
        }

        /// <summary>
        /// Удаление продукта из базы
        /// </summary>
        /// <param name="product">продукт</param>
        public void DelProduct(Product product)
        {
            _productCt.Products.Remove(product);
            _productCt.SaveChanges();
        }

        //
        //Работа с заказами
        //
        /// <summary>
        /// Редактирование заказа
        /// </summary>
        /// <param name="product">заказ</param>
        public void RedactOrder(Order order)
        {
            _orderCt.Orders.Load();

            for (int i = 0; i < _productCt.Products.Local.Count; i++)
            {
                if (_orderCt.Orders.Local[i].id == order.id)
                    _orderCt.Orders.Local[i] = order;
            }

            _orderCt.SaveChanges();
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="product">заказ</param>
        public void AddOrder(Order order)
        {
            _orderCt.Orders.Add(order);
            _orderCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка заказов из базы
        /// </summary>
        /// <returns>список заказов</returns>
        public List<Order> ReadOrders()
        {
            List<Order> orders = new List<Order>();

            _orderCt.Orders.Load();

            for (int i = 0; i < _orderCt.Orders.Local.Count; i++)
            {
                orders.Add(_orderCt.Orders.Local[i]);
            }

            return orders;
        }

        /// <summary>
        /// Удаление заказа из базы
        /// </summary>
        /// <param name="order">заказ</param>
        public void DelOrder(Order order)
        {
            _orderCt.Orders.Remove(order);
            _orderCt.SaveChanges();
        }
    }
}
