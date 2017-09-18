using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Models
{
    public class Location
    {
        [Key]
        public Guid LocationId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Location field cannot be longer than 50 characters.", ErrorMessageResourceName = "", ErrorMessageResourceType = null)]
        [Display(Name = "Location")]
        public string Name { get; set; }

        public Guid SiteId { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<Product> Products { get; set; }

        public List<StockItem> StockItems { get; set; }

    }
}
