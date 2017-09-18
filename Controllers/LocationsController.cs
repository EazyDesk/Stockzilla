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
    /// <summary>
    /// This controller controls the Location Methods throughout the app.
    /// </summary>
    public class LocationsController : Controller
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
        public LocationsController(StockzillaRespository repository, UserManager<StockzillaUser> userManager, SignInManager<StockzillaUser> signInManager)
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

        #region "Add"

        /// <summary>
        /// Returns an empty Add Location View.
        /// </summary>
        /// <returns>Add Location View</returns>
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Checks if the Location Model posted back is valid.  Method then get the User and Site Id and creates a new Location for that particular site.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Add Location View</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Location model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var siteId = user.SiteID;
                model.SiteId = siteId;
                model.CreatedBy = user.Name;
                model.CreatedDate = DateTime.Now;

                _repository.AddLocation(model);
                ModelState.Clear();
                return RedirectToAction("list", new { id = "success" });
            }

            return View();
        }

        #endregion

        #region "Edit"

        /// <summary>
        /// Checks the Location ID and Site Id and loads the data into a Category Model and passes the Model into the Edit Location View for edit.
        /// </summary>
        /// <returns>Edit Category View</returns>
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            if (RouteData.Values.ContainsKey("Id"))
            {
                var user = await GetCurrentUserAsync();
                var siteId = user.SiteID;

                string key;
                key = RouteData.Values["Id"].ToString();
                Location model = _repository.GetLocation(key, siteId);

                return View(model);
            }
            else
            {
                return RedirectToAction("list");
            }
        }

        /// <summary>
        /// Checks if the Location Model posted back is valid and saves the data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Location View</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Location model)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveLocation(model);
                return RedirectToAction("list", new { id = "success" });
            }

            return View(model);
        }

        #endregion

        #region "List"

        /// <summary>
        /// Loads the Location data grid.
        /// </summary>
        /// <returns>Location List View</returns>
        [Authorize]
        public async Task<IActionResult> List()
        {
            //SuccessMsg is used to hide or display the data saved alert notification.
            @ViewBag.SuccessMsg = "hidden";

            if (RouteData.Values.ContainsKey("Id"))
            {
                string key;
                key = RouteData.Values["Id"].ToString();
                if (key == "success")
                {
                    @ViewBag.SuccessMsg = "";
                }
            }

            var user = await GetCurrentUserAsync();
            var siteId = user.SiteID;

            var data = _repository.GetAllLocations(siteId);
            return View(data);
        }

        #endregion
      
    }
}
