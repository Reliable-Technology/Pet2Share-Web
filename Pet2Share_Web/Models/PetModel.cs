using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class PetModel
    {
        public int PetId { get; set; }

        public int? UserId { get; set; }

        public int? PetTypeId { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[a-zA-Z\. ]+$", ErrorMessage = "Only alphabets are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        [RegularExpression(@"^[a-zA-Z\. ]+$", ErrorMessage = "Only alphabets are allowed")]
        public string FamilyName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(1000, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string AboutMe { get; set; }

        [StringLength(100, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string FavFood { get; set; }

        public string PetProfilePic { get; set; }

        public string PetProfileCover { get; set; }


        //TODO: Need to add more fields later
    }

}