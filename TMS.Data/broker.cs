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
    
    public partial class broker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public broker()
        {
            this.contacts = new HashSet<contact>();
        }
    
        public System.Guid brokerkey { get; set; }
        public string brokerid { get; set; }
        public string brokername { get; set; }
        public Nullable<System.Guid> addrkey { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<short> status { get; set; }
        public Nullable<System.DateTime> statusdate { get; set; }
    
        public virtual address address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contact> contacts { get; set; }
    }
}
