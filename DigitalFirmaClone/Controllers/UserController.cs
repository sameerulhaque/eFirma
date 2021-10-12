using DigitalFirmaClone.Bsl.Manager;
using DigitalFirmaClone.Bsl.Model;
using DigitalFirmaClone.Helper;
using DigitalFirmaClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
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
                model.ExternalLogins = await _signInManager.GetExternalAuthenticationSchemesAsync();
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
        //public async Task<IActionResult> Companies()
        //{
        //    APIResponse<List<CompanyData>> reservationList = new APIResponse<List<CompanyData>>();
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c29-16ef-817c-35be2d830017:b48222c13b3a9c6858f8598293f3caf5ec09a90b1f8e62c012c0042da019ea44");
        //        using (var response = await httpClient.GetAsync("http://api.digitafirma.com/v1/companies"))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            reservationList = JsonConvert.DeserializeObject<APIResponse<List<CompanyData>>>(apiResponse);
        //        }
        //    }
        //    return View(reservationList);
        //}

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
                    var CompanyData = await AddCompany(new Company() { Name = user.UserName });
                    user.Company = CompanyData.id;
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
            if (appUser != null)
            {
                var user = new AppUser
                {
                    Name = appUser.UserName,
                    FullName = appUser.UserName,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    PassWord = appUser.PassWord,
                };
                var CompanyData = await AddCompany(new Company() { Name = user.UserName });
                user.Company = CompanyData.id;
                IUserManager.CreateAsync(user);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser.Email, appUser.PassWord, true, lockoutOnFailure: false).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Redirect("/Signature/Index");
            }

            return Redirect("/Signature/Login");
        }


        public async Task<CompanyData> AddCompany(Company file)
        {

            CompanyData receivedReservation = new CompanyData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c3d-192b-817c-4aa4bbb30009:d2a9e08e2cdbf21bc9d0d118b064ecc808f4f83a1cd58495875a59dfdeb29317");

                CreateCompany createCompany = new CreateCompany();
                if (file.Image != null)
                {
                    using (var fileStream = file.Image.OpenReadStream())
                    {
                        byte[] array = new byte[fileStream.Length];
                        fileStream.Read(array, 0, array.Length);
                        createCompany.logo = array;
                    }
                }
                createCompany.name = file.Name;
                StringContent content = new StringContent(JsonConvert.SerializeObject(createCompany), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://api.digitafirma.com/v1/companies", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var APIResponse = JsonConvert.DeserializeObject<APIResponse<List<CompanyData>>>(apiResponse);
                        return APIResponse.data.LastOrDefault();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return new CompanyData();
                    }
                }
            }
            return new CompanyData();
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return Redirect("/");
        }


        //[HttpPost]
        //public async Task<IActionResult> UpdateCompany(Company file)
        //{

        //    CompanyData receivedReservation = new CompanyData();
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c29-16ef-817c-35be2d830017:b48222c13b3a9c6858f8598293f3caf5ec09a90b1f8e62c012c0042da019ea44");

        //        CreateCompany createCompany = new CreateCompany();
        //        using (var fileStream = file.Image.OpenReadStream())
        //        {
        //            byte[] array = new byte[fileStream.Length];
        //            fileStream.Read(array, 0, array.Length);
        //            createCompany.logo = array;
        //        }
        //        createCompany.name = "sameer 1";
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(createCompany), Encoding.UTF8, "application/json");

        //        var request = new HttpRequestMessage
        //        {
        //            RequestUri = new Uri("http://api.digitafirma.com/v1/companies/" + "7f000001-7c29-16ef-817c-350a5c600013"),
        //            Method = new HttpMethod("Patch"),
        //            Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"name\", \"value\": \"" + createCompany.name + "\"},{ \"op\": \"replace\", \"path\": \"logo\", \"value\": \"" + createCompany.logo + "\"}]", Encoding.UTF8, "application/json")
        //        };

        //        var response = await httpClient.SendAsync(request);
        //        int assa = 1;
        //    }
        //    return View();
        //}

    }
}
