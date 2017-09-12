using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int[] MyCatagories { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double[] CatagoryTotals { get; set; }
        public int CatagoryCount { get; set; }
    }
}