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
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;

namespace Bug_Tracker.Controllers
{
    public class TicketsController : Controller
    {
        // GET: Tickets
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(DashboardViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = User.Identity.GetUserId();

            List<Project> relevantProjects = new List<Project>();
            List<Project> irrelevantProjects = new List<Project>();
            List<Ticket> relevantTickets = new List<Ticket>();
            List<Ticket> allTickets = new List<Ticket>();

            foreach (var project in db.Projects.ToList())
            {
                bool relevantProjectflag = false;

                foreach (var ticket in project.Tickets)
                {
                    allTickets.Add(ticket);
                }

                // Find Projects that users are assigned to and mark them Relevant
                foreach (var projectUser in project.Users)
                {
                    if (userId == projectUser.Id)
                    {
                        relevantProjectflag = true;
                    }
                }

                // Filter tickets ONLY for projects that are assigned to users
                // Find tickets ON Projects that are assigned to the PM
                // Find tickets that are assigned to Devs and created by Subs
                if (relevantProjectflag)
                {
                    relevantProjects.Add(project);

                    foreach (var ticket in project.Tickets)
                    {
                        if (User.IsInRole("Project Manager")
                        || ((User.IsInRole("Developer") && userId == ticket.AssignedToUserId) || User.IsInRole("Developer") && userId == ticket.AuthorUserId) || (User.IsInRole("Submitter") && userId == ticket.AuthorUserId))
                        {
                            relevantTickets.Add(ticket);
                        }
                    }
                }

                // Filter tickets ONLY for projects that are NOT assigned to users
                // Find tickets that were created by the PM
                else
                {
                    if (User.IsInRole("Project Manager"))
                    {
                        irrelevantProjects.Add(project);
                        foreach (var ticket in project.Tickets)
                        {
                            if (User.IsInRole("Project Manager") && userId == ticket.AuthorUserId)
                            {
                                relevantTickets.Add(ticket);
                            }
                        }
                    }
                }
            }
            model.RelevantProjects = relevantProjects;
            model.RelevantTickets = relevantTickets;

            if (User.IsInRole("Admin"))
            {
                model.AllTickets = allTickets;
            }

            return View(model);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Body,Created,Updated,ProjectId,TypeId,PriorityId,Status,StatusId,AuthorUserId,AssignedToUserId")] Ticket ticket)
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
                Ticket oldTicket = new ApplicationDbContext().Tickets.Find(ticket.Id);
                if (oldTicket.AssignedToUserId != ticket.AssignedToUserId)
                {
                    var history = new History
                    {
                        ChangeDate = new DateTimeOffset(DateTime.Now),
                        TicketId = ticket.Id,
                        UserId = User.Identity.GetUserId(),
                        Property = "The assigned user has been changed to",
                        NewValue = db.Users.Find(ticket.AssignedToUserId).FirstName,
                    };
                    db.Histories.Add(history);

                    ticket.Updated = DateTimeOffset.Now;
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();

                    await Notify(ticket.Id, history);
                }

                    if (oldTicket.StatusId != ticket.StatusId)
                    {
                        var history = new History
                        {
                            ChangeDate = new DateTimeOffset(DateTime.Now),
                            TicketId = ticket.Id,
                            UserId = User.Identity.GetUserId(),
                            Property = "The status has been set to",
                            OldValue = oldTicket.Status.Name,
                            NewValue = db.Statuses.Find(ticket.StatusId).Name
                        };
                        db.Histories.Add(history);

                        await Notify(ticket.Id, history);
                    }

                    if (oldTicket.Title != ticket.Title)
                    {
                        var history = new History
                        {
                            ChangeDate = new DateTimeOffset(DateTime.Now),
                            TicketId = ticket.Id,
                            UserId = User.Identity.GetUserId(),
                            Property = "The title has been changed to",
                            OldValue = oldTicket.Title,
                            NewValue = ticket.Title
                        };
                        db.Histories.Add(history);

                        await Notify(ticket.Id, history);
                    }

                    if (oldTicket.Body != ticket.Body)
                    {
                        var history = new History
                        {
                            ChangeDate = new DateTimeOffset(DateTime.Now),
                            TicketId = ticket.Id,
                            UserId = User.Identity.GetUserId(),
                            Property = "The description has been changed to",
                            OldValue = oldTicket.Body,
                            NewValue = ticket.Body
                        };
                        db.Histories.Add(history);

                        await Notify(ticket.Id, history);
                    }

                    if (oldTicket.TypeId != ticket.TypeId)
                    {
                        var history = new History
                        {
                            ChangeDate = new DateTimeOffset(DateTime.Now),
                            TicketId = ticket.Id,
                            UserId = User.Identity.GetUserId(),
                            Property = "The ticket type has been changed to",
                            OldValue = oldTicket.Type.Name,
                            NewValue = db.Types.Find(ticket.TypeId).Name
                        };
                        db.Histories.Add(history);

                        await Notify(ticket.Id, history);
                    }

                    if (oldTicket.PriorityId != ticket.PriorityId)
                    {
                        var history = new History
                        {
                            ChangeDate = new DateTimeOffset(DateTime.Now),
                            TicketId = ticket.Id,
                            UserId = User.Identity.GetUserId(),
                            Property = "The priority has been set to",
                            OldValue = oldTicket.Priority.Name,
                            NewValue = db.Priority.Find(ticket.PriorityId).Name
                        };
                        db.Histories.Add(history);

                        await Notify(ticket.Id, history);
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

                ticket.Updated = DateTimeOffset.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(ticket);
        }

        // POST: Ticket/Notifications
        public async Task Notify(int ticketId, History history)
        {
            var db = new ApplicationDbContext();
            var ticket = db.Tickets.Find(ticketId);
            //var ticket = db.Tickets.Include("Project").FirstOrDefault(t => t.Id == ticketId);
            var displayName = db.Users.Find(history.UserId).DisplayName;
            var emailService = new PersonalEmail();

            // If the ticket is already assigned to a user and the user is not being changed
            if (ticket.AssignedToUserId != null && history.Property != "assigned user")
            {
                await emailService.SendAsync(new MailMessage(ConfigurationManager.AppSettings["username"], ticket.AssignedToUser.UserName)
                {
                    Subject = "A ticket that you are assigned to has been changed.",
                    //Body = ticket.AssignedToUser.FirstName + ", </p>" + displayName + " has changed the " + history.Property + " of ticket <i>" + ticket.Title + "</i> from " + history.OldValue + " to <i>" + history.NewValue + "</i>.",
                    Body = ticket.AssignedToUser.FirstName + ", </p>" + displayName + " has made a change to the ticket <i>" + ticket.Title + ".</i></p>" + history.Property + "<i> " + history.NewValue + ".<i>",
                    IsBodyHtml = true
                });
            }

            // If the ticket is being assigned for the first time or being assigned to a different user
            if (history.Property == "assigned user")
            {
                await emailService.SendAsync(new MailMessage(ConfigurationManager.AppSettings["username"], ticket.AssignedToUser.UserName)
                {
                    Subject = "You have been assigned a new ticket.",
                    Body = ticket.AssignedToUser.FirstName + ", </p>" + displayName + " has assigned you to work on a new ticket, <i>" + ticket.Title + "</i>.",
                    //Body = User.Identity.GetUserName() + ", </p><b>" + displayName + "</b> has changed the <b>" + ticket.Title + "</b> from <mark>" + history.OldValue + "</mark> to <mark>" + history.NewValue + "</mark>.",
                    IsBodyHtml = true
                });
            }
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
