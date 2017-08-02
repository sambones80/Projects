using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public string AuthorUserId { get; set; }
        public string FileUrl { get; set; }
        public string FileDisplayName { get; set; }

        public virtual ApplicationUser AuthorUser { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}