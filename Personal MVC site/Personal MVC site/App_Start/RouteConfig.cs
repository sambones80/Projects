using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Personal_MVC_site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "NewSlug",
                url: "Blog/{slug}",
                defaults: new
                {
                    controller = "BlogPosts",
                    action = "Defaults",
                    slug = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            // blog post slug route - Edit
            routes.MapRoute(
                name: "EditSlug",
                url: "Blog/Edit/{slug}",
                defaults: new
                {
                    controller = "BlogPosts",
                    action = "Edit",
                    slug = UrlParameter.Optional
                });
        }
    }
}
