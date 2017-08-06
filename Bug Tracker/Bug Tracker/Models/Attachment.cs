using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTimeOffset Created { get; set; }
        public string AuthorUserId { get; set; }
        public string FileUrl { get; set; }
        public string FileDisplayName { get; set; }

        public virtual ApplicationUser AuthorUser { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}