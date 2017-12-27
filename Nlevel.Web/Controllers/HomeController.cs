﻿using System;
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

//do not use repositories in controller
//do not use ViewBag

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

        [Authorize(Roles = "user")]
        public ActionResult Index(string managerSurname, string clientSurmane, string saleDate)
        {
            ViewBag.ManagerSurname = new SelectList(
                _managerRepository.GetAll(), new ManagerDTO().Surname);
            ViewBag.ClientSurname = new SelectList(
                _clientRepository.GetAll(), new ClientDTO().Surname);
            ViewBag.ProductName = new SelectList(
                _productRepository.GetAll(), new ProductDTO().ProductName);
                
            //not optimal code   
            var purchasesInfos = _saleInfoRepository.GetAll()/*Pagination(1,3)*/.ToList();
            var dates = _saleInfoRepository.GetAll().Select(x => x.SaleDate).Distinct();
            ViewBag.Dates = new SelectList(dates);

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
            //"Choose Manager" - WTF? should be constant
            if (!string.IsNullOrEmpty(managerSurname) && !managerSurname.Equals("Choose Manager"))
            {
                allInfo = allInfo.Where(x => x.ManagerSurname == managerSurname);
            }
            if (!string.IsNullOrEmpty(clientSurmane) && !clientSurmane.Equals("Choose Client"))
            {
                allInfo = allInfo.Where(x => x.ClientSurname == clientSurmane);
            }
            if (!string.IsNullOrEmpty(saleDate) && !saleDate.Equals("Choose Date"))
            {
                allInfo = allInfo.Where(x => x.SaleDate == saleDate);
            }
            return View(allInfo);
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
            var products = _productRepository.GetAll();
            var data = products.Select(product => new ChatrViewModel
                {
                    ProductName = product.ProductName,
                    Amount = product.PurchaseInfo.Count
                })
                .ToList();
                
            //do you need JsonRequestBehavior.AllowGet ?
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
