using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class OrderProducts
    {
        [Key, ForeignKey("Order")]
        [Column("orderId", Order = 0)]
        public int orderID { get; set; }

        [Key, ForeignKey("Product")]
        public int productID { get; set; }

        public int quantity { get; set; }

        // Ссылка на заказ
        public Order order { get; set; }

        // Ссылка на товар
        public Product product { get; set; }
    }
}
