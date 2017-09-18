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
    /// This controller controls the Product Methods throughout the app.
    /// </summary>
    public class ProductsController : Controller
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
        public ProductsController(StockzillaRespository repository, UserManager<StockzillaUser> userManager, SignInManager<StockzillaUser> signInManager)
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
        /// Returns an empty Add Product View.
        /// </summary>
        /// <returns>Add Product View</returns>
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var user = await GetCurrentUserAsync();
            var siteId = user.SiteID;

            //Loads the dropdown data
            ViewBag.CategoryId = new SelectList(_repository.GetAllCategories(siteId), "CategoryId", "Name");
            ViewBag.UOMId = new SelectList(_repository.GetAllUOMs(siteId), "UOMId", "Name");
            ViewBag.LocationId = new SelectList(_repository.GetAllLocations(siteId), "LocationId", "Name");

            Product model = new Product();

            return View(model);
        }

        /// <summary>
        /// Checks if the Product Model posted back is valid.  Method then get the User and Site Id and creates a new Product for that particular site.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Add Model View</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Product model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var siteId = user.SiteID;
                model.SiteId = siteId;
                model.CreatedBy = user.Name;
                model.CreatedDate = DateTime.Now;

                _repository.AddProduct(model);
                ModelState.Clear();

                return RedirectToAction("list", new { id = "success" });
            }

            return View(model);
        }

        #endregion

        #region "Edit"

        /// <summary>
        /// Checks the Product ID and Site Id and loads the data into a Product Model and passes the Model into the Edit Product View for edit.
        /// </summary>
        /// <returns>Edit Product View</returns>
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            if (RouteData.Values.ContainsKey("Id"))
            {
                var user = await GetCurrentUserAsync();
                var siteId = user.SiteID;

                string key;
                key = RouteData.Values["Id"].ToString();

                Product model = new Product();
                model = _repository.GetProduct(key, siteId);

                //Loads the dropdown data
                ViewBag.CategoryId = new SelectList(_repository.GetAllCategories(siteId), "CategoryId", "Name", model.CategoryId);
                ViewBag.UOMId = new SelectList(_repository.GetAllUOMs(siteId), "UOMId", "Name", model.UOMId);
                ViewBag.LocationId = new SelectList(_repository.GetAllLocations(siteId), "LocationId", "Name", model.LocationId);

                return View(model);
            }
            else
            {
                return RedirectToAction("list");
            }
        }

        /// <summary>
        /// Checks if the Product Model posted back is valid and saves the data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Product View</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(model);
                return RedirectToAction("list", new { id = "success" });
            }

            return View(model);
        }

        #endregion

        #region "List"

        /// <summary>
        /// Loads the Product data grid.
        /// </summary>
        /// <returns>Product List View</returns>
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

            var data = _repository.GetAllProducts(siteId);
            return View(data);
        }


        #endregion

    }
}
