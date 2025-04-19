using Demo.Presentation.ViewModels.AccountViewModels;
using Demo.Presentation.ViewModels.RoleViewModels;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.Presentation.Controllers
{
	public class RolesController(RoleManager<IdentityRole> _roleManager, ILogger<RolesController> _logger, IWebHostEnvironment _environment) : Controller
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

		#region Edit Role

		[HttpGet]
		public IActionResult Edit(string? id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			var role = _roleManager.FindByIdAsync(id).Result;
			if (role is null) return NotFound();
			var roleViewModel = new RoleViewModel()
			{
				Id = role.Id,
				RoleName = role.Name
			};
			return View(roleViewModel);
		}

		[HttpPost]
		public IActionResult Edit(RoleViewModel roleViewModel)
		{
			if (roleViewModel.Id.IsNullOrEmpty()) return BadRequest();
			if (!ModelState.IsValid) return View(roleViewModel);
			try
			{
				var role = _roleManager.FindByIdAsync(roleViewModel.Id).Result;
				role.Name = roleViewModel.RoleName;

				var result = _roleManager.UpdateAsync(role).Result;
				if (result.Succeeded)
					return RedirectToAction(nameof(Index));
				else
				{
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
					return View(roleViewModel);
				}
			}
			catch (Exception ex)
			{
				if (_environment.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return View(roleViewModel);
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("ErrorView", ex);
				}
			}
		}
		#endregion

		public IActionResult Details(string? id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			var role = _roleManager.FindByIdAsync(id).Result;
			if (role is null) return NotFound();
			var roleViewModel = new RoleViewModel()
			{
				Id = id,
				RoleName = role.Name
			};
			return View(roleViewModel);
		}

		#region Delete Role

		[HttpGet]
		public IActionResult Delete(string? id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			var role = _roleManager.FindByIdAsync(id).Result;
			if (role is null) return NotFound();
			var roleViewModel = new RoleViewModel()
			{
				Id = id,
				RoleName = role.Name
			};
			return View(roleViewModel);
		}

		[HttpPost]
		public IActionResult Remove(string id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			try
			{
				var role = _roleManager.FindByIdAsync(id).Result;
				if (role is null) return NotFound();
				var result = _roleManager.DeleteAsync(role).Result;
				if (result.Succeeded)
					return RedirectToAction(nameof(Index));
				else
				{
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
					return RedirectToAction(nameof(Delete), new { Id = id });
				}
			}
			catch (Exception ex)
			{
				if (_environment.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return RedirectToAction(nameof(Delete), new { Id = id });
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("ErrorView", ex);
				}
			}
		}
		#endregion
	}
}
