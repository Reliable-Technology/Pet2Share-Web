using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class UserProfileModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[a-zA-Z\. ]+$", ErrorMessage = "Only alphabets are allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[a-zA-Z\. ]+$", ErrorMessage = "Only alphabets are allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        public string AlternateEmail { get; set; }

        //[Required(ErrorMessage = " ")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[a-zA-Z0-9 \( \) \- \+]+$", ErrorMessage = "Only alpha numeric characters are allowed")]
        public string PhoneNumber { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[a-zA-Z0-9 \( \) \- \+]+$", ErrorMessage = "Only alpha numeric characters are allowed")]
        public string SecondaryPhone { get; set; }

        [StringLength(1000, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string AboutMe { get; set; }

        [StringLength(200, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string AddressLine1 { get; set; }

        [StringLength(200, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string AddressLine2 { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string City { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string State { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string Country { get; set; }

        [StringLength(8, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[0-9 \( \) \- \+]+$", ErrorMessage = "Only numeric characters are allowed")]
        public string ZipCode { get; set; }



        //TODO: Need to add more fields later
    }
    
    public class ProfileModel
    {

       



    }
}