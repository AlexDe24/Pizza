using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Logic
{
    public class FileClass
    {
        ClientContext db;

        public FileClass()
        {
            db = new ClientContext();
        }

        public void WriteProfile(Client client)
        {
             // добавляем их в бд
             db.Clients.Add(client);
             db.SaveChanges();
        }

        public List<Client> ReadProfiles()
        {
            List<Client> clients = new List<Client>();

            db.Clients.Load();

            for (int i = 0; i < db.Clients.Local.Count; i++)
            {
                clients.Add(db.Clients.Local[i]);
            }

            return clients;
        }

        public void DelProfile(Client client)
        {
            db.Clients.Remove(client);
        }
    }
}
