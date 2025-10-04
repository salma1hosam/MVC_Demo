using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.UserViewModels
{
	public class UserViewModel
	{
		public string Id { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		public string Role { get; set; }
	}
}
