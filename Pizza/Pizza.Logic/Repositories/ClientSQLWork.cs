using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic.Repositories
{
    public class ClientSQLWork
    {
        BaseContext _BaseCt;

        public ClientSQLWork()
        {
            _BaseCt = new BaseContext();
        }
        /// <summary>
        /// Редактирование клиента
        /// </summary>
        /// <param name="client">клиент</param>
        public void EditClient(Client client)
        {
            Client clientRedact = _BaseCt.Clients.Where(c => c.Login == client.Login)
                .FirstOrDefault();

            //Обновление профиля
            clientRedact.Name = client.Name;
            clientRedact.Surname = client.Surname;
            clientRedact.Middlename = client.Middlename;
            clientRedact.Password = client.Password;

            clientRedact.BirthDate = client.BirthDate;

            clientRedact.Address = client.Address;
            clientRedact.Phone = client.Phone;

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
        public void DeleteClient(Client client)
        {
            Client clientDel = _BaseCt.Clients.Where(c => c.Login == client.Login).FirstOrDefault();

            _BaseCt.Clients.Remove(clientDel);

            _BaseCt.SaveChanges();
        }

    }
}
