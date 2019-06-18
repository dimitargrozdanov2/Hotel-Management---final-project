using HotelManagement.DataModels;
using HotelManagement.Services.Exceptions;
using HotelManagement.ViewModels;
using HotelManagement.Web.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HotelManagement.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this._signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    this._logger.LogInformation("User logged in.");
                    return this.RedirectToLocal(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await this._userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");
                    var userViewModel = new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName
                    };

                    return this.Json(userViewModel);
                }

                var test = result.Errors;

                return this.BadRequest(result.Errors);
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            this.ViewData["ReturnUrl"] = returnUrl;
            if (this.ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await this._userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    this._logger.LogInformation("User created a new account with password.");

                    var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);

                    //After Registering a user, I don't want to sign in with him

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    this._logger.LogInformation("User created a new account with password.");

                    return this.RedirectToLocal(returnUrl);
                }
                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var user = await this._userManager.FindByIdAsync(id);

                    await this._userManager.DeleteAsync(user);

                    this._logger.LogInformation("User deleted.");

                    var userViewModel = new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.UserName
                    };

                    return this.Json(userViewModel);
                }
                catch (EntityInvalidException ex)
                {
                    this.ModelState.AddModelError("Error", ex.Message);
                    return this.BadRequest();
                }
            }
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            this._logger.LogInformation("User logged out.");
            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion Helpers
    }
}