using HABsMap.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HABsMap
{
    public class Utility
    {

        //Search Method
        public static IQueryable<SampleModel>Search( IQueryable<SampleModel> result, string areaname, string species, string date, string dateto)
        {
            //Variable Date to use if dates are wrong way around
            DateTime testdate;

            //CultureInfo to format date correctly for NUIG server search
            CultureInfo en = new CultureInfo("en-US");

            //Search By Date
            if (!String.IsNullOrEmpty(date) && !String.IsNullOrEmpty(dateto))
            {

                //Convert to datetime and specify american format as was causing problems on server
                DateTime sampleDateFrom = Convert.ToDateTime(date, en);
                DateTime sampleDateTo = Convert.ToDateTime(dateto, en);


                //Check if the dates right way round

                if (sampleDateFrom > sampleDateTo)
                {
                    //Testdate = smaller date
                    testdate = sampleDateTo;
                    //set date to to the bigger date
                    sampleDateTo = sampleDateFrom;
                    //set from to the smaller date
                    sampleDateFrom = testdate;

                }

                //Search between two dates
                result = result.Where(r => r.Date >= sampleDateFrom && r.Date <= sampleDateTo);
            }

            if (!String.IsNullOrEmpty(areaname))
            {
                result = result.Where(r => r.Location.Contains(areaname));

            }

            //Name and Species
            if (!String.IsNullOrEmpty(species))
            {
                result = result.Where(r => r.Species.Contains(species)).OrderByDescending(r => r.Date); ;

            }

            return result;

        }
    }
}