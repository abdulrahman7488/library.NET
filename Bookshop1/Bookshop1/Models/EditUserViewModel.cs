using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop1.Models
{
    public class EditUserViewModel
    {
      
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public bool IsAdmin { get; set; }
        

    }
}