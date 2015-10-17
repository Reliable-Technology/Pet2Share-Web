using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pet2Share_Web.Models
{
    public class UploadImageModel
    {
        public int UploadedForId { get; set; }

        [Display(Name = "Local file")]
        public HttpPostedFileBase File { get; set; }


        public int IsUser { get; set; }
        public int IsCover { get; set; }

        [Range(0, int.MaxValue)]
        public int X { get; set; }

        [Range(0, int.MaxValue)]
        public int Y { get; set; }

        [Range(1, int.MaxValue)]
        public int Width { get; set; }

        [Range(1, int.MaxValue)]
        public int Height { get; set; }
    }
}