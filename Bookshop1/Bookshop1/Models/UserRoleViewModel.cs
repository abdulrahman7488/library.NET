using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop1.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}