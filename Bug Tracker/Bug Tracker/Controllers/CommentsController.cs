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

namespace Bug_Tracker.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.AuthorUser).Include(c => c.Ticket);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,Created,Updated,TicketId,AuthorUserId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.AuthorUserId = User.Identity.GetUserId();
                comment.Created = DateTimeOffset.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                var thisTicket = db.Tickets.Find(comment.TicketId);
                if (thisTicket != null)
                {
                    return RedirectToAction("Edit", "Tickets", new { id = thisTicket.Id });
                }
            }

            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorUserId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", comment.TicketId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorUserId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", comment.TicketId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Created,Updated,TicketId,AuthorUserId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorUserId);
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", comment.TicketId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
