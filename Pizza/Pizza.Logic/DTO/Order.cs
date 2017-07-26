using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Pizza.Logic.DTO
{
    public class Order
    {
        public int Id { get; set; }
        public int Nom { get; set; }

        public DateTime Date { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }

        public decimal Discount { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual List<OrderProducts> OrderProducts { get; set; }

        public Order()
        {
            Status = new Status();
        }
    }

    internal class OrderETC : EntityTypeConfiguration<Order>
    {
        public OrderETC()
        {
            HasRequired(x => x.Client)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ClientId)
                .WillCascadeOnDelete(true);
        }
    }

}
