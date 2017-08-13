﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class DashboardViewModel
    {
        public List<Project> RelevantProjects { get; set; }
        public List<Project> IrrelevantProjects { get; set; }
        public List<Ticket> RelevantTickets { get; set; }
        public List<Ticket> IrrelevantTickets { get; set; }
    }
}