using System;
using DAL.Repositories;
using NLevel;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository<DAL.Models.Manager, Manager> managerRepository = new ManagerRepository();
            IRepository<DAL.Models.Client, Client> clientRepository = new ClientRepository();
            IRepository<DAL.Models.Product, Product> productRepository = new ProductRepository();
            IRepository<DAL.Models.PurchaseInfo, PurchaseInfo> purchaseInfoRepository = new PurchaseInfoRepository();

            //var manager = new DAL.Models.Manager { Surname = "Ivan" };
            //managerRepository.Add(manager);
            //var manager1 = new DAL.Models.Manager { Surname = "Petr" };
            //managerRepository.Add(manager1);
            //var manager2 = new DAL.Models.Manager { Surname = "Semen" };
            //managerRepository.Add(manager2);
            //var manager3 = new DAL.Models.Manager { Surname = "Igor" };
            //managerRepository.Add(manager3);
            //var managerEntity = managerRepository.GetEntity(manager);
            //var managerEntity1 = managerRepository.GetEntity(manager1);
            //var managerEntity2 = managerRepository.GetEntity(manager2);
            //var managerEntity3 = managerRepository.GetEntity(manager3);

            //var client = new DAL.Models.Client { Surname = "Alex" };
            //clientRepository.Add(client);
            //var client1 = new DAL.Models.Client { Surname = "Nik" };
            //clientRepository.Add(client1);
            //var client2 = new DAL.Models.Client { Surname = "John" };
            //clientRepository.Add(client2);
            //var client3 = new DAL.Models.Client { Surname = "Ben" };
            //clientRepository.Add(client3);

            //var clientEntity = clientRepository.GetEntity(client);
            //var clientEntity1 = clientRepository.GetEntity(client1);
            //var clientEntity2 = clientRepository.GetEntity(client2);
            //var clientEntity3 = clientRepository.GetEntity(client3);

            //var product = new DAL.Models.Product { ProductName = "Laptop Samsung", ProductCost = 2000 };
            //productRepository.Add(product);
            //var product1 = new DAL.Models.Product { ProductName = "Laptop Asus", ProductCost = 1000 };
            //productRepository.Add(product1);
            //var product2 = new DAL.Models.Product { ProductName = "Laptop Acer", ProductCost = 1500 };
            //productRepository.Add(product2);
            //var product3 = new DAL.Models.Product { ProductName = "Laptop Apple", ProductCost = 5000 };
            //productRepository.Add(product3);

            //var productEntity = productRepository.GetEntity(product);
            //var productEntity1 = productRepository.GetEntity(product1);
            //var productEntity2 = productRepository.GetEntity(product2);
            //var productEntity3 = productRepository.GetEntity(product3);

            //var purchaseInfo = new DAL.Models.PurchaseInfo
            //{
            //    PurchaseDate = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            //    ManagerId = managerEntity.Id,
            //    ClientId = clientEntity.Id,
            //    ProductId = productEntity.Id
            //};
            //purchaseInfoRepository.Add(purchaseInfo);

            //var purchaseInfo1 = new DAL.Models.PurchaseInfo
            //{
            //    PurchaseDate = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            //    ManagerId = managerEntity3.Id,
            //    ClientId = clientEntity1.Id,
            //    ProductId = productEntity.Id
            //};
            //purchaseInfoRepository.Add(purchaseInfo1);

            //var purchaseInfo2 = new DAL.Models.PurchaseInfo
            //{
            //    PurchaseDate = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            //    ManagerId = managerEntity.Id,
            //    ClientId = clientEntity2.Id,
            //    ProductId = productEntity3.Id
            //};
            //purchaseInfoRepository.Add(purchaseInfo2);

            //var purchaseInfo3 = new DAL.Models.PurchaseInfo
            //{
            //    PurchaseDate = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            //    ManagerId = managerEntity1.Id,
            //    ClientId = clientEntity3.Id,
            //    ProductId = productEntity2.Id
            //};
            //purchaseInfoRepository.Add(purchaseInfo3);

            //var purchaseInfo4 = new DAL.Models.PurchaseInfo
            //{
            //    PurchaseDate = DateTime.Now.Date.ToString(CultureInfo.InvariantCulture),
            //    ManagerId = managerEntity2.Id,
            //    ClientId = clientEntity3.Id,
            //    ProductId = productEntity1.Id
            //};
            //purchaseInfoRepository.Add(purchaseInfo4);


            var list = purchaseInfoRepository.GetEntities;
            foreach (var info in list)
            {
                Console.WriteLine(info);
            }

            Console.ReadKey();
        }
    }
}
