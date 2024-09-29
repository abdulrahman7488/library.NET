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

        // GET: DashboardStatistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardStatistic dashboardStatistic = db.DashboardStatistics.Find(id);
            if (dashboardStatistic == null)
            {
                return HttpNotFound();
            }
            return View(dashboardStatistic);
        }

        // GET: DashboardStatistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashboardStatistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TotalBooks,TotalCategories,TotalUsers,ActiveBooks,InactiveBooks")] DashboardStatistic dashboardStatistic)
        {
            if (ModelState.IsValid)
            {
                db.DashboardStatistics.Add(dashboardStatistic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dashboardStatistic);
        }

        // GET: DashboardStatistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardStatistic dashboardStatistic = db.DashboardStatistics.Find(id);
            if (dashboardStatistic == null)
            {
                return HttpNotFound();
            }
            return View(dashboardStatistic);
        }

        // POST: DashboardStatistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TotalBooks,TotalCategories,TotalUsers,ActiveBooks,InactiveBooks")] DashboardStatistic dashboardStatistic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dashboardStatistic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dashboardStatistic);
        }

        // GET: DashboardStatistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DashboardStatistic dashboardStatistic = db.DashboardStatistics.Find(id);
            if (dashboardStatistic == null)
            {
                return HttpNotFound();
            }
            return View(dashboardStatistic);
        }

        // POST: DashboardStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DashboardStatistic dashboardStatistic = db.DashboardStatistics.Find(id);
            db.DashboardStatistics.Remove(dashboardStatistic);
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
