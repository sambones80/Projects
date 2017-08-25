using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string Payee { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public double Amount { get; set; }
        public bool Deleted { get; set; }
        // Type: Deposit or Expense
        public int TypeId { get; set; }
        public int CatagoryId { get; set; }
        public int EnteredById { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual Budgeter.Models.Type Type { get; set; }
        public virtual Catagory Catagory { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
    }
}