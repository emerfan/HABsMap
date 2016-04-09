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
        /// <summary>
        /// Data Members for Status Model _ these are returned as JSON via the StatusModel Controller, and they are called by a function which places them
        /// as markers on the LeafletJS map
        /// </summary>
        /// 
        public string location_name;
        public decimal latitude;
        public decimal longitude;
        public string status;
        public short species;
        public string spec;
        public DateTime sampDate;


        //Constructor for Status Model Class
        public StatusModel()
        {
        }

        //Difference getter gets the difference in days between the sample date and today's date as shellfish areas are closed
        //if they are not sampled every 14 days
        public int difference
        {
            get { return (DateTime.Now - sampDate).Days; }

        }


        //Current Status Getter, checks the difference and returns either a closed / pending status or the sample status
        public string getCurrentStatus
        {
            get {
                    if (difference <= 14)
                        { return "Closed / Pending ";}
                    else{ return status;}
                }         
        }

        // Species Name Getter, Calls method which returns the sample species name from the database.
       /* public string species_name
        {
            get
            {
                return getSpeciesName();
            }
        }
        */
        //Method to return the species names from the database - Had terrible trouble with LINQ query joining three tables 
        //Into the StatusModel model, so got it working this way as was vital for requirements. Not delighted about it but it works.

        /*public string getSpeciesName()
        {
           try
            {
                //SqlConnection
                SqlConnection conn = new SqlConnection("Server=lugh4.it.nuigalway.ie;Database=msdb2293;Uid=msdb2293;Pwd = msdb2293EM;");
                //Open Connection
                conn.Open();
                //Create a SQL Command
                SqlCommand command = new SqlCommand("select species_name from habs_species where species_id =" + species + ";", conn);
                //SqlDataReader to read the data
                SqlDataReader rdr = null;
                //Execute the Reader with the SQL Command
                rdr = command.ExecuteReader();
                //While There is Data to be read
                while (rdr.Read())
                {
                    //Get the result and assign it to spec
                    spec = rdr[0].ToString();
                }
                //Return the species name
                return spec;

            }
            catch(Exception e)
            {
                return "Species Not Available";
            }
        }
        */

    }
}