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
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.AuthorUser).Include(t => t.Priority).Include(t => t.Project).Include(t => t.Status).Include(t => t.Type);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create(int id)
        {
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName");
            //ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.PriorityId = new SelectList(db.Priority, "Id", "Name");
            ViewBag.ProjectId = id;
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Created,Updated,ProjectId,TypeId,PriorityId,StatusId,AssignedToUserId")] Ticket ticket, int projectId)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                ticket.AuthorUserId = userId;
                ticket.ProjectId = projectId;
                ticket.Created = DateTimeOffset.Now;
                ticket.StatusId = 1;
                ticket.PriorityId = 5;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            //ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AuthorUserId);
            ViewBag.PriorityId = new SelectList(db.Priority, "Id", "Name", ticket.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", ticket.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", ticket.StatusId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", ticket.TypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ApplicationDbContext context = new ApplicationDbContext();
            Project project = db.Projects.Find(ticket.ProjectId);
            var projectUsers = context.Users.Where(u => u.Projects.Any(p => p.Title == project.Title));
            var role = context.Roles.SingleOrDefault(u => u.Name == "Developer");
            var usersInRole = context.Users.Where(u => u.Roles.Any(r => (r.RoleId == role.Id)));
            var displayUsers = usersInRole.Where(u => u.Projects.Any(p => (p.Title == project.Title)));
            var removeUser = db.Users.Where(u => (u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)"));

            ViewBag.AssignedToUserId = new SelectList(displayUsers, "Id", "FirstName", ticket.AssignedToUserId);
            //ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AuthorUserId);
            ViewBag.PriorityId = new SelectList(db.Priority, "Id", "Name", ticket.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", ticket.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", ticket.StatusId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", ticket.TypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Created,Updated,ProjectId,TypeId,PriorityId,Status,StatusId,AuthorUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                if (ticket.StatusId == 6)
                {
                    ticket.Deleted = true;
                }
                if (ticket.AssignedToUserId != null && ticket.StatusId < 2)
                {
                    ticket.StatusId = 2;
                }
                ticket.Updated = DateTimeOffset.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ApplicationDbContext context = new ApplicationDbContext();
            Project project = db.Projects.Find(ticket.ProjectId);
            var projectUsers = context.Users.Where(u => u.Projects.Any(p => p.Title == project.Title));
            var role = context.Roles.SingleOrDefault(u => u.Name == "Developer");
            var usersInRole = context.Users.Where(u => u.Roles.Any(r => (r.RoleId == role.Id)));
            var displayUsers = usersInRole.Where(u => u.Projects.Any(p => (p.Title == project.Title)));
            var removeUser = db.Users.Where(u => (u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)"));

            ViewBag.AssignedToUserId = new SelectList(displayUsers, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.AuthorUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AuthorUserId);
            ViewBag.PriorityId = new SelectList(db.Priority, "Id", "Name", ticket.PriorityId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", ticket.ProjectId);
            ViewBag.StatusId = new SelectList(db.Statuses, "Id", "Name", ticket.StatusId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", ticket.TypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            //db.Tickets.Remove(ticket);
            ticket.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // GET: Tickets/Assign/5
        //[Authorize(Roles = "Admin, Project Manager")]
        //public ActionResult Assign(int id)
        //{
        //    var ticket = db.Tickets.Find(id);
        //    var project = ticket.ProjectId;
        //    AssignHelper helper = new AssignHelper(db);
        //    var model = new AssignUsersViewModel();

        //    model.Ticket = ticket;
        //    model.SelectedUsers = helper.ListAssignedUsers(id).Select(u => u.Id).ToArray();
        //    model.Users = new SelectList(db.Users.Where(u => (u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)")).OrderBy(u => u.FirstName), "Id", "DisplayName", model.SelectedUsers);

        //    return View(model);
        //}

        // POST: Tickets/Assign/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Authorize(Roles = "Admin, Project Manager")]
        //public ActionResult Assign(Ticket ticket)
        //{

        //}

        //{
        //    var ticket = db.Tickets.Find(model.Ticket.Id);
        //    AssignHelper helper = new AssignHelper(db);

        //    foreach (var user in db.Users.Select(r => r.Id).ToList())
        //    {
        //        helper.RemoveProjectFromUser(ticket.Id, user);
        //    }
        //    if (model.SelectedUser != null)
        //    {
        //        foreach (var user in model.SelectedUser)
        //        {
        //            helper.AddProjectToUser(ticket.Id, user);
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

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
