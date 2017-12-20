using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using NLevel;
using Product = DAL.Models.Product;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<Product, NLevel.Product>
    {
        private StoreContext _container;
        public ProductRepository()
        {
            _container = new StoreContext();
        }

        private static Product ToObject(NLevel.Product product)
        {
            return new Product
            {
                ProductName = product.ProductName,
                ProductCost = product.ProductCost
            };
        }

        private static NLevel.Product ToEntity(Product product)
        {
            return new NLevel.Product
            {
                ProductName = product.ProductName,
                ProductCost = product.ProductCost
            };
        }

        public NLevel.Product GetEntity(Product product)
        {
            var entity = _container.Products.FirstOrDefault(x => x.ProductName == product.ProductName);
            return entity;
        }

        public void Add(Product dalEntity)
        {
            var product = ToEntity(dalEntity);
            _container.Products.Add(product);
            SaveChanges();
        }

        public void Remove(int id)
        {
            var product = _container.Products.Find(id);
            if (product != null) _container.Products.Remove(product);
            SaveChanges();
        }

        public NLevel.Product GetEntityById(int id)
        {
            var product = _container.Products.FirstOrDefault(pr => pr.Id == id);
            return product;
        }

        public IEnumerable<Product> GetEntities
        {
            get
            {
                var entities = new List<Product>();
                foreach (var product in _container.Products.Select(x => x))
                {
                    entities.Add(ToObject(product));
                }

                return entities;
            }
        }
        public void SaveChanges()
        {
            _container.SaveChanges();
        }

        public void Update(Product missingName)
        {
            _container.Entry(missingName).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }

        ~ProductRepository()
        {
            Dispose();
        }
    }
}
