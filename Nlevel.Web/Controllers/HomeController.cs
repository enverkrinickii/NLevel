using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Models;
using Nlevel.Web.Models;
using NLevel;

namespace Nlevel.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<ManagerDTO, Manager> _managerRepository;
        private IRepository<ClientDTO, Client> _clientRepository;
        private IRepository<ProductDTO, Product> _productRepository;
        private IRepository<PurchaseInfoDTO, PurchaseInfo> _saleInfoRepository;

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            _managerRepository = new ManagerRepository();
            _clientRepository = new ClientRepository();
            _productRepository = new ProductRepository();
            _saleInfoRepository = new PurchaseInfoRepository();

            var purchasesInfos = _saleInfoRepository.GetEntities;

            var allInfo = new List<PurchaseInfoViewModel>();

            foreach (var info in purchasesInfos)
            {
                
                var client = _clientRepository.GetEntityById(info.ClientId);
                var manager = _managerRepository.GetEntityById(info.ManagerId);
                var product = _productRepository.GetEntityById(info.ProductId);
                allInfo.Add(new PurchaseInfoViewModel
                {
                    Id = info.Id,
                    ClientSurname = client.Surname,
                    ManagerSurname = manager.Surname,
                    ProductCost = product.ProductCost,
                    ProductName = product.ProductName,
                    SaleDate = info.SaleDate
                });
            }
            
            return View(allInfo);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Id = id;
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var purchaseInfo = _saleInfoRepository;
            //ViewBag.Managers = _managerRepository.GetEntities;
            //ViewBag.Clients = _clientRepository.GetEntities;
            //ViewBag.Products = _productRepository.GetEntities;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}