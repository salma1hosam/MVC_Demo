using Demo.Presentation.ViewModels.AccountViewModels;
using Demo.Presentation.ViewModels.RoleViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.Presentation.Controllers
{
	public class RolesController(RoleManager<IdentityRole> _roleManager) : Controller
	{
		public IActionResult Index()
		{
			var roles = _roleManager.Roles.ToList();
			var rolesViewModel = new List<RoleViewModel>();
			foreach (var role in roles)
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

		#region Create Role
		[HttpGet]
		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(CreateRoleViewModel createRoleViewModel)
		{
			if (!ModelState.IsValid) return View(createRoleViewModel);
			var role = new IdentityRole()
			{
				Name = createRoleViewModel.RoleName
			};
			var result = _roleManager.CreateAsync(role).Result;
			if (result.Succeeded)
				return RedirectToAction(nameof(Index));
			else
			{
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
				return View(createRoleViewModel);
			}
		}
		#endregion
	}
}
