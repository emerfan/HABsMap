using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HABsMap.Models
{
    public class StatusModel
    {
        /// <summary>
        /// Data Members for Status Model
        /// </summary>
        public string location_name;
        public decimal latitude;
        public decimal longitude;
        public string status;
        public DateTime sampDate;


        public StatusModel()
        {
        }


        public int difference
        {
            get { return (DateTime.Now - sampDate).Days; }

        }


        public string getCurrentStatus
        {
            get {
                    if (difference >= 14)
                        {
                            return "Closed";
                        }
                    else
                        {
                            return status;
                        }
                }
           
        }

    }
}