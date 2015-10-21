using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class PostModal
    {
        public PostModal()
        { }
        public PostModal(int profileId, bool isUser)
        {
            ProfileId = profileId;
            IsUser = isUser;
        }
        public int ProfileId { get; set; }
        public bool IsUser { get; set; }

        [Required(ErrorMessage = " ")]
        [StringLength(300, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string PostMessage { get; set; }

    }

    public class PostCommentModal
    {
        public PostCommentModal()
        { }
        public PostCommentModal(int postId, int profileId, bool isUser)
        {
            PostId = postId;
            ProfileId = profileId;
            IsUser = isUser;
        }
        public int ProfileId { get; set; }
        public int PostId { get; set; }
        public bool IsUser { get; set; }

        [StringLength(300, MinimumLength = 0, ErrorMessage = "Maximum {1} characters are allowed")]
        public string PostMessage { get; set; }

    }

}