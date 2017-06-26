using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping_Cart_01.Models
{
    public class Item
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public string MediaUrl { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}