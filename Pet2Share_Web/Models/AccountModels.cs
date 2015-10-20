using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = " ")]
        [Display(Name = "Username")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Username { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = " ")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " ")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [System.Web.Mvc.Remote("doesUserExist", "Index", HttpMethod = "POST", ErrorMessage = "username already taken")]
        [Display(Name = "User name")]
        public string Email { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Compare("Confirm Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }


    public class IndexModel
    {
        public RegisterModel Register { get; set; }
        public LoginModel Login { get; set; }
    }

    
}