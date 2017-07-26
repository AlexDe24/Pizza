using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Pizza.Logic.DTO
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public string Password { get; set; } //пароль
        [Required]
        public DateTime BirthDate { get; set; } //день рождения
        [Required]
        public string Address { get; set; } //номер телефона
        [Required]
        public string Phone { get; set; } //номер телефона
        public decimal Discount { get; set; } //скидка

        public List<Order> Orders { get; set; }
    }

    internal class ClientETC : EntityTypeConfiguration<Client>
    {
        public ClientETC()
        {
            HasMany(x => x.Orders)
                .WithRequired(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .WillCascadeOnDelete(true);
        }
    }
}
