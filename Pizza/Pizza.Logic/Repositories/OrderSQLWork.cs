using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pizza.Logic.Repositories
{
    public class OrderSQLWork : IDisposable
    {
        BaseContext _BaseCt;

        public OrderSQLWork()
        {
            _BaseCt = new BaseContext();
        }

        public void Dispose()
        {
            _BaseCt.Dispose();
        }

        /// <summary>
        /// Редактирование заказа
        /// </summary>
        /// <param name="order">заказ</param>
        public void EditOrder()
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
            List<Order> orders = _BaseCt.Orders
                .Include(x => x.Client)
                .Include(x => x.Status)
                .Include(x => x.OrderProducts)
                .ToList();

            return orders;
        }

        /// <summary>
        /// Чтение списка заказов из базы осинхронно
        /// </summary>
        /// <returns>список заказов</returns>
        public async Task<List<Order>> GetOrdersAsync()
        {
            await _BaseCt.Products.LoadAsync().ConfigureAwait(false);

            return await _BaseCt.Orders
                .Include(x => x.Client)
                .Include(x => x.Status)
                .Include(x => x.OrderProducts)
                .ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Чтение списка заказов клиента из базы осинхронно
        /// </summary>
        /// <returns>список заказов</returns>
        public async Task<List<Order>> GetClientOrdersAsync(Client Client)
        {
            await _BaseCt.Products.LoadAsync().ConfigureAwait(false);

            return await _BaseCt.Orders.Where(x => x.ClientId == Client.Id)
                .Include(x => x.Client)
                .Include(x => x.Status)
                .Include(x => x.OrderProducts)
                .ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Чтение списка заказов из базы
        /// </summary>
        /// <returns>список заказов</returns>
        public int GetOrdersLastNom()
        {
            var order = _BaseCt.Orders.ToList().LastOrDefault();

            if (order == null)
                return 0;
            else
                return order.Nom;
        }

        /// <summary>
        /// Удаление заказа из базы
        /// </summary>
        /// <param name="order">заказ</param>
        public void DeleteOrder(List<Order> orders)
        {
            _BaseCt.Orders.RemoveRange(orders);
            _BaseCt.SaveChanges();
        }
    }
}
