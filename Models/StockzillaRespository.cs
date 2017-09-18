using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Models
{
    public class StockzillaRespository
    {

        StockzillaContext _context;

        public StockzillaRespository(StockzillaContext context)
        {
            _context = context;
        }

        #region "Get List"

        /// <summary>
        /// Gets List of Products per Site.
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of Products</returns>
        public IEnumerable<Product> GetAllProducts(Guid SiteId)
        {
            var products = (from x in _context.Products where x.SiteId.Equals(SiteId) select x)
        .Include(product => product.Category).Include(product => product.UOM).Include(product => product.Location)
        .ToList();
            return products;
        }

        /// <summary>
        /// Gets List of Categories per Site.
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of Categories</returns>
        public IEnumerable<Category> GetAllCategories(Guid SiteId)
        {
            return (from x in _context.Categories where x.SiteId.Equals(SiteId) orderby x.Name select x).ToList();
        }

        /// <summary>
        /// Gets List of UOMs per Site.
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of UOMs</returns>
        public IEnumerable<UOM> GetAllUOMs(Guid SiteId)
        {
            return (from x in _context.UOMs where x.SiteId.Equals(SiteId) orderby x.Name select x).ToList();
        }

        /// <summary>
        /// Gets List of Locations per Site
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of Locations</returns>
        public IEnumerable<Location> GetAllLocations(Guid SiteId)
        {
            return (from x in _context.Locations where x.SiteId.Equals(SiteId) orderby x.Name select x).ToList();
        }

        /// <summary>
        /// Gets List of Stock Receipts per Site
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of Stock Receipts</returns>
        public IEnumerable<StockReceipt> GetAllStockReceipts(Guid SiteId)
        {
            return (from x in _context.StockReceipts where x.SiteId.Equals(SiteId) orderby x.DateReceived select x)
                .Include(stockreceipt => stockreceipt.Product)
                .Include(stockreceipt => stockreceipt.Location)
                .ToList();
        }

        /// <summary>
        /// Gets List of Available Stock per Site
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of Stock</returns>
        public IEnumerable<StockItem> GetAllAvailableStock(Guid SiteId)
        {
            return (from x in _context.StockItems where x.QtyAvailable > 0 && x.SiteId.Equals(SiteId) orderby x.DateReceived select x)
                .Include(stockitem => stockitem.Product)
                .Include(stockitem => stockitem.Location)
                .ToList();
        }

        /// <summary>
        /// Gets List of All Stock per Site
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>List of Stock</returns>
        public IEnumerable<StockItem> GetAllStock(Guid SiteId)
        {
            return (from x in _context.StockItems where x.SiteId.Equals(SiteId) orderby x.DateReceived select x)
                .Include(stockitem => stockitem.Product)
                .Include(stockitem => stockitem.Location)
                .ToList();
        }

        #endregion

        #region "Get Item"

        /// <summary>
        /// Gets Product by Id and Site
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SiteId"></param>
        /// <returns>Product</returns>
        public Product GetProduct(string Id, Guid SiteId)
        {
            Product item = (from x in _context.Products where x.ProductId.ToString().Equals(Id) && x.SiteId.Equals(SiteId) select x)
        .Include(product => product.Category).Include(product => product.UOM).Include(product => product.Location).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Gets Category by Id and Site
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SiteId"></param>
        /// <returns>Category</returns>
        public Category GetCategory(string Id, Guid SiteId)
        {
            Category item = (from x in _context.Categories where x.CategoryId.ToString().Equals(Id) && x.SiteId.Equals(SiteId) select x).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Gets UOM by Id and Site
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SiteId"></param>
        /// <returns>UOM</returns>
        public UOM GetUOM(string Id, Guid SiteId)
        {
            UOM item = (from x in _context.UOMs where x.UOMId.ToString().Equals(Id) && x.SiteId.Equals(SiteId) select x).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Get Location by Id and Site
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SiteId"></param>
        /// <returns></returns>
        public Location GetLocation(string Id, Guid SiteId)
        {
            Location item = (from x in _context.Locations where x.LocationId.ToString().Equals(Id) && x.SiteId.Equals(SiteId) select x).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Get Stock Receipt by Id and Site
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SiteId"></param>
        /// <returns>Stock Receipt</returns>
        public StockReceipt GetStockReceipt(string Id, Guid SiteId)
        {
            StockReceipt item = (from x in _context.StockReceipts where x.StockReceiptId.ToString().Equals(Id) && x.SiteId.Equals(SiteId) select x)
        .Include(StockReceipt => StockReceipt.Product).Include(StockReceipt => StockReceipt.Location).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Gets Settings by Site
        /// </summary>
        /// <param name="SiteId"></param>
        /// <returns>Settings</returns>
        public Settings GetSettings(Guid SiteId)
        {
            Settings item = (from x in _context.Settings where x.SiteId.Equals(SiteId) select x).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Get Stock Item by Id and Site
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SiteId"></param>
        /// <returns>Stock Item</returns>
        public StockItem GetStockItem(string Id, Guid SiteId)
        {
            StockItem item = (from x in _context.StockItems where x.StockId.ToString().Equals(Id) && x.SiteId.Equals(SiteId) select x)
        .Include(StockItem => StockItem.Product).Include(StockItem => StockItem.Location).FirstOrDefault();
            return item;
        }

        #endregion

        #region "Save New Item"

        /// <summary>
        /// Add new Product to database
        /// </summary>
        /// <param name="model"></param>
        public void AddProduct(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Add new Category to databse
        /// </summary>
        /// <param name="model"></param>
        public void AddCategory(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Add new UOM to database
        /// </summary>
        /// <param name="model"></param>
        public void AddUOM(UOM model)
        {
            _context.UOMs.Add(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Add new Location to database
        /// </summary>
        /// <param name="model"></param>
        public void AddLocation(Location model)
        {
            _context.Locations.Add(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Add new Stock Receipt to database
        /// </summary>
        /// <param name="model"></param>
        public void AddStockReceipt(StockReceipt model)
        {
            _context.StockReceipts.Add(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Add new Settings to database
        /// </summary>
        /// <param name="model"></param>
        public void AddSettings(Settings model)
        {
            _context.Settings.Add(model);
            _context.SaveChangesAsync();
        }

        /// <summary>
        /// Add new Stock Item to database
        /// </summary>
        /// <param name="model"></param>
        public void AddStockItem(StockItem model)
        {
            _context.StockItems.Add(model);
            _context.SaveChanges();
        }

        #endregion

        #region "Update Existing Item"

        /// <summary>
        /// Save changes to existing Product in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveProduct(Product model)
        {
            _context.Products.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Save changes to existing Category in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveCategory(Category model)
        {
            _context.Categories.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Save changes to existing UOM in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveUOM(UOM model)
        {
            _context.UOMs.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Save changes to existing Location in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveLocation(Location model)
        {
            _context.Locations.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Save changes to existing Stock Receipt in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveStockReceipt(StockReceipt model)
        {
            _context.StockReceipts.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get Product Default Location by Product Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Location Id</returns>
        public string GetProductDefaultLocation(string Id)
        {
            string item = (from x in _context.Products where x.ProductId.ToString().Equals(Id) select x.Location.LocationId.ToString()).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// Save changes to existing Stock Item in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveStockItem(StockItem model)
        {
            _context.StockItems.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// Save changes to existing Stock Receipt in database
        /// </summary>
        /// <param name="model"></param>
        public void SaveSettings(Settings model)
        {
            _context.Settings.Update(model);
            _context.SaveChanges();
        }

        #endregion

        #region "Settings"

        public string GetGRN(Guid SiteId)
        {
            Settings settingsModel = GetSettings(SiteId);
            int GRNValue = settingsModel.GRNNo + 1;
            settingsModel.GRNNo = GRNValue;
            SaveSettings(settingsModel);
            string GRNNo = "GRN" + GRNValue;
            return GRNNo;
        }

        #endregion

    }
}
