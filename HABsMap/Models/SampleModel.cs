using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace HABsMap.Models
{
    /// <summary>
    /// This model represents the result of  a LINQ query in the StatusModel class, which takes the most recent sample joined with
    /// the habs_area model and returns the areas which are added to the map along with the most recent sample information.
    /// </summary>
    /// 

    public class SampleModel
    {
        /// <summary>
        /// Data Members for Status Model _ these are returned as JSON via the StatusModel Controller, and they are called by a function which places them
        /// as markers on the LeafletJS map
        /// </summary>
        /// 


        public string Location;
        public string Status;
        public DateTime Date;
        public string Species;
        public string ASP;
        public string PSP;
        public string DSP;
        public string AZP;
        public string PTX;
        public string YTX;
        public string Tissue;

        public SampleModel()
        {

        }


    }
}