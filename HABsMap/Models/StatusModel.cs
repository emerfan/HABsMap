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

    public class StatusModel
    {

        //Declare an instance of the database model, used for species query
        msdb2293Entities db = new msdb2293Entities();


        /// <summary>
        /// Data Members for Status Model _ these are returned as JSON via the StatusModel Controller, and they are called by a function which places them
        /// as markers on the LeafletJS map
        /// </summary>
        /// 

        public string Location;
        public decimal latitude;
        public decimal longitude;
        public string sample_status;
        public short species;
        public string spec;
        public DateTime Date;


        //Constructor for Status Model Class
        public StatusModel()
        {
        }

        //Difference getter gets the difference in days between the sample date and today's date as shellfish areas are closed
        //if they are not sampled every 14 days
        public int difference
        {
            get { return (DateTime.Now - Date).Days; }

        }


        //Current Status Getter, checks the difference and returns either a closed / pending status or the sample status
        public string Status
        {
            get {
                    if (difference >= 14)
                        { return "Closed / Pending";}
                    else{ return sample_status;}
                }         
        }

        // Species Name Getter, Calls method which returns the sample species name
        public string Species
        {
            get
            {
                return getSpeciesName();
            }
        }
        
        //Method to get the species name from the habs_species model
        public string getSpeciesName()
        {
            try
            {
                string species = (from h in db.habs_species
                                  where h.species_id == this.species
                                  select h.species_name).First();
                                  return species;
            }
            catch(Exception err)
            {
                return "Species Unknown";
            }


        }

    }
}