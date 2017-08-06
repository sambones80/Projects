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
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Created,Updated")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTimeOffset.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Created,Updated")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Updated = DateTimeOffset.Now;
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // GET: Projects/Assign/5
        [Authorize(Roles = "Superuser, Admin, Project Manager, Guest")]
        public ActionResult Assign(int id)
        {
            var project = db.Projects.Find(id);
            AssignHelper helper = new AssignHelper(db);
            var model = new AssignUsersViewModel();

            model.Project = project;
            model.SelectedUsers = helper.ListAssignedUsers(id).Select(u => u.Id).ToArray();
            model.Users = new MultiSelectList(db.Users.Where(u => (u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)")).OrderBy(u => u.FirstName), "Id", "DisplayName", model.SelectedUsers);

            return View(model);
        }

        // POST: Projects/Assign/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Superuser, Admin, Project Manager, Guest")]
        public ActionResult Assign(AssignUsersViewModel model)
        {
            var project = db.Projects.Find(model.Project.Id);
            AssignHelper helper = new AssignHelper(db);

            foreach (var user in db.Users.Select(r => r.Id).ToList())
            {
                helper.RemoveProjectFromUser(project.Id, user);
            }
            if (model.SelectedUsers != null)
            {
                foreach (var user in model.SelectedUsers)
                {
                    helper.AddProjectToUser(project.Id, user);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
