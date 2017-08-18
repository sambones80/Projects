using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.Comments = new HashSet<Comment>();
            this.Attachments = new HashSet<Attachment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTimeOffset Created { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy h:mm tt}")]
        public DateTimeOffset? Updated { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }

        public string AuthorUserId { get; set; }
        public string AssignedToUserId { get; set; }

        public virtual Bug_Tracker.Models.Type Type { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Status Status { get; set; }

        public virtual ApplicationUser AuthorUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }
        public virtual Project Project { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        //public virtual ICollection<Notification> Notifications { get; set; }
    }
}