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
    public class BooksController : Controller
    {
        private libraryEntities db = new libraryEntities();


        public IActionResult AddToCart(int id)
        {
            // العثور على الكتاب من قاعدة البيانات
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // إعداد عنصر العربة
            var cartItem = new CartItem
            {
                BookID = book.BookID,
                Title = book.Title,
                Price = book.Price,
                Quantity = 1 // يمكنك تعيين الكمية حسب الحاجة
            };

            // إضافة العنصر إلى العربة
            Cart cart = GetCart();
            cart.AddItem(cartItem);

            // حفظ العربة في الجلسة
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index", "Books");
        }

        public IActionResult ViewCart()
        {
            var cart = GetCart();
            return View(cart);
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            return cart ?? new Cart();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,Author,Price,ISBN,PublishedYear,CategoryID,number_of_books,image,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,Title,Author,Price,ISBN,PublishedYear,CategoryID,number_of_books,image,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
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
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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
