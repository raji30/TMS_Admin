using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
   public partial class TMSDBContext : DbContext
    {
        public TMSDBContext()
           : base("name=postgresEntities")
        {
        }
        public DbSet<deliveryorder> Deliveryorders { get; set; }
        public DbSet<driverdetail> Driverdetails { get; set; }
        public DbSet<address> Addresses { get; set; }
        public DbSet<invoice> Invoices { get; set; }
        public DbSet<dodriverdetail> DODriverDetails { get; set; }
        public DbSet<role> Roles { get; set; }
        public DbSet<shipmentdetail> ShipmentDetail { get; set; }
        public DbSet<addresstype> AddressTypes { get; set; }
        public DbSet<user> Users { get; set; }
        public DbSet<doaddressdetail> DOAddressDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map to the correct Chinook Database tables
            modelBuilder.Entity<deliveryorder>().ToTable("deliveryorder", "public");
            modelBuilder.Entity<driverdetail>().ToTable("driverdetail", "public");
            modelBuilder.Entity<address>().ToTable("address", "public");
            modelBuilder.Entity<invoice>().ToTable("invoice", "public");
            modelBuilder.Entity<dodriverdetail>().ToTable("dodriverdetail", "public");
            modelBuilder.Entity<role>().ToTable("role", "public");
            modelBuilder.Entity<shipmentdetail>().ToTable("shipmentdetail", "public");
            modelBuilder.Entity<addresstype>().ToTable("addresstype", "public");
            modelBuilder.Entity<user>().ToTable("user", "public");
            modelBuilder.Entity<doaddressdetail>().ToTable("doaddressdetail", "public");
            // Chinook Database for PostgreSQL doesn't auto-increment Ids
            modelBuilder.Conventions
                .Remove<StoreGeneratedIdentityKeyConvention>();
        }
    }
}
