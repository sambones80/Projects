using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budgeter.Models;
using Microsoft.AspNet.Identity;

namespace Budgeter.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            HouseholdViewModel model = new HouseholdViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }

            household.Total = 0;

            foreach (var bankAccount in household.BankAccounts)
            {
                if (bankAccount.Deleted != true)
                {
                    if (bankAccount.Balance <= 0)
                    {
                        ViewBag.OverdraftError = "You are in danger of overdrawing your account!";
                    }

                    var balance = bankAccount.Balance;
                    household.Total += balance;
                    //total = total + balance;
                }
            }

            household.TotalBudget = 0;

            foreach (var budget in household.Budgets)
            {
                foreach (var item in budget.Items)
                {
                    var amount = item.Amount;
                    household.TotalBudget += amount;
                }
            }

            //foreach (var account in household.BankAccounts)
            //{
            //    foreach (var transaction in account.Transactions.Where(c => c.Catagory.Name = ))
            //    {
                    
            //    }
            //}

            db.SaveChanges();
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name, CreatedById, Deleted")] Household household)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                household.CreatedById = userId;
                db.Households.Add(household);

                HouseholdUsersHelper helper = new HouseholdUsersHelper(db);
                helper.AddUserToHousehold(household.Id, userId);

                Budget budget = new Budget
                {
                    HouseholdId = household.Id,
                    household = db.Households.Find(household.Id),
                    Name = household.Name + "'s Budget",
                    Amount = 0
                };
                db.Budgets.Add(budget);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(household);
        }

        // GET: Households/Assign/5
        public ActionResult Assign(int id)
        {
            var household = db.Households.Find(id);
            HouseholdUsersHelper helper = new HouseholdUsersHelper(db);
            var model = new AssignUsersViewModel();

            model.Household = household;
            model.SelectedUsers = helper.ListAssignedUsers(id).ToArray(); 
            model.Users = new MultiSelectList(model.SelectedUsers.Where(u => (u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)")).OrderBy(u => u.FirstName), "Id", "DisplayName", model.SelectedUsers);
            //model.Users = new MultiSelectList(db.Users.Where(u => (u.DisplayName != "N/A" && u.DisplayName != "(Remove Assigned User)")).OrderBy(u => u.FirstName), "Id", "DisplayName", model.SelectedUsers);

            return View(model);
        }

        // POST: Households/Assign/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Assign(AssignUsersViewModel model)
        {
            var household = db.Households.Find(model.Household.Id);
            HouseholdUsersHelper helper = new HouseholdUsersHelper(db);

            //foreach (var user in db.Users.Select(r => r.Id).ToList())
            //{
            //    helper.RemoveUserFromHousehold(household.Id, user);
            //}
            foreach (var user in db.Users.Select(r => r.Id).ToList())
            {
                if (model.SelectedUsers != null)
                {
                    foreach (var item in model.SelectedUsers)
                    {
                        helper.RemoveUserFromHousehold(household.Id, item.Id);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedById,Deleted")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = household.Id  });
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            household.Deleted = true;
            db.Entry(household).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // GET: Households/Leave/5
        public ActionResult Leave(int? id)
        {
            ViewBag.HouseholdId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Leave/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave(int id)
        {
            var user = User.Identity.GetUserId();
            HouseholdUsersHelper helper = new HouseholdUsersHelper(db);
            helper.RemoveUserFromHousehold(id, user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
