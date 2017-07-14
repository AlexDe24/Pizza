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
        BaseContext _BaseCt;

        string _cacheDir;

        public FileClass()
        {
            _BaseCt = new BaseContext();

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
            Client clientRedact = _BaseCt.Clients.Where(c => c.login == client.login)
        .FirstOrDefault();

            //Обновление профиля
            clientRedact.name = client.name;
            clientRedact.surname = client.surname;
            clientRedact.middlename = client.middlename;
            clientRedact.password = client.password;

            clientRedact.birthDateDay = client.birthDateDay;
            clientRedact.birthDateMonth = client.birthDateMonth;
            clientRedact.birthDateYear = client.birthDateYear;

            clientRedact.address = client.address;
            clientRedact.phone = client.phone;

            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="client">клиент</param>
        public void AddClient(Client client)
        {
            _BaseCt.Clients.Add(client);
            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка клиентов из базы данных
        /// </summary>
        /// <returns>список клиентов</returns>
        public List<Client> ReadClients()
        {
            List<Client> clients = new List<Client>();

            _BaseCt.Clients.Load();

            for (int i = 0; i < _BaseCt.Clients.Local.Count; i++)
            {
                clients.Add(_BaseCt.Clients.Local[i]);
            }

            return clients;
        }

        /// <summary>
        /// Удаление клиента из базы
        /// </summary>
        /// <param name="client">клиент</param>
        public void DelClient(Client client)
        {
            Client clientDel = _BaseCt.Clients.Where(c => c.login == client.login).FirstOrDefault();

            _BaseCt.Clients.Remove(clientDel);
            _BaseCt.SaveChanges();
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
            Product ProductRedact = _BaseCt.Products.Where(c => c.name == product.name).FirstOrDefault();

            ProductRedact.name = product.name;
            ProductRedact.price = product.price;
            ProductRedact.category = product.category;

            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <param name="product">продукт</param>
        public void AddProduct(Product product)
        {
            _BaseCt.Products.Add(product);
            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка подуктов из базы данных
        /// </summary>
        /// <returns>список продуктов</returns>
        public List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();

            _BaseCt.Products.Load();

            for (int i = 0; i < _BaseCt.Products.Local.Count; i++)
            {
                products.Add(_BaseCt.Products.Local[i]);
            }

            return products;
        }

        /// <summary>
        /// Удаление продукта из базы
        /// </summary>
        /// <param name="product">продукт</param>
        public void DelProduct(Product product)
        {
            _BaseCt.Products.Remove(product);
            _BaseCt.SaveChanges();
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
            Order OrderRedact = _BaseCt.Orders.Where(c => c.id == order.id)
.FirstOrDefault();

            OrderRedact.condition = order.condition;

            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="order">заказ</param>
        public void AddOrder(Order order)
        {
            _BaseCt.Orders.Add(order);
            _BaseCt.SaveChanges();
        }

        /// <summary>
        /// Чтение списка заказов из базы
        /// </summary>
        /// <returns>список заказов</returns>
        public List<Order> ReadOrders()
        {
            List<Order> orders = new List<Order>();

            orders = _BaseCt.Orders
    .Include(p => p.products)
    .Include(p => p.client).ToList();

            return orders;
        }

        /// <summary>
        /// Удаление заказа из базы
        /// </summary>
        /// <param name="order">заказ</param>
        public void DelOrder(Order order)
        {
            _BaseCt.Orders.Remove(order);
            _BaseCt.SaveChanges();
        }
    }
}
