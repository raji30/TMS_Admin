﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TMS.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class App_modelEntities : DbContext
    {
        public App_modelEntities()
            : base("name=App_modelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<approle> approles { get; set; }
        public virtual DbSet<broker> brokers { get; set; }
        public virtual DbSet<carrier> carriers { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<customeraddress> customeraddresses { get; set; }
        public virtual DbSet<document> documents { get; set; }
        public virtual DbSet<documenttype> documenttypes { get; set; }
        public virtual DbSet<driver> drivers { get; set; }
        public virtual DbSet<invoicedetail> invoicedetails { get; set; }
        public virtual DbSet<invoiceheader> invoiceheaders { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<shippingport> shippingports { get; set; }
        public virtual DbSet<shippingportterminal> shippingportterminals { get; set; }
        public virtual DbSet<tms_containersize> tms_containersize { get; set; }
        public virtual DbSet<tms_holdreason> tms_holdreason { get; set; }
        public virtual DbSet<tms_orderdetail> tms_orderdetail { get; set; }
        public virtual DbSet<tms_orderdetailcomments> tms_orderdetailcomments { get; set; }
        public virtual DbSet<tms_orderheader> tms_orderheader { get; set; }
        public virtual DbSet<tms_orderheadercomments> tms_orderheadercomments { get; set; }
        public virtual DbSet<tms_ordersource> tms_ordersource { get; set; }
        public virtual DbSet<tms_orderstatus> tms_orderstatus { get; set; }
        public virtual DbSet<tms_ordertype> tms_ordertype { get; set; }
        public virtual DbSet<tms_priority> tms_priority { get; set; }
        public virtual DbSet<tms_routes> tms_routes { get; set; }
        public virtual DbSet<vendor> vendors { get; set; }
        public virtual DbSet<voucherdetail> voucherdetails { get; set; }
        public virtual DbSet<voucherheader> voucherheaders { get; set; }
        public virtual DbSet<warehouse> warehouses { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
