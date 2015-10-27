using Pet2Share_API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pet2Share_Web.Controllers
{
    public class PetFeedController : Controller
    {
        //
        // GET: /Feed/
        [Authorize]
        public ActionResult Index(int? id)
        {

            var CurrentPage = id ?? 1;

            if (Request.IsAjaxRequest())
            {
                //Thread.Sleep(3000);
                return PartialView("_FeedPartial", new Pet2Share_Web.Models.FeedModel() { PostList = PostManager.GetMyFeed(BL.BLPetCookie.Instance.GetCurrentPetId(), true, 3, CurrentPage), IsUser = false });
            }
            return View();
            //return View("Index", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 10, CurrentPage));

        }

        //[Authorize]
        //public ActionResult GetFeed(int PetId, int PageNo, , bool isUser)
        //{

        //    //var CurrentPage = PageNo ?? 1;

        //    if (Request.IsAjaxRequest())
        //    {
        //        //Thread.Sleep(3000);
        //        return PartialView("_FeedPartial", PostManager.GetPostsByPet(PetId, 3, PageNo));
        //    }
        //    return View();
        //    //return View("Index", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 10, CurrentPage));

        //}
    }
}
