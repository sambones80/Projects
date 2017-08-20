using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Bug_Tracker.Controllers
{
    public class AttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attachments
        public ActionResult Index()
        {
            var attachments = db.Attachments.Include(a => a.AuthorUser).Include(a => a.Ticket);
            return View(attachments.ToList());
        }

        // GET: Attachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // GET: Attachments/Create
        public ActionResult Create()
        {
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            return View();
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,Body,Created,AuthorUserId,FileUrl,FileDisplayName")] Attachment attachment, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Only allow certain file types
                var ext = Path.GetExtension(file.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                {
                    ModelState.AddModelError("file", "Invalid Format.");
                }
            }
            if (ModelState.IsValid)
            {
                if (attachment != null)
                {
                    // Relative server path
                    var filePath = "/Uploads/";
                    // Path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    // File url for relative path
                    attachment.FileUrl = filePath + file.FileName;
                    // Save file
                    file.SaveAs(Path.Combine(absPath, file.FileName));
                }

                attachment.AuthorUserId = User.Identity.GetUserId();
                attachment.Created = DateTimeOffset.Now;
                db.Attachments.Add(attachment);
                db.SaveChanges();
                var thisTicket = db.Tickets.Find(attachment.TicketId);
                if (thisTicket != null)
                {
                    return RedirectToAction("Edit", "Tickets", new { id = thisTicket.Id });
                }
            }
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", attachment.AuthorUserId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", attachment.TicketId);
            return View(attachment);
        }

        // GET: Attachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", attachment.AuthorUserId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", attachment.TicketId);
            return View(attachment);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,Body,Created,AuthorUserId,FileUrl,FileDisplayName")] Attachment attachment, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (attachment != null)
                {
                    // Relative server path
                    var filePath = "/Uploads/";
                    // Path on physical drive on server
                    var absPath = Server.MapPath("~" + filePath);
                    // File url for relative path
                    attachment.FileUrl = filePath + attachment.FileDisplayName;
                    // Save file
                    file.SaveAs(Path.Combine(absPath, attachment.FileDisplayName));
                }

                attachment.AuthorUserId = User.Identity.GetUserId();
                attachment.Created = DateTimeOffset.Now;
                db.Attachments.Add(attachment);
                db.Entry(attachment).State = EntityState.Modified;
                db.SaveChanges();
                var thisTicket = db.Tickets.Find(attachment.TicketId);
                if (thisTicket != null)
                {
                    return RedirectToAction("Edit", "Tickets", new { id = thisTicket.Id });
                }
            }
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", attachment.AuthorUserId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", attachment.TicketId);
            return View(attachment);
        }

        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attachment attachment = db.Attachments.Find(id);
            db.Attachments.Remove(attachment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
