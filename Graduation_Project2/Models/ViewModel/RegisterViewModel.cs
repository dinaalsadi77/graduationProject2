using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation_Project2.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="user@domain.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Miss Match")]
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
    }
}
