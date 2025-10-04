using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.UserViewModels
{
	public class UserDetailsViewModel
	{
		public string Id { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string PhoneNumber { get; set; }

		[EmailAddress]
		public string Email { get; set; }
	}
}
