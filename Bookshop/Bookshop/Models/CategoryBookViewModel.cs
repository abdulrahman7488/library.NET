using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop.Models
{
    public class CategoryBookViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}