using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HABsMap.Models;

namespace HABsMap.Controllers
{
    public class MapController : Controller
    {
        //Declare an instance of the database model
        msdb2293Entities db = new msdb2293Entities();
        // GET: Map
        public ActionResult Index()
        {
            //Test

            //Code to create a Dropdown list of the areas
            //List to hold the areas for the dropdown
            var areaList = new List<String>();

            //query to get the areas in alphabetical order from the list
            var areaQuery = from a in db.habs_area
                            orderby a.location_name
                            select a.location_name;

            //Add to areas to the list
            areaList.AddRange(areaQuery.Distinct());

            //Create the SelectList to pass to the view
            ViewBag.areaname = new SelectList(areaList);


            return View("Index");
        }
    }
}