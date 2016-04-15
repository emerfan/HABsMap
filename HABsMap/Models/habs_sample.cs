//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HABsMap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class habs_sample
    {
        public string sample_id { get; set; }
        public string location_id { get; set; }
        public short species_id { get; set; }
        public string report_id { get; set; }
        public string tissue { get; set; }
        public string asp { get; set; }
        public string azp { get; set; }
        public string dsp { get; set; }
        public string ptx { get; set; }
        public string ytx { get; set; }
        public string psp { get; set; }
        public string sample_status { get; set; }
        public Nullable<System.DateTime> date_sampled { get; set; }
        public Nullable<System.DateTime> sample_date { get; set; }
        public Nullable<short> sample_frequency { get; set; }
    
        public virtual habs_area habs_area { get; set; }
        public virtual habs_species habs_species { get; set; }
    }
}
