using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using NLevel;
using ManagerDTO = DAL.Models.ManagerDTO;

namespace DAL.Repositories
{
    public class ManagerRepository : IRepository<ManagerDTO, Manager>
    {
        private StoreContext _container;
        public ManagerRepository()
        {
            _container = new StoreContext();
        }

        private static ManagerDTO ToObject(Manager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return new ManagerDTO
            {
                Surname = manager.Surname
            };
        }

        private static Manager ToEntity(ManagerDTO manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return new Manager
            {
                Surname = manager.Surname
            };
        }

        public Manager GetEntity(ManagerDTO manager)
        {
            var entity = _container.Managers.FirstOrDefault(x => x.Surname == manager.Surname);
            return entity;
        }

        public void Add(ManagerDTO dalEntity)
        {
            var manager = ToEntity(dalEntity);
            _container.Managers.Add(manager);
            SaveChanges();
        }

        public void Remove(int id)
        {
            var manager = _container.Managers.Find(id);
            _container.Managers.Remove(manager ?? throw new InvalidOperationException());
            SaveChanges();
        }

        public Manager GetEntityById(int id)
        {
            var manager = _container.Managers.FirstOrDefault(mngr => mngr.Id == id);
            return manager;
        }

        public IEnumerable<ManagerDTO> GetEntities
        {
            get
            {
                var entities = new List<ManagerDTO>();
                foreach (var manager in _container.Managers.Select(x => x))
                {
                    entities.Add(ToObject(manager));
                }

                return entities;
            }
        }

        public void SaveChanges()
        {
            _container.SaveChanges();
        }

        public void Update(ManagerDTO missingName)
        {
            _container.Entry(missingName).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }

        ~ManagerRepository()
        {
            Dispose();
        }
    }
}
