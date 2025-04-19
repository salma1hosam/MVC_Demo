using Demo.BusinessLogic.DataTransferObjects.Employee;
using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.EmployeeViewModels;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Presentation.Controllers
{
	public class UsersController(UserManager<ApplicationUser> _userManager , ILogger<UsersController> _logger , IWebHostEnvironment _environment) : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			var users = _userManager.Users.ToList();			
			List<UserViewModel> usersViewModel = [];
			foreach (var user in users)
			{
				var viewModel = new UserViewModel
				{
					Id = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber
					// Role
				};

				usersViewModel.Add(viewModel);
			}
			return View(usersViewModel);
		}

		#region Edit User

		[HttpGet]
		public IActionResult Edit(string? id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			var user = _userManager.FindByIdAsync(id).Result;
			if (user is null) return NotFound();

			var userEditViewModel = new UserEditViewModel()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber
			};
			return View(userEditViewModel);
		}

		[HttpPost]
		public IActionResult Edit([FromRoute] string? id, UserEditViewModel userEditViewModel)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			if (!ModelState.IsValid) return View(userEditViewModel);
			try
			{
				var user = _userManager.FindByIdAsync(id).Result;
				user.FirstName = userEditViewModel.FirstName;
				user.LastName = userEditViewModel.LastName;
				user.PhoneNumber = userEditViewModel.PhoneNumber;

				var result = _userManager.UpdateAsync(user).Result;
				if (result.Succeeded)
					return RedirectToAction(nameof(Index));
				else
				{
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
					return View(userEditViewModel);
				}
			}
			catch (Exception ex)
			{
				if (_environment.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
					return View(userEditViewModel);
				}
				else
				{
					_logger.LogError(ex.Message);
					return View("ErrorView", ex);
				}
			}
		}
		#endregion

		[HttpGet]
		public IActionResult Details(string? id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			var user = _userManager.FindByIdAsync(id).Result;
			if (user is null) return NotFound();
			var userViewModel = new UserDetailsViewModel()
			{
				Id = id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Email = user.Email
			};
			return View(userViewModel);
		}

		#region Delete User

		[HttpGet]
		public IActionResult Delete(string? id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			var user = _userManager.FindByIdAsync(id).Result;
			if (user is null) return NotFound();
			var userViewModel = new UserDetailsViewModel()
			{
				Id = id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Email = user.Email
			};
			return View(userViewModel);
		}

		[HttpPost]
		public IActionResult DeleteUser(string id)
		{
			if (id.IsNullOrEmpty()) return BadRequest();
			try
			{
				var user = _userManager.FindByIdAsync(id).Result;
				if (user is null) return NotFound();
				var result = _userManager.DeleteAsync(user).Result;
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
