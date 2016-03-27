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
    public class SampleController : Controller
    {
        private msdb2293Entities db = new msdb2293Entities();


        //GET: Specific Area Samples
        public ActionResult Index(string areaname)
        {
            //Viewbag variable for the page title
            ViewBag.AreaName = areaname;

            //Get the Shellfish Samples
            var habs_shellfish_sample = db.habs_sample.Include(h => h.habs_area).Include(h => h.habs_species);


            if (!String.IsNullOrEmpty(areaname))
            {
                //Search for the correct shellfish samples matching the query string passed
                habs_shellfish_sample = habs_shellfish_sample.Where(h => h.habs_area.location_name.Contains(areaname));
            }
            //Return them to the view
            return View(habs_shellfish_sample.ToList());
        }



        public ActionResult SearchSamples()
        {
            return View();
        }

        //Prevent the program from caching the JSON result
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult GetSamples(string areaname)
        {
            //Return all the samples and the area name.
            var result = (
                            from a in db.habs_sample
                            join c in db.habs_area
                            on a.location_id equals c.location_id


                            join d in db.habs_species
                            on a.species_id equals d.species_id
                            select new
                            {
                                Location = c.location_name,
                                Status = a.sample_status,
                                Date = a.sample_date,
                                Species = d.species_name,
                                ASP = a.asp,
                                PSP = a.psp,
                                DSP = a.dsp,
                                AZP = a.azp,
                            });

            if (!String.IsNullOrEmpty(areaname))
            {
                result = result.Where(r => r.Location.Contains(areaname));
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Sample/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            habs_sample habs_sample = db.habs_sample.Find(id);
            if (habs_sample == null)
            {
                return HttpNotFound();
            }
            return View(habs_sample);
        }

        // GET: Sample/Create
        public ActionResult Create()
        {
            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name");
            ViewBag.species_id = new SelectList(db.habs_species, "species_id", "species_name");
            return View();
        }

        // POST: Sample/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sample_id,location_id,species_id,report_id,tissue,asp,azp,dsp,ptx,ytx,psp,sample_status,date_sampled,sample_date")] habs_sample habs_sample)
        {
            if (ModelState.IsValid)
            {
                db.habs_sample.Add(habs_sample);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name", habs_sample.location_id);
            ViewBag.species_id = new SelectList(db.habs_species, "species_id", "species_name", habs_sample.species_id);
            return View(habs_sample);
        }

        // GET: Sample/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            habs_sample habs_sample = db.habs_sample.Find(id);
            if (habs_sample == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name", habs_sample.location_id);
            ViewBag.species_id = new SelectList(db.habs_species, "species_id", "species_name", habs_sample.species_id);
            return View(habs_sample);
        }

        // POST: Sample/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sample_id,location_id,species_id,report_id,tissue,asp,azp,dsp,ptx,ytx,psp,sample_status,date_sampled,sample_date")] habs_sample habs_sample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habs_sample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_id = new SelectList(db.habs_area, "location_id", "location_name", habs_sample.location_id);
            ViewBag.species_id = new SelectList(db.habs_species, "species_id", "species_name", habs_sample.species_id);
            return View(habs_sample);
        }

        // GET: Sample/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            habs_sample habs_sample = db.habs_sample.Find(id);
            if (habs_sample == null)
            {
                return HttpNotFound();
            }
            return View(habs_sample);
        }

        // POST: Sample/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            habs_sample habs_sample = db.habs_sample.Find(id);
            db.habs_sample.Remove(habs_sample);
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
