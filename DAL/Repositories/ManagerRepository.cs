using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DAL.Interfaces;
using NLevel;
using ManagerDTO = DAL.Models.ManagerDTO;

namespace DAL.Repositories
{
    public class ManagerRepository : IRepository<ManagerDTO>
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
                throw new ArgumentNullException($"client cannot be null");
            }
            return Mapper.Map<ManagerDTO>(manager);
        }

        private static Manager ToEntity(ManagerDTO manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException($"client cannot be null");
            }
            return Mapper.Map<Manager>(manager);
        }

        public ManagerDTO GetEntity(ManagerDTO manager)
        {
            var entity = ToObject(_container.Managers.FirstOrDefault(x => x.Surname == manager.Surname));
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

        public ManagerDTO GetEntityById(int id)
        {
            return ToObject(_container.Managers.Find(id));
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

        public IEnumerable<ManagerDTO> GetAll()
        {
            foreach (var manager in _container.Managers)
            {
                yield return ToObject(manager);
            }
        }

        public IEnumerable<ManagerDTO> Pagination(int begin, int amount)
        {
            var managers = _container.Managers.OrderBy(x => x.Surname).Skip((begin - 1) * amount).Take(amount);
            foreach (var manager in managers)
            {
                yield return ToObject(manager);
            }
            //var managers =
            //    GetAll().OrderBy(x => x.Surname).Skip((begin - 1) * amount).Take(amount);
            //return managers;
        }

        public ManagerDTO GetEntityByName(string name)
        {
            return ToObject(_container.Managers.FirstOrDefault(x => x.Surname == name));
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
