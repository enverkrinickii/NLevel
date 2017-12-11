using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NLevel;
using Client = DAL.Models.Client;

namespace DAL.Repositories
{
    public class ClientRepository : IRepository<Client, NLevel.Client> 
    {
        private StoreContext _container;
        public ClientRepository()
        {
            _container = new StoreContext();
        }
        
        private static Client ToObject(NLevel.Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return new Client
            {
                Surname = client.Surname
            };
        }

        private static NLevel.Client ToEntity(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return new NLevel.Client
            {
                Surname = client.Surname
            };
        }

        public NLevel.Client GetEntity(Client client)
        {
            var entity = _container.Clients.FirstOrDefault(x => x.Surname == client.Surname);
            return entity;
        }

        public void Add(Client dalEntity)
        {
            var client = ToEntity(dalEntity);
            try
            {
                _container.Clients.Add(client);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            SaveChanges();
        }

        public void Remove(Client dalEntity)
        {
            var client = ToEntity(dalEntity);
            try
            {
                _container.Clients.Remove(client);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            SaveChanges();
        }

        public IEnumerable<Client> GetEntities
        {
            get
            {
                var entities = new List<Client>();
                foreach (var client in _container.Clients.Select(x => x))
                {
                    entities.Add(ToObject(client));
                }

                return entities;
            }
        }

        public NLevel.Client GetEntityById(int id)
        {
            var client = _container.Clients.FirstOrDefault(cl => cl.Id == id);
            return client;
        }

        public void SaveChanges()
        {
            _container.SaveChanges();
        }

        public void Update(Client missingName)
        {
            _container.Entry(missingName).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }

        ~ClientRepository()
        {
            Dispose();
        }
    }
}
