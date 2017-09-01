﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Invite
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public DateTimeOffset Created { get; set; }
        public string SentById { get; set; }
        public string ToEmail { get; set; }
        public string Secret { get; set; }
        public virtual Household Household { get; set; }
        public virtual ApplicationUser SentBy { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}