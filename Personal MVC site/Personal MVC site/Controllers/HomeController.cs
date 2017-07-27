using Personal_MVC_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Web.Mvc;

namespace Personal_MVC_site.Controllers
{
    [RequireHttps]
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
        public ActionResult Sent()
        {
            ViewBag.Message = "Thanks for Contacting Me";

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold>({1})</p><p>{2}</p>";
                    var email = new MailMessage(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["emailfrom"])
                    {
                        Subject = "Portfolio Contact Email",
                        Body = string.Format(body, model.ToName, model.ToEmail, model.Body),
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return RedirectToAction("Sent");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View();
        }
    }
}