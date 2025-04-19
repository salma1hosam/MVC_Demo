using Demo.Presentation.ViewModels.RoleViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
	public class RolesController(RoleManager<IdentityRole> _roleManager) : Controller
	{
		public IActionResult Index()
		{
			var roles = _roleManager.Roles.ToList();
			var rolesViewModel = new List<RoleViewModel>();
			foreach(var role in roles)
			{
				var viewModel = new RoleViewModel()
				{
					Id = role.Id,
					RoleName = role.Name
				};
				rolesViewModel.Add(viewModel);
			}
			return View(rolesViewModel);
		}
	}
}
