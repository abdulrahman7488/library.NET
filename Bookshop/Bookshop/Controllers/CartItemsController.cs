using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bookshop.Models;

namespace Bookshop.Controllers
{
    public class CartItemsController : Controller
    {
        private libraryEntities db = new libraryEntities();

        // GET: CartItems
        public ActionResult Index()
        {
            var cartItems = db.CartItems.Include(c => c.Book).Include(c => c.ShoppingCart).Include(c => c.User);
            return View(cartItems.ToList());
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            ViewBag.CartID = new SelectList(db.ShoppingCarts, "CartID", "CartID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartItemID,CartID,BookID,Quantity,Price,UserID")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", cartItem.BookID);
            ViewBag.CartID = new SelectList(db.ShoppingCarts, "CartID", "CartID", cartItem.CartID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", cartItem.UserID);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", cartItem.BookID);
            ViewBag.CartID = new SelectList(db.ShoppingCarts, "CartID", "CartID", cartItem.CartID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", cartItem.UserID);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CartItemID,CartID,BookID,Quantity,Price,UserID")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", cartItem.BookID);
            ViewBag.CartID = new SelectList(db.ShoppingCarts, "CartID", "CartID", cartItem.CartID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", cartItem.UserID);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
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
