using Bookshop1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshop1.Controllers
{
    public class HomeController : Controller
    {
        private libraryEntities db = new libraryEntities();

        public ActionResult Index()
        {
            var model = new CategoryBookViewModel
            {

                Categories = db.Categories.ToList(),
                Books = db.Books.ToList()

            };
            return View(model);
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
    }
}