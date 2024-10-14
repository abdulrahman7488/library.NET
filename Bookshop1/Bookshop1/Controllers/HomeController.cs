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

        // تعديل الأكشن Contact ليشمل التعامل مع نموذج الاتصال
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                // هنا يمكنك إضافة الكود لحفظ الرسالة في قاعدة البيانات أو إرسالها للبريد الإلكتروني
                ViewBag.Message = "تم إرسال رسالتك بنجاح. شكرًا لتواصلك معنا!";
                ModelState.Clear(); // مسح النموذج بعد الإرسال
            }
            return View(model);
        }
    }
}