using System.Web.Mvc;

namespace Bug_Tracker.Models
{
	public class AssignUsersViewModel
	{
		public Project Project { get; set; }

		public MultiSelectList Users { get; set; }
		public string[] SelectedUsers { get; set; }
	}
}