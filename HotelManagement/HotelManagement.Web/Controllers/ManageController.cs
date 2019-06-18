using HotelManagement.DataModels;
using HotelManagement.Web.Models.ManageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await this._userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this._userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            var hasPassword = await this._userManager.HasPasswordAsync(user);

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            var changePasswordResult = await this._userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                this.AddErrors(changePasswordResult);
                return this.View(model);
            }

            await this._signInManager.SignInAsync(user, isPersistent: false);
            this._logger.LogInformation("User changed their password successfully.");
            this.StatusMessage = "Your password has been changed.";

            return this.RedirectToAction(nameof(ChangePassword));
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion Helpers
    }
}