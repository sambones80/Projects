using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTimeOffset Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTimeOffset? Updated { get; set; }
        public int TicketId { get; set; }

        public string AuthorUserId { get; set; }

        public virtual ApplicationUser AuthorUser { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}