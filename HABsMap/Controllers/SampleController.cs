using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HABsMap.Models;
using System.Globalization;

namespace HABsMap.Controllers
{
    public class SampleController : Controller
    {
        private msdb2293Entities db = new msdb2293Entities();



        //GET: Specific Area Samples, Take parameters for the search function
        public ActionResult Index(string areaname, string species, string date, string dateto)
        {

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



                //Check if search parameters were passed
                if (!String.IsNullOrEmpty(areaname) || !String.IsNullOrEmpty(species)
                    || !String.IsNullOrEmpty(date) && !String.IsNullOrEmpty(dateto))
                {
                    result = Utility.Search(result, areaname, species, date, dateto);
                }

                //Pass back to the View as query breadcrumbs
                ViewBag.AreaName = areaname;
                ViewBag.Species = species;
                ViewBag.Date = date;
                ViewBag.DateTo = dateto;

                return View(result);

            }
            catch(Exception err)
            {
                ViewBag.ErrorMessage = "Could not perform search, please try again.";
                return View("Search");
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
            speciesList.Insert(0, String.Empty);

            return View("Search");
        }


    }
}
