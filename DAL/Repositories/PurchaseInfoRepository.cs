using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using NLevel;
using PurchaseInfo = DAL.Models.PurchaseInfo;

namespace DAL.Repositories
{
    public class PurchaseInfoRepository : IRepository<PurchaseInfo, NLevel.PurchaseInfo>
    {
        private StoreContext _container;

        public PurchaseInfoRepository()
        {
            _container = new StoreContext();
        }

        private static PurchaseInfo ToObject(NLevel.PurchaseInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return new PurchaseInfo
            {
                PurchaseDate = info.SaleDate,
                ClientId = info.Client.Id,
                ManagerId = info.Manager.Id,
                ProductId = info.Product.Id
            };
        }

        private static NLevel.PurchaseInfo ToEntity(PurchaseInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("client cannot be null");
            }
            return new NLevel.PurchaseInfo
            {
                Id = info.Id,
                SaleDate = info.PurchaseDate,
                ClientId = info.ClientId,
                ManagerId = info.ManagerId,
                ProductId = info.ProductId
            };
        }
        public NLevel.PurchaseInfo GetEntity(PurchaseInfo purchaseInfo)
        {
            var entity = _container.PurchasesInfo.FirstOrDefault(x => x.Id == purchaseInfo.Id);
            return entity;
        }

        public void Add(PurchaseInfo dalEntity)
        {
            var info = ToEntity(dalEntity);
            _container.PurchasesInfo.Add(info);
            SaveChanges();
        }

        public void Remove(int id)
        {
            var info = _container.PurchasesInfo.Find(id);
            if (info != null) _container.PurchasesInfo.Remove(info);
            SaveChanges();
        }

        public NLevel.PurchaseInfo GetEntityById(int id)
        {
            var product = _container.PurchasesInfo.FirstOrDefault(info => info.Id == id);
            return product;
        }

        public IEnumerable<PurchaseInfo> GetEntities
        {
            get
            {
                var entities = new List<PurchaseInfo>();
                foreach (var item in _container.PurchasesInfo.Select(x => x))
                {
                    entities.Add(ToObject(item));
                }

                return entities;
            }
        }

        public void SaveChanges()
        {
            _container.SaveChanges();
        }

        public void Update(PurchaseInfo missingName)
        {
            _container.Entry(missingName).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }

        ~PurchaseInfoRepository()
        {
            Dispose();
        }
    }
}
