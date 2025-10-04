using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.UserViewModels
{
	public class UserEditViewModel
	{
		[MaxLength(50, ErrorMessage = "Max length should be 50 character")]
		[MinLength(5, ErrorMessage = "Min length should be 5 characters")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[MaxLength(50, ErrorMessage = "Max length should be 50 character")]
		[MinLength(5, ErrorMessage = "Min length should be 5 characters")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Phone Number")]
		[Phone]
		public string PhoneNumber { get; set; }
	}
}
