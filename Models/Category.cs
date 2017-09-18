using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Category field cannot be longer than 50 characters.", ErrorMessageResourceName = "", ErrorMessageResourceType = null)]
        [Display(Name = "Category")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Guid SiteId { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
