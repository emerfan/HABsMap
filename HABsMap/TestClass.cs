using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HABsMap.Models;
using HABsMap.Controllers;
using NUnit.Framework;

namespace HABsMap
{
    [TestFixture]
    public class TestClass
    {
        //Declare an Instance of Entities
        msdb2293Entities db = new msdb2293Entities();


        //Test Map View
        [Test]
        public void testMapView()
        {
            var controller = new MapController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }



        [Test]
        //Test Search View
        public void testSearchView()
        {
            var controller = new SampleController();
            var result = controller.Search() as ViewResult;
            Assert.AreEqual("Search", result.ViewName);
        }



        //Test Sample Search Queries Species - Using Join Query from Sample Search
        [Test]
        public void testAreaSearch()
        {
            string areaname = "Rosmuc";
            //Return all the samples and the area name.
            var result = (
                            from a in db.habs_sample
                            join c in db.habs_area
                            on a.location_id equals c.location_id
                            join d in db.habs_species
                            on a.species_id equals d.species_id
                            where c.location_name.Equals(areaname)
                            select c.location_name);


            string newResult = result.First().ToString();
            Assert.AreEqual("Rosmuc", newResult);

        }


        //Test Sample Search Queries Species Name - Using Join Query from Sample Search
        [Test]
        public void testSpeciesSearch()
        {
            string speciesname = "Crassostrea gigas";
            //Return all the samples and the area name.
            var result = (
                            from a in db.habs_sample
                            join c in db.habs_area
                            on a.location_id equals c.location_id
                            join d in db.habs_species
                            on a.species_id equals d.species_id
                            where d.species_name.Equals(speciesname)
                            select d.species_name);


            string newResult = result.First().ToString();
            Assert.AreEqual("Crassostrea gigas", newResult);

        }


        //Test Sample Search Queries Date - Using Join Query from Sample Search
        [Test]
        public void testDateSearch()
        {
            //String date to simulate
            string date = "01/19/2016";

            //Convert to DateTime
            DateTime sampdate = Convert.ToDateTime(date);

            //Return all the samples and the area name.
            var result = (
                            from a in db.habs_sample
                            join c in db.habs_area
                            on a.location_id equals c.location_id
                            join d in db.habs_species
                            on a.species_id equals d.species_id
                            where c.location_name.Equals("Rosmuc")
                            select a.date_sampled);


            string newResult = result.First().ToString();
            DateTime dateResult = Convert.ToDateTime(newResult);
            
            Assert.AreEqual(sampdate, dateResult);

        }


        [Test]
        //Test StatusModel to assert that the model is returning the sample status if the sample date difference is less than frequency specified.
        public void testStatusModelOriginalStatus()
        {
            //Test date 
            string testDate = "04/15/2016";
            //Convert to DateTime
            DateTime dateTime = Convert.ToDateTime(testDate);
            //Create New Status Model
            StatusModel status = new StatusModel();
            //Set Frequency
            status.frequency = 3;
            //Set Status
            status.sample_status = "Open";
            //Set Date
            status.Date = dateTime;
            //Get Model Status
            string result = status.Status;
            //Assert Equals
            Assert.AreEqual("Open", result);
        }


        [Test]
        //Test StatusModel to assert that the model is returning Closed / Pending if the sample date difference is greater than frequency specified.
        public void testStatusModelClosedPending()
        {
            //Test date 
            string testDate = "04/10/2016";
            //Convert to DateTime
            DateTime dateTime = Convert.ToDateTime(testDate);
            //Create New Status Model
            StatusModel status = new StatusModel();
            //Set Frequency
            status.frequency = 3;
            //Set Status
            status.sample_status = "Open";
            //Set Date
            status.Date = dateTime;
            //Get Model Status
            string result = status.Status;
            //Assert Equals
            Assert.AreEqual("Closed / Pending", result);
        }



    }
}