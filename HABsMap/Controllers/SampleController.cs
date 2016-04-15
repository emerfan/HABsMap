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
        public ActionResult Index(string areaname, string species, string date, string dateto)
        {
            //Viewbag variable for the page title and for the search breadcrumbs

            ViewBag.AreaName = areaname;
            ViewBag.Species = species;
            ViewBag.Date = date;
            ViewBag.DateTo = dateto;

            try
            {

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
                                Date = a.date_sampled??DateTime.Now,
                                Species = d.species_name,
                                ASP = a.asp,
                                PSP = a.psp,
                                DSP = a.dsp,
                                AZP = a.azp,
                                PTX = a.ptx,
                                YTX = a.ytx,
                                Tissue = a.tissue,
                                Frequency = a.sample_frequency
                            });

                /******
                       Search function - requires location name, species name, date from and date to.
                       Filters first by areaname, then by species, then the date range.

                       *******/


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
                            DateTime sampleDateFrom = Convert.ToDateTime(date);
                            DateTime sampleDateTo = Convert.ToDateTime(dateto);
                            //Search between two dates
                            result = result.Where(r => r.Date >= sampleDateFrom && r.Date <= sampleDateTo);
                        }
                }
            }
        


            return View(result);

            }
            catch(Exception err)
            {
                return View("Could not get samples.");
            }
        }




        //Return the Search Page View
        public ActionResult Search()
        {
            //Code to create dropdown lists for the sample searchbox
            var areaList = new List<String>();
            var speciesList = new List<String>();


            //query to get the areas in alphabetical order from the db
            var areaQuery = from a in db.habs_area
                            orderby a.location_name
                            select a.location_name;

            //query to get the species in alphabetical order from the db
            var speciesQuery = from s in db.habs_species
                               orderby s.species_name
                               select s.species_name;

            //Add areas to the list
            areaList.AddRange(areaQuery.Distinct());


            //Add species to the list
            speciesList.AddRange(speciesQuery.Distinct());

            //Create the SelectList to pass to the view
            ViewBag.areaname = new SelectList(areaList);

            //Create the SelectList to pass to the view
            ViewBag.species = new SelectList(speciesList);


            return View("Search");
        }


    }
}
