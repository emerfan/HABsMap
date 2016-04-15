using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using HABsMap.Controllers;

namespace HABsMapTests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void testMapView()
        {
            var controller = new MapController();
            var result = controller.Index() as ViewResult;
        }
    }
}
 