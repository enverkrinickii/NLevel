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
    public class ProductController : Controller
    {
        private IRepository<ProductDTO> _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        // GET: Product
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var products = _productRepository.GetAll();
            var productsViewModel = products.Select(Mapper.Map<ProductViewModel>).ToList();
            return View(productsViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(Mapper.Map<ProductDTO>(product));
                return RedirectToAction("Index");
            }
            return View(product);
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

            var productDto = _productRepository.GetEntityById(myId);

            if (productDto == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<ProductViewModel>(productDto));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _productRepository.Remove(id);
            return RedirectToAction("Index");
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

            var manager = _productRepository.GetEntityById(myId);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ProductViewModel>(manager));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel manager)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(Mapper.Map<ProductDTO>(manager));
                _productRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manager);
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