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
            //Handle Exceptions
            try
            {
                //Query to get most recent sample from the samples table and the areas table
                var result = from a in db.habs_area
                             join c in db.habs_sample
                             on a.location_id equals c.location_id into samples
                             let sample = samples.OrderByDescending(c => c.date_sampled).FirstOrDefault()
                             //Create new Status Models which are returned to the View with the information
                             select new StatusModel
                             {
                                 Location = a.location_name,
                                 latitude = a.latitude,
                                 longitude = a.longitude,
                                 species = sample.species_id,
                                 sample_status = sample.sample_status,
                                 Date = sample.sample_date ?? DateTime.Now
                             };

                //Search By Name Only or by Name and Species
                if (!String.IsNullOrEmpty(areaname))
                {
                    //Filter the results if an areaname is searched for.
                    result = result.Where(r => r.Location.Contains(areaname));
                }

                //Return the result to the View
                return View(result);
            }

            //Exception Handling
            catch(Exception err)
            {
                //Return Error Message
                return View("Error, Could not get status update list.");
            }
        }

        //Returns areas as JSON Objects to be passed to the LeafletJS Map
        //Prevents JSON Caching
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult GetMapMarkers()
        {

            try
            {
                //Query to get most recent sample from the samples table and the areas table
                var result = from a in db.habs_area
                             join c in db.habs_sample
                             on a.location_id equals c.location_id into samples
                             let sample = samples.OrderByDescending(c => c.date_sampled).FirstOrDefault()
                             //Create new Status Models which are returned to the View as JSON
                             select new StatusModel
                             {
                                 Location = a.location_name,
                                 latitude = a.latitude,
                                 longitude = a.longitude,
                                 species = sample.species_id,
                                 sample_status = sample.sample_status,
                                 Date = sample.sample_date ?? DateTime.Now
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //Exception Handling
            catch (Exception err)
            {
                //Return of JSON is denied
                return Json(null, JsonRequestBehavior.DenyGet);
            }

        }


    }
}