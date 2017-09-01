using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Item
    {
        // Budget Item (catagory)
        public int Id { get; set; }
        public int CatagoryId { get; set; }
        public int BudgetId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double Amount { get; set; }
        public bool Deleted { get; set; }
        public virtual Catagory Catagory { get; set; }
        public virtual Budget Budget { get; set; }
    }
}