//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace library.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.CartItems = new HashSet<CartItem>();
        }
    
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ISBN { get; set; }
        public Nullable<int> PublishedYear { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> number_of_books { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
