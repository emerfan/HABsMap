using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HABsMap.Models;
using HABsMap.Controllers;
using NUnit.Framework;

namespace HABsMapTesting
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestMapDetails()
        {
            var controller = new MapController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}