using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.RoleViewModels
{
	public class RoleViewModel
	{
		public string Id { get; set; }

		[Display(Name = "Role Name")]
		[MaxLength(20, ErrorMessage = "Role Name Can Not Be More Than 20 Characters")]
		public string RoleName { get; set; }
	}
}
