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
        private IRepository<ManagerDTO> _managerRepository;
        private IRepository<ClientDTO> _clientRepository;
        private IRepository<ProductDTO> _productRepository;
        private IRepository<PurchaseInfoDTO> _saleInfoRepository;

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            _managerRepository = new ManagerRepository();
            _clientRepository = new ClientRepository();
            _productRepository = new ProductRepository();
            _saleInfoRepository = new PurchaseInfoRepository();

            var purchasesInfos = _saleInfoRepository.GetAll().ToList();

            var allInfo = (from info in purchasesInfos
                let client = _clientRepository.GetEntityById(info.ClientId)
                let manager = _managerRepository.GetEntityById(info.ManagerId)
                let product = _productRepository.GetEntityById(info.ProductId)
                select new PurchaseInfoViewModel
                {
                    Id = info.Id,
                    ClientSurname = client.Surname,
                    ManagerSurname = manager.Surname,
                    ProductCost = product.ProductCost,
                    ProductName = product.ProductName,
                    SaleDate = info.SaleDate
                }).ToList();

            return View(allInfo);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var purchaseInfo = _saleInfoRepository.GetEntityById(id);
            ViewBag.Managers = new SelectList(_managerRepository.GetEntities, "Id", "Surname", purchaseInfo.ManagerId);
            ViewBag.Clients = new SelectList(_clientRepository.GetEntities, "Id", "Surname", purchaseInfo.ClientId);

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