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
    
    public partial class Review
    {
        public string userID { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> rate { get; set; }
        public string reviewNotes { get; set; }
        public int reviewID { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Product Product { get; set; }
    }
}
