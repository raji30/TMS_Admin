//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public driver()
        {
            this.tms_routes = new HashSet<tms_routes>();
        }
    
        public System.Guid driverkey { get; set; }
        public string driverid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public System.Guid addrkey { get; set; }
        public Nullable<System.Guid> carrierkey { get; set; }
        public string drivinglicenseno { get; set; }
        public Nullable<System.DateTime> drivinglicenseexpirydate { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<System.DateTime> statusdate { get; set; }
        public Nullable<System.Guid> vendkey { get; set; }
    
        public virtual address address { get; set; }
        public virtual carrier carrier { get; set; }
        public virtual vendor vendor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tms_routes> tms_routes { get; set; }
    }
}
