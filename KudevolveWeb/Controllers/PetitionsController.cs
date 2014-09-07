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
    public class PetitionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Petitions
        public ActionResult Index()
        {
            return View(db.Petitions.ToList());
        }

        // GET: Petitions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Petition petition = db.Petitions.Find(id);
            if (petition == null)
            {
                return HttpNotFound();
            }
            return View(petition);
        }

        // GET: Petitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Petitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PetitionId,Name,DateCreated,Content,Category,Votes,Status,Hashtag,Likes,URL")] Petition petition)
        {
            if (ModelState.IsValid)
            {
                db.Petitions.Add(petition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(petition);
        }

        // GET: Petitions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Petition petition = db.Petitions.Find(id);
            if (petition == null)
            {
                return HttpNotFound();
            }
            return View(petition);
        }

        // POST: Petitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PetitionId,Name,DateCreated,Content,Category,Votes,Status,Hashtag,Likes,URL")] Petition petition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(petition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(petition);
        }

        // GET: Petitions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Petition petition = db.Petitions.Find(id);
            if (petition == null)
            {
                return HttpNotFound();
            }
            return View(petition);
        }

        // POST: Petitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Petition petition = db.Petitions.Find(id);
            db.Petitions.Remove(petition);
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
