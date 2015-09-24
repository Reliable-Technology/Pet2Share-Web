using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Pet2Share_Web.Models
{
    [MetadataTypeAttribute(typeof(User.UserMetaData))]
    public partial class User
    {
        internal sealed class UserMetaData
        {
            private UserMetaData() { }
            public int Id;

            [Display(Name = "Username")]
            [Required(ErrorMessage = "You must fill in all of the fields.")]
            [MinLength(4, ErrorMessage = "{0} must have minimum length is {1} characters.")]
            [MaxLength(50, ErrorMessage = "{0} must have maximum length is {1} characters.")]
            public string Username;

            [Display(Name = "Password")]
            [Required(ErrorMessage = "You must fill in all of the fields.")]
            [MinLength(4, ErrorMessage = "{0} must have minimum length is {1} characters.")]
            [MaxLength(100, ErrorMessage = "{0} must have maximum length is {1} characters.")]
            public string Password;

            [Display(Name = "Email")]
            [Required(ErrorMessage = "You must fill in all of the fields.")]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                ErrorMessage = "Please enter correct email address")]
            public string Email;

            [Display(Name = "AlternateEmail")]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                ErrorMessage = "Please enter correct alternate email address")]
            public string AlternateEmail;

            [Display(Name = "Phone")]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                ErrorMessage = "Please enter correct alternate email address")]
            public string Phone;

        }
    }
}