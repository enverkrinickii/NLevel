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
    public class ClientController : Controller
    {
        private IRepository<ClientDTO> _clientRepository;

        public ClientController()
        {
            _clientRepository = new ClientRepository();
        }
        // GET: Client
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            //bad practice to call repos from controller directly
            var clients = _clientRepository.GetAll();
            var clientsViewModels = clients.Select(Mapper.Map<ClientViewModel>).ToList();
            return View(clientsViewModels);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel client)
        {
            if (!ModelState.IsValid) return View(client);
            _clientRepository.Add(Mapper.Map<ClientDTO>(client));
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            int myId;
            //you have to use HasValue
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                myId = (int) id;
            }

            var client = _clientRepository.GetEntityById(myId);

            if (client == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<ClientViewModel>(client));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _clientRepository.Remove(id);
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

            var client = _clientRepository.GetEntityById(myId);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ClientViewModel>(client));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Update(Mapper.Map<ClientDTO>(client));
                //in some cases you call SaveChanges in repo, sometimes in controller - it's a mess.
                _clientRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {            
                _clientRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
