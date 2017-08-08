using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bug_Tracker.Models
{
	public class UserListViewModel
    {
        //public IEnumerable<ApplicationUser> Users { get; set; }
        //public IEnumerable<IdentityRole> Roles { get; set; }
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}