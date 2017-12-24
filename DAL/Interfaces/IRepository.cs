using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<TDalEntity> : IDisposable
        where TDalEntity : class
        
    {
        TDalEntity GetEntity(TDalEntity source);
        void Add(TDalEntity dalEntity);
        void Remove(int id);
        TDalEntity GetEntityById(int id);
        IEnumerable<TDalEntity> GetEntities { get; }
        void SaveChanges();
        void Update(TDalEntity entity);
        IEnumerable<TDalEntity> GetAll();
        IEnumerable<TDalEntity> Pagination(int begin, int amount);
        TDalEntity GetEntityByName(string name);
    }
}
