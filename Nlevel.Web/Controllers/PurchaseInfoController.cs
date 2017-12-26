using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Nlevel.Web.Models;

namespace Nlevel.Web.Controllers
{
    public class PurchaseInfoController : Controller
    {
        private IRepository<ManagerDTO> _managerRepository;
        private IRepository<ClientDTO> _clientRepository;
        private IRepository<ProductDTO> _productRepository;
        private IRepository<PurchaseInfoDTO> _saleInfoRepository;

        public PurchaseInfoController()
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

            var allInfo = purchasesInfos.Select(ToViewModelObject);
            return View(allInfo.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
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
            ViewBag.ManagerSurname = new SelectList(_managerRepository.GetAll(), new ManagerDTO().Surname);
            ViewBag.ClientSurname = new SelectList(_clientRepository.GetAll(), new ClientDTO().Surname);
            ViewBag.ProductName = new SelectList(_productRepository.GetAll(), new ProductDTO().ProductName);

            return View(ToViewModelObject(purchaseInfo));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseInfoViewModel infoViewModel)
        {
            if (ModelState.IsValid)
            {
                _saleInfoRepository.Update(ToDtoObject(infoViewModel));
                _saleInfoRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerSurname = new SelectList(_managerRepository.GetAll(), new ManagerDTO().Surname);
            ViewBag.ClientSurname = new SelectList(_clientRepository.GetAll(), new ClientDTO().Surname);
            ViewBag.ProductName = new SelectList(_productRepository.GetAll(), new ProductDTO().ProductName);

            return View(infoViewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
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

            if (purchaseInfo == null)
            {
                return HttpNotFound();
            }
            return View(ToViewModelObject(purchaseInfo));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _saleInfoRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ManagerSurname = new SelectList(_managerRepository.GetAll(), new ManagerDTO().Surname);
            ViewBag.ClientSurname = new SelectList(_clientRepository.GetAll(), new ClientDTO().Surname);
            ViewBag.ProductName = new SelectList(_productRepository.GetAll(), new ProductDTO().ProductName);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseInfoViewModel purchaseInfo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ManagerSurname = new SelectList(_managerRepository.GetAll(), new ManagerDTO().Surname);
                ViewBag.ClientSurname = new SelectList(_clientRepository.GetAll(), new ClientDTO().Surname);
                ViewBag.ProductName = new SelectList(_productRepository.GetAll(), new ProductDTO().ProductName);
                return View(purchaseInfo);
            }
            _saleInfoRepository.Add(ToDtoObject(purchaseInfo));
            return RedirectToAction("Index");
        }

        private PurchaseInfoViewModel ToViewModelObject(PurchaseInfoDTO purchaseInfo)
        {
            var client = _clientRepository.GetEntityById(purchaseInfo.ClientId);
            var manager = _managerRepository.GetEntityById(purchaseInfo.ManagerId);
            var product = _productRepository.GetEntityById(purchaseInfo.ProductId);
            var info = new PurchaseInfoViewModel
            {
                Id = purchaseInfo.Id,
                ClientSurname = client.Surname,
                ManagerSurname = manager.Surname,
                ProductCost = product.ProductCost,
                ProductName = product.ProductName,
                SaleDate = purchaseInfo.SaleDate
            };

            return info;
        }

        private PurchaseInfoDTO ToDtoObject(PurchaseInfoViewModel infoViewModel)
        {
            var client = _clientRepository.GetEntityByName(infoViewModel.ClientSurname);
            var manager = _managerRepository.GetEntityByName(infoViewModel.ManagerSurname);
            var product = _productRepository.GetEntityByName(infoViewModel.ProductName);
            var info = new PurchaseInfoDTO
            {
                ClientId = client.Id,
                Id = infoViewModel.Id,
                ManagerId = manager.Id,
                ProductId = product.Id,
                SaleDate = infoViewModel.SaleDate
            };
            return info;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _saleInfoRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}