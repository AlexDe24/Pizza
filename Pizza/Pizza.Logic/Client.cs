using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class Client
    {
        public int id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string middlename { get; set; }
        public string password { get; set; } //пароль
        public string birthDateDay { get; set; } //день рождения
        public string birthDateMonth { get; set; } //месяц рождения
        public string birthDateYear { get; set; } //год рождения
        public string address { get; set; } //номер телефона
        public string phone { get; set; } //номер телефона

        //public List<Order> order { get; set; }
    }
}
