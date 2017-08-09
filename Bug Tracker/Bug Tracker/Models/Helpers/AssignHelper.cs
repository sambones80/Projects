using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.Models
{
    public class AssignHelper
    {
        private ApplicationDbContext db;

        public AssignHelper(ApplicationDbContext context)
        {
            //this.userManager = new UserManager<ApplicationUser>(
            //    new UserStore<ApplicationUser>(context));
            //this.roleManager = new RoleManager<IdentityRole>(
            //    new RoleStore<IdentityRole>(context));
            this.db = context;
        }

        public bool IsProjectOnUser(int projectId, string userId)
        {
            var project = db.Projects.Find(projectId);
            var userCheck = project.Users.Any(p => p.Id == userId);
            return (userCheck);
        }

        //public string GetUserInRole(string userId)
        //{
        //    return userManager.GetRoles(userId).First();
        //}

        //public IList<string> ListAssignedUsers(int projectId)
        public ICollection<ApplicationUser> ListAssignedUsers(int projectId)
        {
            Project project = db.Projects.Find(projectId);
            var userList = project.Users.ToList();
            return (userList);
        }

        public bool AddProjectToUser(int projectId, string userId)
        {
            Project project = db.Projects.Find(projectId);
            ApplicationUser user = db.Users.Find(userId);

            project.Users.Add(user);
            
            try
            {
                var userAdded = db.SaveChanges();

                if (userAdded != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }            

            //var roles = userManager.GetRoles(userId);
            //foreach(var role in roles)
            //{
            //    RemoveUserFromRole(userId, role);
            //}
            //var result = userManager.AddToRole(projectId, userId);
            //return result.Succeeded;
        }

        public bool RemoveProjectFromUser(int projectId, string userId)
        {
            Project project = db.Projects.Find(projectId);
            ApplicationUser user = db.Users.Find(userId);

            var result = project.Users.Remove(user);

            try
            {
                var userRemoved = db.SaveChanges();

                if (userRemoved != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            //var result = userManager.RemoveFromRole(projectId, userId);
            //return result.Succeeded;
        }

        //public bool AddTicketToUser(int ticketId, string userId)
        //{
        //    Ticket ticket = db.Tickets.Find(ticketId);
        //    ApplicationUser user = db.Users.Find(userId);

        //    ticket.AssignedToUserId.Add

        //    try
        //    {
        //        var userAdded = db.SaveChanges();

        //        if (userAdded != 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    //var roles = userManager.GetRoles(userId);
        //    //foreach(var role in roles)
        //    //{
        //    //    RemoveUserFromRole(userId, role);
        //    //}
        //    //var result = userManager.AddToRole(projectId, userId);
        //    //return result.Succeeded;
        //}

        //NEEDED?
        //public IList<ApplicationUser> UsersInRole(string roleName)
        //{
        //    var userIDs = roleManager.FindByName(roleName).Users.Select(r => r.UserId);
        //    return userManager.Users.Where(u => userIDs.Contains(u.Id)).ToList();
        //    //Select(u => new UserDropDownViewModel { Id = u.Id, Name = u.DisplayName }).
        //}

        //NEEDED?
        //public IList<ApplicationUser> UsersNotInRole(string roleName)
        //{
        //    var userIDs = System.Web.Security.Roles.GetUsersInRole(roleName);
        //    return userManager.Users.Where(u => !userIDs.Contains(u.Id)).ToList();
        //    //Select(u => new UserDropDownViewModel { Id = u.Id, Name = u.DisplayName }).
        //}

        //public IList<string> ListUserRoles(string userId)
        //{
        //    return manager.GetRoles(userId);
        //}

        //public bool AddUserToRole(string userId, string roleName)
        //{
        //    var result = manager.AddToRole(userId, roleName);
        //    return result.Succeeded;
        //}

        //public bool RemoveUserFromRole(string userId, string roleName)
        //{
        //    var result = manager.RemoveUserFromRole(string userId, string roleName);
        //    return result.Succeeded;
        //}
    }
}
