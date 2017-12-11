using System;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IRepository<TDalEntity, TEntity> : IDisposable
        where TEntity : class 
        where TDalEntity : class
        
    {
        TEntity GetEntity(TDalEntity source);
        void Add(TDalEntity dalEntity);
        void Remove(TDalEntity dalEntity);
        TEntity GetEntityById(int id);
        IEnumerable<TDalEntity> GetEntities { get; }
        void SaveChanges();
        void Update(TDalEntity entity);
    }
}
