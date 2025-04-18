using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.Utilities;
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
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (user is not null)
            {
                var flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                if (flag)
                {
                    var result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is Not Allowed");
                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is Locked Out");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
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

        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(forgetPasswordViewModel.Email).Result;
                if (user is not null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result; //Generated Token valid for one time

                    //BaseUrl/Account/ResetPassword?email=salma@gmail.com&Token=
                    var resetPasswordLink = Url.Action("ResetPassword",
                                                      "Account",
                                                      new { email = forgetPasswordViewModel.Email , token },
                                                      Request.Scheme);  //Schema => gets the schema(protocol , host , port)(BaseUrl) of the request to the SendResetPasswordLink Action
                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset Password",
                        Body = resetPasswordLink
                    };

                    //Send Email
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), forgetPasswordViewModel);
        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
		public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
		{
			if (!ModelState.IsValid) return View(resetPasswordViewModel);

			string email = TempData["email"] as string ?? string.Empty;
			string token = TempData["token"] as string ?? string.Empty;

			var user = _userManager.FindByEmailAsync(email).Result;

			if (user is not null)
			{
				var result = _userManager.ResetPasswordAsync(user , token , resetPasswordViewModel.Password).Result;
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				else
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(nameof(ResetPassword), resetPasswordViewModel);
		}
		#endregion
	}
}
