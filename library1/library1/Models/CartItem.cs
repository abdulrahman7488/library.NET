//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace library1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartItem
    {
        public int CartItemID { get; set; }
        public Nullable<int> CartID { get; set; }
        public Nullable<int> BookID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual User User { get; set; }
    }
}
