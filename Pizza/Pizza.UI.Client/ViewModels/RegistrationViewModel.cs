using AutoMapper;
using Caliburn.Micro;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.UI.Client.ViewModels
{
    internal class RegistrationViewModel : Screen
    {
        #region Properties

        public ComboBoxesDatesList Dates { get; set; } = new ComboBoxesDatesList(); //заполнение дат

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } //номер телефона
        public string Phone { get; set; } //номер телефона

        #endregion

        #region UI Commands
        public async Task HandleRegistrationOk()
        {
            ClientSQLWork clientSQLWork = new ClientSQLWork();
            Logic.DTO.Client client = new Logic.DTO.Client();

            //var clientt = Mapper.Map<RegistrationViewModel, Logic.DTO.Client(this)>;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationViewModel, Logic.DTO.Client>());

            var mapper = config.CreateMapper();
            client = mapper.Map<Logic.DTO.Client>(this);
        }
        #endregion
    }
}
