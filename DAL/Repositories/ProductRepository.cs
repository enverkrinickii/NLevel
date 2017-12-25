using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DAL.Interfaces;
using NLevel;
using ProductDTO = DAL.Models.ProductDTO;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<ProductDTO>
    {
        private StoreContext _container;
        public ProductRepository()
        {
            _container = new StoreContext();
        }

        private static ProductDTO ToObject(Product product)
        {

            return Mapper.Map<ProductDTO>(product);
        }

        private static Product ToEntity(ProductDTO product)
        {
            return Mapper.Map<Product>(product);
        }

        public ProductDTO GetEntity(ProductDTO product)
        {
            var entity = ToObject(_container.Products.FirstOrDefault(x => x.ProductName == product.ProductName));
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

        public ProductDTO GetEntityById(int id)
        {
            return ToObject(_container.Products.Find(id));
        }

        public void SaveChanges()
        {
            _container.SaveChanges();
        }

        public void Update(ProductDTO missingName)
        {
            _container.Entry(ToEntity(missingName)).State = EntityState.Modified;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            foreach (var product in _container.Products)
            {
                yield return ToObject(product);
            }
        }

        public IEnumerable<ProductDTO> Pagination(int begin, int amount)
        {
            var products = _container.Products.OrderBy(x => x.ProductName).Skip((begin - 1) * amount).Take(amount);
            foreach (var product in products)
            {
                yield return ToObject(product);
            }
        }

        public ProductDTO GetEntityByName(string name)
        {
            return ToObject(_container.Products.FirstOrDefault(x => x.ProductName == name));
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
