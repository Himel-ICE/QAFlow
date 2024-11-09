using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFlow.Models;

namespace CFlow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Profile()
        {
            return View();  
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Question()
        {
            return View();
        }
    }
}