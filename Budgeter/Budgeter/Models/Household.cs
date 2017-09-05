using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Household
    {
        public Household()
        {
            this.Budgets = new HashSet<Budget>();
            this.Users = new HashSet<ApplicationUser>();
            this.BankAccounts = new HashSet<BankAccount>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedById { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        //public double total { get; set; }
    }
}