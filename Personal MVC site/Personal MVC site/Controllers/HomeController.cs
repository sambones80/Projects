using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personal_MVC_site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ComingSoon()
        {
            ViewBag.Message = "Coming Soon";

            return View();
        }
        public ActionResult GDGallery()
        {
            ViewBag.Message = "Graphic Design";

            return View();
        }
        public ActionResult Blog()
        {
            ViewBag.Message = "Blog";

            return View();
        }
        public ActionResult Numbers()
        {
            ViewBag.Message = "Numbers";

            return View();
        }

    }
}