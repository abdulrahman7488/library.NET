using Bookshop1.Models;
using Microsoft.AspNet.Identity;
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
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var product = db.Books.Find(id); // البحث عن المنتج بناءً على الـ id
            if (product == null)
            {
                return Json(new { success = false });
            }

            // استرجاع العربة من الجلسة أو إنشاء عربة جديدة
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();

            // تحقق من وجود المنتج بالفعل في العربة
            var cartItem = cart.FirstOrDefault(x => x.BookID == id);
            if (cartItem != null)
            {
                // زيادة الكمية إذا كان موجودًا
                cartItem.Quantity++;
            }
            else
            {
                // إضافة منتج جديد إلى العربة
                cartItem = new CartItem
                {
                    BookID = product.BookID,
                    Quantity = 1,
                    Title = product.Title,
                    Price = product.Price // تأكد من وجود هذا الحقل
                };
                cart.Add(cartItem);

                // حفظ المنتج في جدول CartItems في قاعدة البيانات
                var newCartItem = new CartItem
                {
                    BookID = product.BookID,
                    Title = product.Title,
                    Quantity = 1,
                    Price = product.Price // تأكد من وجود هذا الحقل
                };
                db.CartItems.Add(newCartItem);
                db.SaveChanges();
            }

            // حفظ العربة في الجلسة
            Session["Cart"] = cart;

            // حساب السعر الكلي
            decimal totalPrice = (decimal)cart.Sum(c => c.Quantity * c.Price);

            return Json(new { success = true, totalPrice = totalPrice });
        }
        // دالة لحساب السعر الكلي

    }
}