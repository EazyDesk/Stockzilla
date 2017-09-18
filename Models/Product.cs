using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Stockzilla.Models
{
    public class Product
    {
        /// <summary>
        /// Constructor required to set the Reorder Level to 0 by default, otherwise it appears blank when adding a new product.
        /// </summary>
        public Product()
        {
            ReorderLevel = 0;
        }

        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Product Code field cannot be longer than 50 characters.", ErrorMessageResourceName = "", ErrorMessageResourceType = null)]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [MaxLength(200, ErrorMessage = "The Description field cannot be longer than 200 characters.", ErrorMessageResourceName = "", ErrorMessageResourceType = null)]
        public string Description { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "Category")]
        public Guid? CategoryId { get; set; }

        public virtual UOM UOM { get; set; }

        [ForeignKey("UOMId")]
        [Display(Name = "UOM")]
        public Guid? UOMId { get; set; }

        public Guid SiteId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Traceability")]
        public string Traceability { get; set; }

        [Display(Name = "Stock Reorder Level")]
        public Decimal ReorderLevel { get; set; }

        public virtual Location Location { get; set; }

        [ForeignKey("LocationId")]
        [Display(Name = "Default Location")]
        public Guid? LocationId { get; set; }

        public List<StockItem> StockItems { get; set; }
    }
}
