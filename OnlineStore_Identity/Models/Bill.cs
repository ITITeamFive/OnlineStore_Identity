//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStore_Identity.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            //this.BillProducts = new HashSet<BillProduct>();
        }
    
        public int billID { get; set; }
        public Nullable<double> billSubTotal { get; set; }
        public Nullable<double> billTotal { get; set; }
        public Nullable<System.DateTime> billDate { get; set; }
        public string billNotes { get; set; } = "Thank you for visiting us";
        public Nullable<int> addressID { get; set; }
        public Nullable<int> paymentID { get; set; }
        public string userID { get; set; }
    
        //public virtual Address Address { get; set; }
        //public virtual Payment Payment { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BillProduct> BillProducts { get; set; }
        //public virtual AspNetUser AspNetUser { get; set; }
    }
}
