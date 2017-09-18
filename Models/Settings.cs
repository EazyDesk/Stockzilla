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
    public class Settings
    {
        [Key] 
        public Guid SiteId { get; set; }

        [Display(Name = "Current GRN No.")]
        public int GRNNo { get; set; }
    }
}
