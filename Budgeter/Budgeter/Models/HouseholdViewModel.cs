using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class HouseholdViewModel
    {
        public Household household { get; set; }
        public List<Transaction> MyTransactions { get; set; }
        public List<BankAccount> MyBankAccounts { get; set; }
        public List<Item> MyBudgetItems { get; set; }
    }
}