﻿using AutoMapper;
using Caliburn.Micro;
using Pizza.Logic;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pizza.UI.Client.ViewModels
{
    internal class RegistrationViewModel : Screen
    {
        #region Properties

        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; } //номер телефона
        public string Phone { get; set; } //номер телефона

        #endregion

        #region Constructor

        internal RegistrationViewModel()
        {
            BirthDate = DateTime.Now;

            DisplayName = "Регистрация";
        }

        #endregion

        #region UI Commands

        public async Task HandleRegistrationOk(PasswordBox passwordOrig, PasswordBox passwordControl)
        {
            using (var repository = new ClientSQLWork())
            {
                var isLoginFree = await repository.IsLoginFree(Login).ConfigureAwait(false);
                if(!isLoginFree)
                {
                    MessageBox.Show(Properties.Resources.LoginBusy, "Внимание!");
                    return;
                }
            }

            PasswordClass passwordClass = new PasswordClass();

            if (passwordClass.Base64Encode(passwordOrig.Password) == passwordClass.Base64Encode(passwordControl.Password))
            {
                try
                {
                    Password = passwordClass.Base64Encode(passwordOrig.Password);

                    var client = new Logic.DTO.Client();

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationViewModel, Logic.DTO.Client>());
                    var mapper = config.CreateMapper();
                    client = mapper.Map<Logic.DTO.Client>(this);

                    ClientSQLWork clientSQLWork = new ClientSQLWork();
                    clientSQLWork.AddClient(client);

                    TryClose();
                }
                catch (Exception)
                {
                    MessageBox.Show(Properties.Resources.RequiredParameters, "Внимание!");
                }

            }
        }

        #endregion
    }
}
