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
    public class ManagerController : Controller
    {
        private IRepository<ManagerDTO> _managerRepository;

        public ManagerController()
        {
            _managerRepository = new ManagerRepository();
        }
        // GET: Manager
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var managers = _managerRepository.GetAll();
            var managersViewModels = managers.Select(Mapper.Map<ManagerViewModel>).ToList();
            return View(managersViewModels);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManagerViewModel manager)
        {
            if (ModelState.IsValid)
            {
                _managerRepository.Add(Mapper.Map<ManagerDTO>(manager));
                return RedirectToAction("Index");
            }
            return View(manager);
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

            var manager = _managerRepository.GetEntityById(myId);

            if (manager == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<ManagerViewModel>(manager));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _managerRepository.Remove(id);
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

            var manager = _managerRepository.GetEntityById(myId);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ManagerViewModel>(manager));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManagerViewModel manager)
        {
            if (ModelState.IsValid)
            {
                _managerRepository.Update(Mapper.Map<ManagerDTO>(manager));
                _managerRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _managerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}