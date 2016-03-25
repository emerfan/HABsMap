using System;
using System.Collections.Generic;
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
        public string currentStatus;
        public DateTime thedate;


        public StatusModel()
        {
            // TODO: Complete member initialization
            setStatus();
        }



        public void setStatus()
        {
            if ((thedate - DateTime.Now).TotalDays < 14)
            {
                this.currentStatus = "Closed/Pending";
            }
            else if ((thedate == DateTime.Now))
            {
                this.currentStatus = "NOW";
            }
            else
            {
                this.currentStatus = status;
            }
        }
    }
}