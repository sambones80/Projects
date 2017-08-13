namespace Bug_Tracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bug_Tracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Bug_Tracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //if (!context.Roles.Any(r => r.Name == "Admin"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //}

            //if (!context.Roles.Any(r => r.Name == "Project Manager"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "Project Manager" });
            //}

            //if (!context.Roles.Any(r => r.Name == "Submitter"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "Submitter" });
            //}

            //if (!context.Roles.Any(r => r.Name == "Developer"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "Developer" });
            //}

            //if (!context.Roles.Any(r => r.Name == "Guest"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "Guest" });
            //}

            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //if (!context.Users.Any(u => u.Email == "sambones80@gmail.com"))
            //{
            //    userManager.Create(new ApplicationUser
            //    {
            //        FirstName = "Sam",
            //        LastName = "Gray",
            //        PhoneNumber = "(704) 877-4313",
            //        UserName = "sambones80@gmail.com",
            //        Email = "sambones80@gmail.com",
            //    }, "spaceball1");
            //}

            //if (!context.Users.Any(u => u.Email == "admin@bugtracker.com"))
            //{
            //    userManager.Create(new ApplicationUser
            //    {
            //        FirstName = "Admin",
            //        LastName = "User",
            //        PhoneNumber = "(###) ###-####",
            //        UserName = "admin@bugtracker.com",
            //        Email = "admin@bugtracker.com",
            //    }, "password1");
            //}

            //if (!context.Users.Any(u => u.Email == "pm@bugtracker.com"))
            //{
            //    userManager.Create(new ApplicationUser
            //    {
            //        FirstName = "Project Manager",
            //        LastName = "User",
            //        PhoneNumber = "(###) ###-####",
            //        UserName = "pm@bugtracker.com",
            //        Email = "pm@bugtracker.com",
            //    }, "password1");
            //}

            //if (!context.Users.Any(u => u.Email == "dev@bugtracker.com"))
            //{
            //    userManager.Create(new ApplicationUser
            //    {
            //        FirstName = "Developer",
            //        LastName = "User",
            //        PhoneNumber = "(###) ###-####",
            //        UserName = "dev@bugtracker.com",
            //        Email = "dev@bugtracker.com",
            //    }, "password1");
            //}

            //if (!context.Users.Any(u => u.Email == "submitter@bugtracker.com"))
            //{
            //    userManager.Create(new ApplicationUser
            //    {
            //        FirstName = "Submitter",
            //        LastName = "User",
            //        PhoneNumber = "(###) ###-####",
            //        UserName = "submitter@bugtracker.com",
            //        Email = "submitter@bugtracker.com",
            //    }, "password1");
            //}

            //if (!context.Users.Any(u => u.Email == "guest@bugtracker.com"))
            //{
            //    userManager.Create(new ApplicationUser
            //    {
            //        FirstName = "Guest",
            //        LastName = "User",
            //        PhoneNumber = "(###) ###-####",
            //        UserName = "guest@bugtracker.com",
            //        Email = "guest@bugtracker.com",
            //    }, "password1");
            //}

            //var userIdSam = userManager.FindByEmail("sambones80@gmail.com").Id;
            //userManager.AddToRole(userIdSam, "Admin");

            //var userIdAdmin = userManager.FindByEmail("admin@bugtracker.com").Id;
            //userManager.AddToRole(userIdAdmin, "Admin");

            //var userIdPm = userManager.FindByEmail("pm@bugtracker.com").Id;
            //userManager.AddToRole(userIdPm, "Project Manager");

            //var userIdDev = userManager.FindByEmail("dev@bugtracker.com").Id;
            //userManager.AddToRole(userIdDev, "Developer");

            //var userIdSub = userManager.FindByEmail("submitter@bugtracker.com").Id;
            //userManager.AddToRole(userIdSub, "Submitter");

            //var userIdGuest = userManager.FindByEmail("guest@bugtracker.com").Id;
            //userManager.AddToRole(userIdGuest, "Guest");
        }
    }
}
