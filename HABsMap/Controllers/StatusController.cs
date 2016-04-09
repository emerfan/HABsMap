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
        public ActionResult Index(string areaname)
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
                              species = sample.species_id,
                              status = sample.sample_status,
                              sampDate = sample.sample_date ?? DateTime.Now
                          };
            //Search By Name Only or by Name and Species
            if (!String.IsNullOrEmpty(areaname))
            {
                result = result.Where(r => r.location_name.Contains(areaname));
            }

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
                             species = sample.species_id,
                             status = sample.sample_status,
                             sampDate = sample.sample_date ?? DateTime.Now
                         };
                

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}