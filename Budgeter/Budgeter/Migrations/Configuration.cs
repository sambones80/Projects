namespace Budgeter.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Budgeter.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Budgeter.Models.ApplicationDbContext context)
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

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Household Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Household Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Household User"))
            {
                roleManager.Create(new IdentityRole { Name = "Household User" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "sam@sgcreative.net"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Samuel",
                    LastName = "Gray",
                    DisplayName = "Sam",
                    PhoneNumber = "(704) 000-0000",
                    UserName = "sam@sgcreative.net",
                    Email = "sam@sgcreative.net",
                }, "spaceball1");
            }

            if (!context.Users.Any(u => u.Email == "sambones80@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Sam",
                    LastName = "Gray",
                    DisplayName = "Sambones",
                    PhoneNumber = "(704) 000-0000",
                    UserName = "sambones80@gmail.com",
                    Email = "sambones80@gmail.com",
                }, "spaceball1");
            }

            if (!context.Users.Any(u => u.Email == "samsspambox@yahoo.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Rachel",
                    LastName = "Gray",
                    DisplayName = "Rachel",
                    PhoneNumber = "(704) 000-0000",
                    UserName = "samsspambox@yahoo.com",
                    Email = "samsspambox@yahoo.com",
                }, "spaceball1");
            }

            var userIdAdmin = userManager.FindByEmail("sam@sgcreative.net").Id;
            userManager.AddToRole(userIdAdmin, "Admin");

            var userIdHAdmin = userManager.FindByEmail("sam@sgcreative.net").Id;
            userManager.AddToRole(userIdHAdmin, "Household Admin");

            var userIdUser = userManager.FindByEmail("sam@sgcreative.net").Id;
            userManager.AddToRole(userIdUser, "Household User");

        }
    }
}
