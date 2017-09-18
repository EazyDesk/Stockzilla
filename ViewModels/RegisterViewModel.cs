using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
