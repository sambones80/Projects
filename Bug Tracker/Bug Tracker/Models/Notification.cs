using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        [Required]
        public string Message { get; set; }
        public string Type { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTimeOffset Created { get; set; }

        public string NotifyUserId { get; set; }

        public virtual ApplicationUser NotifyUser { get; set; }
    }
}