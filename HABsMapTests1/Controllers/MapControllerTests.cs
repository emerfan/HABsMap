using Microsoft.VisualStudio.TestTools.UnitTesting;
using HABsMap.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HABsMap.Models;

namespace HABsMap.Controllers.Tests
{
    [TestClass()]
    public class MapControllerTests
    {
        //Declare an instance of the database model
        msdb2293Entities db = new msdb2293Entities();

        [TestMethod()]
        public void IndexTest()
        {
            var controller = new MapController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }
    }
}