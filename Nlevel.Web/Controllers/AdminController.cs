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
        public ActionResult Index()
        {
            return View();
        }
    }
}