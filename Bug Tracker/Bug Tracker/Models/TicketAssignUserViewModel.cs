using System.Web.Mvc;

namespace Bug_Tracker.Models
{
	public class TicketAssignUserViewModel
	{
        public Ticket Ticket { get; set; }

        public SelectList Users { get; set; }
		public string SelectedUser { get; set; }
	}
}