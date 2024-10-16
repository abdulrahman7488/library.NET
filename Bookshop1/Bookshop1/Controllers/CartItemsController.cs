using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bookshop1.Models;
using Microsoft.AspNet.Identity;

namespace Bookshop1.Controllers
{
    public class CartItemsController : Controller
    {
        private libraryEntities db = new libraryEntities();

        // GET: CartItems
        public ActionResult Index()
        {
            // استرجاع العربة من الجلسة
            List<CartItem> cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartItemID == id);
            if (cartItem == null)
            {
                TempData["Message"] = "Item not found.";
                return RedirectToAction("Index");
            }

            return View(cartItem);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cartItem = db.CartItems.SingleOrDefault(c => c.CartItemID == id);
            if (cartItem != null)
            {
                cartItem.IsDeleted = true; // تغيير القيمة بدلاً من الحذف
                db.SaveChanges();

                // تحديث العربة في الجلسة إذا كانت موجودة
                List<CartItem> cart = Session["Cart"] as List<CartItem>;
                if (cart != null)
                {
                    var sessionCartItem = cart.SingleOrDefault(c => c.CartItemID == id);
                    if (sessionCartItem != null)
                    {
                        sessionCartItem.IsDeleted = true; // تغيير قيمة العنصر في الجلسة
                        Session["Cart"] = cart; // تحديث العربة في الجلسة
                    }
                }

                TempData["Message"] = "Item marked as deleted successfully.";
            }
            else
            {
                TempData["Message"] = "Item not found.";
            }

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
