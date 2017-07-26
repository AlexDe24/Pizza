using Pizza.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Pizza.Logic.Repositories
{
    public class OrderSQLWork
    {
        BaseContext _BaseCt;

        public OrderSQLWork()
        {
            _BaseCt = new BaseContext();
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
