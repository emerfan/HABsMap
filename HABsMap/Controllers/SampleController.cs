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


        //GET: Specific Area Samples, Take parameters for the search function
        public ActionResult Index(string areaname, string species, string date)
        {
            //Viewbag variable for the page title
            ViewBag.AreaName = areaname;

            //Return all the samples and the area name.
            var result = (
                            from a in db.habs_sample
                            join c in db.habs_area
                            on a.location_id equals c.location_id


                            join d in db.habs_species
                            on a.species_id equals d.species_id
                            select new SampleModel()
                            {
                                Location = c.location_name,
                                Status = a.sample_status,
                                Date = a.sample_date??DateTime.Now,
                                Species = d.species_name,
                                ASP = a.asp,
                                PSP = a.psp,
                                DSP = a.dsp,
                                AZP = a.azp,
                                PTX = a.ptx,
                                YTX = a.ytx,
                                Tissue = a.tissue
                            });

            //Search By Name Only or by Name and Species
            if (!String.IsNullOrEmpty(areaname))
            {
                result = result.Where(r => r.Location.Contains(areaname));
                //Name and Species
                if (!String.IsNullOrEmpty(species))
                {
                    result = result.Where(r => r.Species.Contains(species));

                 //Search By Name and Species and Date
                       if (!String.IsNullOrEmpty(date))
                        {
                            DateTime sampleDate = Convert.ToDateTime(date);
                            result = result.Where(r => r.Date.Equals(sampleDate));
                        }
                }

                //Search By Name and Date
                else if (!String.IsNullOrEmpty(date))
                {
                    DateTime sampleDate = Convert.ToDateTime(date);
                    result = result.Where(r => r.Date.Equals(sampleDate));
                }
            }

            //Search By Species Only
            else if (!String.IsNullOrEmpty(species))
            {
                result = result.Where(r => r.Species.Contains(species));   
                           
                //Search By Species and Date
                if (!String.IsNullOrEmpty(date))
                    {
                        DateTime sampleDate = Convert.ToDateTime(date);
                        result = result.Where(r => r.Date.Equals(sampleDate));
                    }
            }

            //Search By Date Only
            else if (!String.IsNullOrEmpty(date))
            {
                DateTime sampleDate = Convert.ToDateTime(date);
                result = result.Where(r => r.Date.Equals(sampleDate));
            }



            return View(result);
        }




        //This is for the Angular Search View, Might Get Rid of it
        public ActionResult SearchSamples()
        {
            return View();
        }



        //Prevent the program from caching the JSON result - Angular Search Page
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

    }
}
