﻿using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeter.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
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
    }
}