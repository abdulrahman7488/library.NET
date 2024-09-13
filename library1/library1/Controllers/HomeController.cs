using library1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace library1.Controllers
{
    public class HomeController : Controller
    {
        private libraryEntities3 db = new libraryEntities3();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

    }
}