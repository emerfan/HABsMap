﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class msdb2293Entities : DbContext
    {
        public msdb2293Entities()
            : base("name=msdb2293Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<habs_alert> habs_alert { get; set; }
        public virtual DbSet<habs_area> habs_area { get; set; }
        public virtual DbSet<habs_sample> habs_sample { get; set; }
        public virtual DbSet<habs_species> habs_species { get; set; }
    }
}
