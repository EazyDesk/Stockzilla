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
    public class StockReceiptsController : Controller
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
        public StockReceiptsController(StockzillaRespository repository, UserManager<StockzillaUser> userManager, SignInManager<StockzillaUser> signInManager)
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

        /// <summary>
        /// Called by JQuery to load the Default Product Location.  This is triggered when the Product dropwon on the Add View changes.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json String</returns>
        [HttpGet]
        public JsonResult UpdatePreferredLocation(string id)
        {
            return Json(_repository.GetProductDefaultLocation(id));
        }

        #endregion

        #region "Add"

        /// <summary>
        /// Returns an empty Add Stock Receipt View.
        /// </summary>
        /// <returns>Add Stock Receipt View</returns>
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var user = await GetCurrentUserAsync();
            var siteId = user.SiteID;

            //Loads the dropdown data
            ViewBag.ProductId = new SelectList(_repository.GetAllProducts(siteId), "ProductId", "ProductCode");
            ViewBag.LocationId = new SelectList(_repository.GetAllLocations(siteId), "LocationId", "Name");

            return View();
        }

        /// <summary>
        /// Checks if the Add Stock Receipt Model posted back is valid.  Method then get the User and Site Id and creates a new Stock Receipt for that particular site.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Add Model View</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(StockReceipt model)
        {
            var user = await GetCurrentUserAsync();
            var siteId = user.SiteID;

            if (ModelState.IsValid)
            {
                model.SiteId = siteId;
                model.ReceivedBy = user.Name;
                model.DateReceived = DateTime.Now;

                _repository.AddStockReceipt(model);
                CreateStockRecord(model);
                ModelState.Clear();

                return RedirectToAction("list", new { id = "success" });
            }

            //Loads the dropdown data
            ViewBag.ProductId = new SelectList(_repository.GetAllProducts(siteId), "ProductId", "ProductCode");
            ViewBag.LocationId = new SelectList(_repository.GetAllLocations(siteId), "LocationId", "Name");

            return View(model);
        }

        /// <summary>
        /// Creates the stock record on creation of the stock receipt.
        /// </summary>
        /// <param name="stockReceiptModel"></param>
        private void CreateStockRecord(StockReceipt stockReceiptModel)
        {
            string GRNNo = _repository.GetGRN(stockReceiptModel.SiteId);
            //StockItem stockModel = new StockItem { GRNNo = GRNNo, ProductId = stockReceiptModel.ProductId, Product = stockReceiptModel.Product, LocationId= stockReceiptModel.LocationId, Location = stockReceiptModel.Location, SerialNo = stockReceiptModel.SerialNo, BatchNo = stockReceiptModel.BatchNo, DateReceived = stockReceiptModel.DateReceived, ReceivedBy = stockReceiptModel.ReceivedBy, InitialQty = stockReceiptModel.Qty, QtyAvailable = stockReceiptModel.Qty, CostPerUOM = stockReceiptModel.CostPerUOM, TotalCost = stockReceiptModel.TotalCost, Notes=stockReceiptModel.Notes, SiteId = stockReceiptModel.SiteId};
            //_repository.AddStockItem(stockModel);
        }

        #endregion

        #region "List"

        /// <summary>
        /// Loads the Stock Receipt data grid.
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

            var data = _repository.GetAllStockReceipts(siteId);
            return View(data);
        }



        #endregion    

    }
}
