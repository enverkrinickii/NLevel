using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<TDalEntity, TEntity> : IDisposable
        where TEntity : class 
        where TDalEntity : class
        
    {
        TDalEntity GetEntity(TDalEntity source);
        void Add(TDalEntity dalEntity);
        void Remove(int id);
        TDalEntity GetEntityById(int id);
        IEnumerable<TDalEntity> GetEntities { get; }
        void SaveChanges();
        void Update(TDalEntity entity);
    }
}
