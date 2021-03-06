using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
