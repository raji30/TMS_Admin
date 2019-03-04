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
    
    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            this.customeraddresses = new HashSet<customeraddress>();
            this.tms_orderheader = new HashSet<tms_orderheader>();
            this.contacts = new HashSet<contact>();
        }
    
        public System.Guid custkey { get; set; }
        public string custid { get; set; }
        public string custname { get; set; }
        public System.Guid addrkey { get; set; }
        public System.DateTime createdate { get; set; }
        public Nullable<short> customergroup { get; set; }
        public short status { get; set; }
        public System.DateTime statusdate { get; set; }
        public Nullable<decimal> creditlimit { get; set; }
        public Nullable<short> creditstatus { get; set; }
        public Nullable<bool> creditcheck { get; set; }
    
        public virtual address address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customeraddress> customeraddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tms_orderheader> tms_orderheader { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contact> contacts { get; set; }
    }
}
