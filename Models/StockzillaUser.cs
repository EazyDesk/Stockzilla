using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Models
{
    /// <summary>
    /// Extends the Identity User table with our own fields.
    /// </summary>
    public class StockzillaUser : IdentityUser
    {
        public Guid SiteID { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
