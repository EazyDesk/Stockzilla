using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stockzilla.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Controllers
{
    /// <summary>
    /// This controller controls the Stock Receipt Methods throughout the app.
    /// </summary>
    public class StockItemsController : Controller
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
        public StockItemsController(StockzillaRespository repository, UserManager<StockzillaUser> userManager, SignInManager<StockzillaUser> signInManager)
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

        #region "List"

        /// <summary>
        /// Loads the Stock data grid.
        /// </summary>
        /// <returns>Stock Receipt List View</returns>
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

            var data = _repository.GetAllAvailableStock(siteId);
            return View(data);
        }

        #endregion

        #region "Edit"

        /// <summary>
        /// Checks the Stock ID and Site Id and loads the data into a Stock Items Model and passes the Model into the Edit Stock View for edit.
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

                StockItem model = new StockItem();
                model = _repository.GetStockItem(key, siteId);

                //Loads the dropdown data
                ViewBag.LocationId = new SelectList(_repository.GetAllLocations(siteId), "LocationId", "Name", model.LocationId);

                return View(model);
            }
            else
            {
                return RedirectToAction("list");
            }
        }

        /// <summary>
        /// Checks if the Stock Item Model posted back is valid and saves the data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Stock Item View</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(StockItem model)
        {
            model.Product = null;
            ModelState.Clear();
            TryValidateModel(model);
            if (ModelState.IsValid)
            {
                _repository.SaveStockItem(model);
                return RedirectToAction("list", new { id = "success" });
            }

            return View(model);
        }

        #endregion

    }
}
