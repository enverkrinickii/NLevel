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

        public HomeController()
        {
            _managerRepository = new ManagerRepository();
            _clientRepository = new ClientRepository();
            _productRepository = new ProductRepository();
            _saleInfoRepository = new PurchaseInfoRepository();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index(string managerSurname, string clientSurmane, string productName)
        {
            ViewBag.ManagerSurname = new SelectList(
                _managerRepository.GetAll(), new ManagerDTO().Surname);
            ViewBag.ClientSurname = new SelectList(
                _clientRepository.GetAll(), new ClientDTO().Surname);
            ViewBag.ProductName = new SelectList(
                _productRepository.GetAll(), new ProductDTO().ProductName);
            var purchasesInfos = _saleInfoRepository.GetAll()/*Pagination(1,3)*/.ToList();

            var allInfo = from info in purchasesInfos
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
                };
            if (!string.IsNullOrEmpty(managerSurname) && !managerSurname.Equals("Choose Manager"))
            {
                allInfo = allInfo.Where(x => x.ManagerSurname == managerSurname);
            }
            if (!string.IsNullOrEmpty(clientSurmane) && !clientSurmane.Equals("Choose Client"))
            {
                allInfo = allInfo.Where(x => x.ClientSurname == clientSurmane);
            }
            if (!string.IsNullOrEmpty(productName) && !productName.Equals("Choose Product"))
            {
                allInfo = allInfo.Where(x => x.ProductName == productName);
            }
            return View(allInfo);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Id = id;
            int myId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                myId = (int) id;
            }
            var purchaseInfo = _saleInfoRepository.GetEntityById(myId);
            var client = _clientRepository.GetEntityById(purchaseInfo.ClientId);
            var manager = _managerRepository.GetEntityById(purchaseInfo.ManagerId);
            var product = _productRepository.GetEntityById(purchaseInfo.ProductId);
            ViewBag.ManagerSurname = new SelectList(_managerRepository.GetAll(), manager.Surname);
            ViewBag.ClientSurname = new SelectList(_clientRepository.GetAll(), client.Surname);
            ViewBag.ProductName = new SelectList(_productRepository.GetAll(), product.ProductName);

            var info = new PurchaseInfoViewModel
            {
                Id = purchaseInfo.Id,
                ClientSurname = client.Surname,
                ManagerSurname = manager.Surname,
                ProductCost = product.ProductCost,
                ProductName = product.ProductName,
                SaleDate = purchaseInfo.SaleDate
            };
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseInfoViewModel infoViewModel)
        {
            var client = _clientRepository.GetEntityByName(infoViewModel.ClientSurname);
            var manager = _managerRepository.GetEntityByName(infoViewModel.ManagerSurname);
            var product = _productRepository.GetEntityByName(infoViewModel.ProductName);
            if (ModelState.IsValid)
            {
                var info = new PurchaseInfoDTO
                {
                    ClientId = client.Id,
                    Id = infoViewModel.Id,
                    ManagerId = manager.Id,
                    ProductId = product.Id,
                    SaleDate = infoViewModel.SaleDate
                };

                _saleInfoRepository.Update(info);
                return RedirectToAction("Index");
            }
            ViewBag.ManagerSurname = new SelectList(_managerRepository.GetAll(), manager.Surname);
            ViewBag.ClientSurname = new SelectList(_clientRepository.GetAll(), client.Surname);
            ViewBag.ProductName = new SelectList(_productRepository.GetAll(), product.ProductName);
            return View(infoViewModel);
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

        public JsonResult DiagramDataJsonResult()
        {
            var managers = _managerRepository.GetAll();
            var data = managers.Select(manager => new ChatrViewModel
                {
                    ManagerSurname = manager.Surname,
                    ManagersPurchases = manager.PurchaseInfo.Count
                })
                .ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}