using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop1.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; } // لرفع الملف من المستخدم

    }
}