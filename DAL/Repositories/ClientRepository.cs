using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using NLevel;
using ClientDTO = DAL.Models.ClientDTO;
using AutoMapper;

namespace DAL.Repositories
{
    public class ClientRepository : IRepository<ClientDTO, Client> 
    {
        private StoreContext _container;
        public ClientRepository()
        {
            _container = new StoreContext();
        }

        private static ClientDTO ToObject(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return Mapper.Map<ClientDTO>(client);
        }

        private static Client ToEntity(ClientDTO client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return Mapper.Map<Client>(client);
        }

        public ClientDTO GetEntity(ClientDTO client)
        {
            var entity = _container.Clients.FirstOrDefault(x => x.Id == client.Id);
            return ToObject(entity);
        }

        public void Add(ClientDTO dalEntity)
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

        public void Remove(int id)
        {
            var client = _container.Clients.Find(id);
            try
            {
                _container.Clients.Remove(client);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception);
            }
            SaveChanges();
        }

        public IEnumerable<ClientDTO> GetEntities
        {
            get
            {
                var entities = new List<ClientDTO>();
                foreach (var client in _container.Clients.Select(x => x))
                {
                    entities.Add(ToObject(client));
                }

                return entities;
            }
        }

        public ClientDTO GetEntityById(int id)
        {
            return ToObject(_container.Clients.Find(id));
        }

        public void SaveChanges()
        {
            _container.SaveChanges();
        }

        public void Update(ClientDTO missingName)
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
