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
    //using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            //this.BillProducts = new HashSet<BillProduct>();
            //this.WishLists = new HashSet<WishList>();
            this.Reviews = new HashSet<Review>();
        }

        public int productID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(12)]
        public string productName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string productBrand { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string productMaterial { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<double> productPrice { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<double> productDiscount { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string productDescription { get; set; }
        
        public Nullable<int> classID { get; set; }
       
        public Nullable<int> categoryID { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //[JsonIgnore]

        //public virtual ICollection<BillProduct> BillProducts { get; set; }
        //[JsonIgnore]
        public virtual Category Category { get; set; }
        //[JsonIgnore]

        //public virtual Class Class { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //[JsonIgnore]

        //public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
