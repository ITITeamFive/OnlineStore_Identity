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

    public partial class Store
    {
        public Nullable<int> productID { get; set; }
        public string productColor { get; set; }
        public string productSize { get; set; }
        public byte[] productImage { get; set; }
        //public string productImage { get; set; }
        public Nullable<int> productQuantity { get; set; }
        public int ID { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        //public virtual ICollection<BillProduct> BillProducts { get; set; }

    }
}