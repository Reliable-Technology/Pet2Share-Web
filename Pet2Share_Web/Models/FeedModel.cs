using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class FeedModel
    {
        public List<Pet2Share_API.Domain.Post> PostList { get; set; }

        public bool IsUser { get; set; }
    }
}