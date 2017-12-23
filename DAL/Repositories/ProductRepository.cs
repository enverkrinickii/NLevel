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
            var product = ToObject(_container.Products.FirstOrDefault(pr => pr.Id == id));
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

        public IEnumerable<ProductDTO> GetAll()
        {
            foreach (var product in _container.Products)
            {
                yield return ToObject(product);
            }
        }

        public IEnumerable<ProductDTO> Pagination(int begin, int amount)
        {
            var products = GetAll().ToList();
            if (begin >= products.Count || begin + amount > products.Count) yield break;
            for (var i = begin; i < begin + amount; i++)
            {
                yield return products[i];
            }
        }

        public void Dispose()
        {
            _container?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
