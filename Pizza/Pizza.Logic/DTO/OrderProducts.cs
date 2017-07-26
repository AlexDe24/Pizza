using System.Collections.Generic;

namespace Pizza.Logic.DTO
{
    public class OrderProducts
    {
        public int Id { get; set; }

        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public int CountProducts { get; set; }

        public Product Products { get; set; }
        public Order Orders { get; set; }
    }
}
