using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KudevolveWeb.Models;

namespace KudevolveWeb.Controllers
{
    public class CountiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Counties
        public ActionResult Index()
        {
            return View(db.Counties.ToList());
        }

        // GET: Counties/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County county = db.Counties.Find(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            return View(county);
        }

        // GET: Counties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Counties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Governor,Senator")] County county)
        {
            if (ModelState.IsValid)
            {
                db.Counties.Add(county);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(county);
        }

        // GET: Counties/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County county = db.Counties.Find(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            return View(county);
        }

        // POST: Counties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Governor,Senator")] County county)
        {
            if (ModelState.IsValid)
            {
                db.Entry(county).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(county);
        }

        // GET: Counties/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            County county = db.Counties.Find(id);
            if (county == null)
            {
                return HttpNotFound();
            }
            return View(county);
        }

        // POST: Counties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            County county = db.Counties.Find(id);
            db.Counties.Remove(county);
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
