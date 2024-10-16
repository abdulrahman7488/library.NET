using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bookshop1.Models;
using System.IO;

namespace Bookshop1.Controllers
{
    public class BooksController : Controller
    {
        private libraryEntities db = new libraryEntities();

        public ActionResult BooksManage()
        {
            var books = db.Books.ToList();
            return View(books);
        }

        // GET: Books
        public ActionResult Index(string searchQuery)
        {
            var books = db.Books.Include(b => b.Category);

            // فحص إذا كان هناك بحث
            if (!string.IsNullOrEmpty(searchQuery))
            {
                books = books.Where(b => b.Title.Contains(searchQuery) || b.Author.Contains(searchQuery));
            }

            var booksList = books.ToList();

            // إذا كان هناك كتاب واحد فقط في نتائج البحث، الانتقال إلى صفحة التفاصيل
            if (booksList.Count == 1)
            {
                return RedirectToAction("Details", new { id = booksList[0].BookID });
            }

            return View(booksList);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = db.Books.Include(b => b.Category).FirstOrDefault(b => b.BookID == id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,Author,Price,ISBN,PublishedYear,CategoryID,number_of_books,Description,ImagePath")] Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // تحقق من أن المستخدم قام برفع صورة
                if (ImageFile != null)
                {
                    // مسار تخزين الصورة في المجلد "Images"
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; // اسم فريد للملف
                    book.ImagePath = "~/Images/" + fileName;

                    // حفظ الصورة الفعلية في المجلد "Images"
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,Title,Author,Price,ISBN,PublishedYear,CategoryID,number_of_books,Description,ImagePath")] Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // إذا قام المستخدم برفع صورة جديدة، يتم تحديث المسار
                if (ImageFile != null)
                {
                    // مسار تخزين الصورة في المجلد "Images"
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension; // اسم فريد للملف
                    book.ImagePath = "~/Images/" + fileName;

                    // حفظ الصورة الفعلية في المجلد "Images"
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    ImageFile.SaveAs(fileName);
                }

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // العثور على الكتاب
            Book book = db.Books.Find(id);

            // حذف العناصر المرتبطة بالكتاب من CartItems
            var cartItems = db.CartItems.Where(c => c.BookID == id).ToList();
            db.CartItems.RemoveRange(cartItems);

            // حذف الكتاب
            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("BooksManage");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
