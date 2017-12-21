using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using NLevel;
using ProductDTO = DAL.Models.ProductDTO;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<ProductDTO, Product>
    {
        private StoreContext _container;
        public ProductRepository()
        {
            _container = new StoreContext();
        }

        private static ProductDTO ToObject(Product product)
        {
            return new ProductDTO
            {
                ProductName = product.ProductName,
                ProductCost = product.ProductCost
            };
        }

        private static Product ToEntity(ProductDTO product)
        {
            return new Product
            {
                ProductName = product.ProductName,
                ProductCost = product.ProductCost
            };
        }

        public Product GetEntity(ProductDTO product)
        {
            var entity = _container.Products.FirstOrDefault(x => x.ProductName == product.ProductName);
            return entity;
        }

        public void Add(ProductDTO dalEntity)
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

        public Product GetEntityById(int id)
        {
            var product = _container.Products.FirstOrDefault(pr => pr.Id == id);
            return product;
        }

        public IEnumerable<ProductDTO> GetEntities
        {
            get
            {
                var entities = new List<ProductDTO>();
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

        public void Update(ProductDTO missingName)
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
