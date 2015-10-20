using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class ConnectionModel
    {
        [Required(ErrorMessage = " ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Maximum {1} characters are allowed")]
        public string SearchQuery { get; set; }

        public List<Pet2Share_API.Domain.SmallUser> UsersList { get; set; }

    }

    public class ConnectionViewModel
    {
        public Pet2Share_API.Domain.User UserDetails { get; set; }
        public int ConnectionCount { get; set; }
    }
}