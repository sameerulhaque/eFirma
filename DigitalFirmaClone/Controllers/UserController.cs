using DigitalFirmaClone.Bsl.Manager;
using DigitalFirmaClone.Bsl.Model;
using DigitalFirmaClone.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserManager IUserManager;
        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserManager IUserManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.IUserManager = IUserManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Signature/Index");
            }
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = "/Signature/Index",
                ExternalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync()
            };
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Email = model.Email.Trim();
            var appUser = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (appUser == null)
            {
                ModelState.AddModelError("FileNameValidation", "Account does not exist.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Redirect("/Signature/Index");
            }

            ModelState.AddModelError("FileNameValidation", "Password does not match.");
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "User",
                                    new { ReturnUrl = returnUrl });

            var properties =
                _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync()
            };

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login", loginViewModel);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var Name = info.Principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                if (user == null)
                {
                    user = new AppUser
                    {
                        Name = Name,
                        FullName = Name,
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                        PassWord = info.ProviderKey,
                        AuthenticationType = "GOOGLE"
                    };
                    IUserManager.CreateAsync(user);
                }

                var result = await _signInManager.PasswordSignInAsync(info.Principal.FindFirstValue(ClaimTypes.Email), info.ProviderKey, true, lockoutOnFailure: false).ConfigureAwait(false);
                if (result.Succeeded)
                    return LocalRedirect(returnUrl);
            }

            return View("Error");
        }

        public async Task<IActionResult> CreateAccount(AppUser appUser)
        {
            if (appUser == null)
            {
                var user = new AppUser
                {
                    Name = appUser.UserName,
                    FullName = appUser.UserName,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    PassWord = appUser.PassWord,
                };
                IUserManager.CreateAsync(user);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser.Email, appUser.PassWord, true, lockoutOnFailure: false).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Redirect("/Signature/Index");
            }

            return Redirect("/Signature/Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return Redirect("/");
        }
    }
}
