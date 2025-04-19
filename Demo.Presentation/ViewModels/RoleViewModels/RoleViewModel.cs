using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.RoleViewModels
{
	public class RoleViewModel
	{
		public string Id { get; set; }

		[Display(Name = "Role Name")]
		public string RoleName { get; set; }
	}
}
