using Pizza.Logic.DTO;
using Pizza.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic.Factory
{
    public class ClientFactory
    {
        public List<Client> Create()
        {
            using (var clientSQLWork = new ClientSQLWork())
            {
                var client = clientSQLWork.ReadClientsAsync().Result;
                return client;
            }
        }
    }
}
