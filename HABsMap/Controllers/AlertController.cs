using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HABsMap.Models;

namespace HABsMap.Controllers
{
    public class AlertController : Controller
    {
        private msdb2293Entities db = new msdb2293Entities();

        // GET: Alert
        public ActionResult Index()
        {
            var habs_alert = db.habs_alert.Include(h => h.habs_area);
            return View(habs_alert.ToList());
        }

        // GET: Alert/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            habs_alert habs_alert = db.habs_alert.Find(id);
            if (habs_alert == null)
            {
                return HttpNotFound();
            }
            return View(habs_alert);
        }

        // GET: Alert/Create
        public ActionResult Create()
        {
            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name");
            return View();
        }

        // POST: Alert/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,user_email,email_status,location_id")] habs_alert habs_alert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.habs_alert.Add(habs_alert);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name", habs_alert.location_id);
                return View(habs_alert);

            }
            catch(Exception err)
            {
                return View("Could not add email.");
            }

        }

        // GET: Alert/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            habs_alert habs_alert = db.habs_alert.Find(id);
            if (habs_alert == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name", habs_alert.location_id);
            return View(habs_alert);
        }

        // POST: Alert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,user_email,email_status,location_id")] habs_alert habs_alert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habs_alert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name", habs_alert.location_id);
            return View(habs_alert);
        }

        // GET: Alert/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            habs_alert habs_alert = db.habs_alert.Find(id);
            if (habs_alert == null)
            {
                return HttpNotFound();
            }
            return View(habs_alert);
        }

        // POST: Alert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            habs_alert habs_alert = db.habs_alert.Find(id);
            db.habs_alert.Remove(habs_alert);
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
