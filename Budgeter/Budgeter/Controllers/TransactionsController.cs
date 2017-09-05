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
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.Catagory).Include(t => t.Type);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int householdId, int? transactionId)
        {
            ViewBag.HouseholdId = householdId;
            if (transactionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Deposit
        public ActionResult Deposit(int householdId, int accountId)
        {
            //ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name");
            ViewBag.HouseholdId = householdId;
            ViewBag.BankAccountId = accountId;
            //ViewBag.CatagoryId = 1;
            //ViewBag.TypeId = 1;
            ViewBag.EnteredbyId = User.Identity.GetUserId();
            return View();
        }

        // POST: Transactions/Deposit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind(Include = "Id,BankAccountId,Payee,Description,Created,Amount,Deleted,TypeId,CatagoryId,EnteredById")] Transaction transaction, int householdId, int accountId)
        {
            if (ModelState.IsValid)
            {
                transaction.BankAccountId = accountId;
                var bankAccount = db.BankAccounts.Find(transaction.BankAccountId);
                if (bankAccount != null)
                {
                    transaction.BankAccount = bankAccount;
                    transaction.EnteredBy = ViewBag.EnteredbyId;
                    transaction.CatagoryId = 1;
                    transaction.TypeId = 1;
                    transaction.Created = DateTimeOffset.Now;
                    transaction.Deleted = false;
                    transaction.Payee = "n/a";
                    transaction.BankAccount.Balance += transaction.Amount;
                }
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Details", "Households", new { id = householdId });
            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Name", transaction.CatagoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create(int? householdId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Household household = db.Households.Find(householdId);
            var ownAccounts = context.BankAccounts.Where(b => b.Household.Id == household.Id);

            ViewBag.HouseholdId = householdId;
            //ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name");
            ViewBag.BankAccountId = new SelectList(ownAccounts, "Id", "Name");
            //ViewBag.BankAccountId = id;
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            ViewBag.EnteredbyId = User.Identity.GetUserId();
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankAccountId,Payee,Description,Created,Amount,Deleted,TypeId,CatagoryId,EnteredById")] Transaction transaction, int householdId)
        {
            if (ModelState.IsValid)
            {
                var bankAccount = db.BankAccounts.Find(transaction.BankAccountId);
                if (bankAccount != null)
                { 
                    transaction.BankAccount = bankAccount;
                    transaction.EnteredBy = ViewBag.EnteredbyId;
                    transaction.TypeId = 2;
                    transaction.Created = DateTimeOffset.Now;
                    transaction.Deleted = false;
                    transaction.BankAccount.Balance -= transaction.Amount;
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Households", new { id = householdId });
                }
            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Name", transaction.CatagoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        // ///////////// Need to adjust the bank account balance(s) if amount is changed ///////////////////////
        public ActionResult Edit(int householdId, int? transactionId)
        {
            if (transactionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = householdId;
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Name", transaction.CatagoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,Payee,Description,Created,Amount,Deleted,TypeId,CatagoryId,EnteredById")] Transaction transaction, int householdId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Households", new { id = householdId });
            }
            ViewBag.HouseholdId = householdId;
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CatagoryId = new SelectList(db.Catagories, "Id", "Name", transaction.CatagoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
