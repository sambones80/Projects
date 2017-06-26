namespace Shopping_Cart_01.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Shopping_Cart_01.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Shopping_Cart_01.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "sambones80@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "sambones80@gmail.com",
                    Email = "sambones80@gmail.com",
                    FirstName = "Sam",
                    LastName = "Gray",
                    DisplayName = "Sambones80"
                }, "spaceball1");
            }

            var userId = userManager.FindByEmail("sambones80@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");
        }
    }
}
