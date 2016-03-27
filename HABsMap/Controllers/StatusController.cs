using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HABsMap.Models;
using System.Web.Script.Serialization;

namespace HABsMap.Controllers
{
    public class StatusController : Controller
    {
        //Declare an instance of the database model
        msdb2293Entities db = new msdb2293Entities();

        // GET: Status
        //Returns a List View of the Areas
        public ActionResult Index()
        {
            var result = from a in db.habs_area
                         join c in db.habs_sample
                         on a.location_id equals c.location_id into samples
                         let sample = samples.OrderByDescending(c => c.date_sampled).FirstOrDefault()
                         select new StatusModel
                         {
                             location_name = a.location_name,
                             latitude = a.latitude,
                             longitude = a.longitude,
                             status = sample.sample_status,
                             sampDate = sample.sample_date ?? DateTime.Now
                         };


            return View(result);
        }

        //Returns areas as JSON Objects to be passed to the LeafletJS Map
        //Prevents JSON Caching
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult GetMapMarkers()
        {
            var result = from a in db.habs_area
                         join c in db.habs_sample
                         on a.location_id equals c.location_id into samples
                         let sample = samples.OrderByDescending(c => c.date_sampled).FirstOrDefault()
                         select new StatusModel
                         {
                             location_name = a.location_name,
                             latitude = a.latitude,
                             longitude = a.longitude,
                             status = sample.sample_status,
                             sampDate = sample.sample_date ?? DateTime.Now
                         };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Returns areas as JSON Objects to be passed to the LeafletJS Map
        //Prevents JSON Caching
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult GetMapMarkersJS()
        {
            List<StatusModel> SM = new List<StatusModel>();
            List<String> Stats = new List<String>();

            var stati = from a in db.habs_area
                         join c in db.habs_sample
                         on a.location_id equals c.location_id into samples
                         let sample = samples.OrderByDescending(c => c.date_sampled).FirstOrDefault()
                         select new StatusModel
                         {
                             location_name = a.location_name,
                             latitude = a.latitude,
                             longitude = a.longitude,
                             status = sample.sample_status,
                             sampDate = sample.sample_date ?? DateTime.Now
                         };

            foreach (var status in stati)
            {
                SM.Add(status);
            }

            var json = new JavaScriptSerializer().Serialize(SM);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}