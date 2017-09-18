using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stockzilla.Models;
using Stockzilla.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Controllers
{
    /// <summary>
    /// This controller controls the authentication methods throughout the app.
    /// </summary>
    public class AuthController : Controller
    {

        #region "Constuctors/Variables/Misc"

        StockzillaRespository _repository;
        private readonly UserManager<StockzillaUser> _userManager;
        private readonly SignInManager<StockzillaUser> _signInManager;

        /// <summary>
        /// Constructor to inject User Manager & SignIn Manager Service.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AuthController(StockzillaRespository repository ,UserManager<StockzillaUser> userManager,
            SignInManager<StockzillaUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void CreateSettingsFile(Guid SiteId)
        {
            Settings model = new Settings { SiteId = SiteId, GRNNo = 0 };
            _repository.AddSettings(model);
        }

        #endregion

        #region "Login"

        /// <summary>
        /// Checks if the user is authenticated.  If the user is authenticated it will direct them to the Products List otherwise it will return a blank Login View.
        /// </summary>
        /// <returns>Login View</returns>
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("List", "Products");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        ///  Checks if the Login Model posted back is valid and attempts to authenticate the user.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Login View</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Products");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        #endregion

        #region "Register"

        /// <summary>
        /// Loads the initial Register View to allow the user to register.  If the user is already registered and authenticated it will redirect them to the Products List.
        /// </summary>
        /// <returns>Register View</returns>
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("List", "Products");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Checks if the Register Model posted back is valid and create the user record.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Register View</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                Guid SiteId = Guid.NewGuid();
                var user = new StockzillaUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.Telephone, Company = model.Company, Name = model.Name, SiteID = SiteId };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    CreateSettingsFile(SiteId);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("List", "Products");
                }
            }

            return View(model);
        }

        #endregion

        #region "Logout"

        /// <summary>
        /// Signs the user out and redirects them to the Login View.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        #endregion

    }
}
