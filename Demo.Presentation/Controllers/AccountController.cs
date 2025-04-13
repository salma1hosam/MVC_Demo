using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        
        [HttpGet] 
        public IActionResult Register() => View(); 

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid) return View(registerViewModel);

            var user = new ApplicationUser()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };

            var result = _userManager.CreateAsync(user , registerViewModel.Password).Result;

            if (result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(registerViewModel);
            }
        }
        #endregion

        #region Sign In

        [HttpGet]
        public IActionResult LogIn() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                if (user is not null)
                {
                    var result = _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    else
                        ModelState.AddModelError(string.Empty, "Invalid Login");

                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login");
            return View(loginViewModel);
        }
        #endregion

        [HttpGet]
        public ActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
    }
}
