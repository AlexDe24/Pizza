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
                client.login = read.ReadLine();
                client.password = read.ReadLine();
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

            write.WriteLine(client.login);
            write.WriteLine(client.password);

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

            clientRedact.birthDate = client.birthDate;

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
            List<Client> clients = _BaseCt.Clients.ToList();

            return clients;
        }

        /// <summary>
        /// Удаление клиента и связанных заказов из базы
        /// </summary>
        /// <param name="client">клиент</param>
        public void DelClient(Client client)
        {
            Client clientDel = _BaseCt.Clients.Where(c => c.login == client.login).FirstOrDefault();

            _BaseCt.Orders.RemoveRange(ReadOrders().Where(x => x.client == clientDel));

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
            List<Product> products = _BaseCt.Products.Include(p => p.category).ToList();

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
        /// <param name="order">заказ</param>
        public void RedactOrder()
        {
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
                .Include(p => p.status)
                .Include(p => p.client)
                .Include(p => p.products)
                .ToList();

            ReadCategory();
            return orders;
        }

        /// <summary>
        /// Удаление заказа из базы
        /// </summary>
        /// <param name="order">заказ</param>
        public void DelOrder(List<Order> orders)
        {
            _BaseCt.Orders.RemoveRange(orders);
            //_BaseCt.SaveChanges();
        }

        //
        //Работа с категориями
        //
        /// <summary>
        /// Чтение списка категорий
        /// </summary>
        public List<Category> ReadCategory()
        {
            List<Category> category = _BaseCt.Category.ToList();

            return category;
        }

        /// <summary>
        /// Создание списка категорий
        /// </summary>
        public void AddCategory(string[] categ)
        {
            List<Category> category = new List<Category>();

            for (int i = 0; i < categ.Length; i++)
            {
                category.Add(new Category { name = categ[i] });
                _BaseCt.Category.Add(category[i]);
            }
            
            _BaseCt.SaveChanges();
        }

        //
        //Работа с состояниями
        //
        /// <summary>
        /// Чтение списка состояний
        /// </summary>
        public List<Status> ReadStatus()
        {
            List<Status> status = _BaseCt.Statuses.ToList();

            return status;
        }

        /// <summary>
        /// Создание списка состояний
        /// </summary>
        public void AddStatus(string[] stat)
        {
            List<Status> status = new List<Status>();

            for (int i = 0; i < stat.Length; i++)
            {
                status.Add(new Status { name = stat[i] });
                _BaseCt.Statuses.Add(status[i]);
            }
            
            _BaseCt.SaveChanges();
        }
    }
}
