using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pizza.Logic.Repositories
{
    public class ClientSQLWork : IDisposable
    {
        BaseContext _BaseCt;

        public ClientSQLWork()
        {
            _BaseCt = new BaseContext();
        }

        public void Dispose()
        {
            _BaseCt.Dispose();
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

        public void EditClientDiscount(Client client)
        {
            Client clientRedact = _BaseCt.Clients.Where(c => c.Login == client.Login)
                .FirstOrDefault();

            //Обновление профиля
            clientRedact.Discount = client.Discount;

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
        public async Task<List<Client>> ReadClientsAsync()
        { 
            return await _BaseCt.Clients.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Client> GetClient(string login, string password)
        {
            return await _BaseCt.Clients.SingleOrDefaultAsync(x => x.Login == login && x.Password == password).ConfigureAwait(false);
        }

        public async Task<bool> IsLoginFree(string login)
        {
            return !(await _BaseCt.Clients.AnyAsync(x => x.Login == login).ConfigureAwait(false));
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
