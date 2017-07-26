using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pizza.Logic
{
    public class Client
    {
        public int id { get; set; }
        [Required]
        public string login { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string middlename { get; set; }
        public string password { get; set; } //пароль
        [Required]
        public DateTime birthDate { get; set; } //день рождения
        [Required]
        public string address { get; set; } //номер телефона
        [Required]
        public string phone { get; set; } //номер телефона
        public decimal discount { get; set; } //скидка

        public List<Order> order { get; set; }
    }
}
