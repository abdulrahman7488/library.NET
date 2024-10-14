using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bookshop1.Models;

namespace Bookshop1.Controllers
{
    public class DashboardStatisticsController : Controller
    {
        private libraryEntities db = new libraryEntities();


        // GET: DashboardStatistics
        public ActionResult Index()
        {
            // إنشاء إحصائيات لوحة التحكم
            var dashboardStatistics = new DashboardStatistic
            {
                TotalBooks = db.Books.Count(),
                TotalCategories = db.Categories.Count(),
                TotalUsers = db.Users.Count(),
            };

            return View(dashboardStatistics);
        }

        // عرض الكتب في لوحة التحكم
        public ActionResult ManageBooks()
        {
            var books = db.Books.ToList();
            return View(books);
        }

        // إضافة كتاب جديد
        public ActionResult CreateBook()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("ManageBooks");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", book.CategoryID);
            return View(book);
        }

        // تعديل كتاب
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", book.CategoryID);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageBooks");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", book.CategoryID);
            return View(book);
        }

        // حذف كتاب
        public ActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBookConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("ManageBooks");
        }
    }
}
