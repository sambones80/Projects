namespace Personal_MVC_site.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Personal_MVC_site.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Personal_MVC_site.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(context));
            // Admin
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
                    DisplayName = "Sam"
                }, "spaceball1");
            }
            var adminId = userManager.FindByEmail("sambones80@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            // Moderator
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }
            if (!context.Users.Any(u => u.Email == "sam@sgcreative.net"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "sam@sgcreative.net",
                    Email = "sam@sgcreative.net",
                    FirstName = "Samuel",
                    LastName = "Gray",
                    DisplayName = "Sam-Moderator"
                }, "Pig$h4rk");
            }
            var modId = userManager.FindByEmail("sam@sgcreative.net").Id;
            userManager.AddToRole(modId, "Moderator");

            // Eric-Mod
            if (!context.Users.Any(u => u.Email == "ewatkins@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ewatkins@coderfoundry.com",
                    Email = "ewatkins@coderfoundry.com",
                    FirstName = "Eric",
                    LastName = "Watkins",
                    DisplayName = "EricMod"
                }, "password1");
            }
            var modIdEric = userManager.FindByEmail("ewatkins@coderfoundry.com").Id;
            userManager.AddToRole(modIdEric, "Moderator");

            // Sean-Mod
            if (!context.Users.Any(u => u.Email == "sszpunar@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "sszpunar@coderfoundry.com",
                    Email = "sszpunar@coderfoundry.com",
                    FirstName = "Sean",
                    LastName = "Szpunar",
                    DisplayName = "SeanMod"
                }, "password1");
            }
            var modIdSean = userManager.FindByEmail("sszpunar@coderfoundry.com").Id;
            userManager.AddToRole(modIdSean, "Moderator");
        }
    }
}
