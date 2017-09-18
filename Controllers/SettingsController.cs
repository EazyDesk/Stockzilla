using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stockzilla.Models;
using Stockzilla.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Controllers
{
    public class SettingsController : Controller
    {

        #region "Constuctors/Variables/Misc"

        StockzillaRespository _repository;
        private readonly UserManager<StockzillaUser> _userManager;
        private readonly SignInManager<StockzillaUser> _signInManager;

        /// <summary>
        /// Constructor to inject User Manager, SignIn Manager & EF Repository Service.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public SettingsController(StockzillaRespository repository, UserManager<StockzillaUser> userManager, SignInManager<StockzillaUser> signInManager)
        {
            _repository = repository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets the current user logged in.
        /// </summary>
        /// <returns>User</returns>
        private Task<StockzillaUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        #endregion

        #region "Edit"

        /// <summary>
        /// Checks the Site Id and loads the settings data into the Settings View.
        /// </summary>
        /// <returns>Edit Category View</returns>
        [Authorize]
        public async Task<IActionResult> Edit()
        {   
                var user = await GetCurrentUserAsync();
                var siteId = user.SiteID;

                Settings model = _repository.GetSettings(siteId);

                return View(model);
        }

        #endregion

    }
}
