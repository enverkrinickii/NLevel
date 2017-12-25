using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Nlevel.Web.Models;

namespace Nlevel.Web.Controllers
{
    public class AdminController : Controller
    {
        private IRepository<ManagerDTO> _managerRepository;
        private IRepository<ClientDTO> _clientRepository;
        private IRepository<ProductDTO> _productRepository;
        private IRepository<PurchaseInfoDTO> _saleInfoRepository;

        public AdminController()
        {
            _managerRepository = new ManagerRepository();
            _clientRepository = new ClientRepository();
            _productRepository = new ProductRepository();
            _saleInfoRepository = new PurchaseInfoRepository();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
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
            return View(allInfo.ToList());
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
                myId = (int)id;
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
    }
}