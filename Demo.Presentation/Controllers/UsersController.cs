using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Presentation.Controllers
{
	public class UsersController(UserManager<ApplicationUser> _userManager) : Controller
	{
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
	}
}
