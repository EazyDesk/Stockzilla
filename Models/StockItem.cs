using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Models
{
    public class StockItem
    {

        [Key]
        public Guid StockId { get; set; }

        [MaxLength(20)]
        [Display(Name = "GRN No.")]
        public string GRNNo { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("ProductId")]
        [Required]
        [Display(Name = "Product")]
        public Guid? ProductId { get; set; }

        public virtual Location Location { get; set; }

        [ForeignKey("LocationId")]
        [Display(Name = "Location")]
        public Guid? LocationId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Serial No.")]
        public string SerialNo { get; set; }

        [MaxLength(100)]
        [Display(Name = "Batch No.")]
        public string BatchNo { get; set; }

        [Display(Name = "Date Received")]
        public DateTime DateReceived { get; set; }

        [MaxLength(100)]
        [Display(Name = "Received By")]
        public string ReceivedBy { get; set; }

        [Required]
        [Display(Name = "InitialQty")]
        public decimal InitialQty { get; set; }

        [Required]
        public decimal QtyAvailable { get; set; }

        [Required]
        [Display(Name = "Cost Per UOM")]
        public Decimal CostPerUOM { get; set; }

        [Required]
        [Display(Name = "Total Cost")]
        public Decimal TotalCost { get; set; }

        public string Notes { get; set; }

        public Guid SiteId { get; set; }

    }
}
