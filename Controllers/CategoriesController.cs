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
    /// This controller controls the Category Methods throughout the app.
    /// </summary>
    public class CategoriesController : Controller
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
        public CategoriesController(StockzillaRespository repository, UserManager<StockzillaUser> userManager, SignInManager<StockzillaUser> signInManager)
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
        /// Returns an empty Add Category View.
        /// </summary>
        /// <returns>Add Category View</returns>
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Checks if the Category Model posted back is valid.  Method then get the User and Site Id and creates a new Category for that particular site.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Add Category View</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Category model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var siteId = user.SiteID;
                model.SiteId = siteId;
                model.CreatedBy = user.Name;
                model.CreatedDate = DateTime.Now;

                _repository.AddCategory(model);
                ModelState.Clear();
                return RedirectToAction("list", new { id = "success" });
            }

            return View();
        }

        #endregion

        #region "Edit"

        /// <summary>
        /// Checks the Category ID and Site Id and loads the data into a Category Model and passes the Model into the Edit Category View for edit.
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
                Category model = _repository.GetCategory(key, siteId);

                return View(model);
            }
            else
            {
                return RedirectToAction("list");
            }
        }

        /// <summary>
        /// Checks if the Category Model posted back is valid and saves the data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Category View</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveCategory(model);
                return RedirectToAction("list", new { id = "success" });
            }

            return View(model);
        }

        #endregion

        #region "List"

        /// <summary>
        /// Loads the Category data grid.
        /// </summary>
        /// <returns>Category List View</returns>
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

            var data = _repository.GetAllCategories(siteId);
            return View(data);
        }

        #endregion

    }
}
