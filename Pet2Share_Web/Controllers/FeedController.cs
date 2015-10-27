using Pet2Share_API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Pet2Share_Web.Controllers
{
    public class FeedController : Controller
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
                //PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 3, CurrentPage)
                return PartialView("_FeedPartial", new Pet2Share_Web.Models.FeedModel() { PostList = PostManager.GetMyFeed(BL.BLAuth.Instance.GetUserID(), false, 3, CurrentPage), IsUser = true });
            }
            return View();
            //return View("Index", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 10, CurrentPage));

        }

        [Authorize]
        public ActionResult UserFeed(int UserId, int PageNo, bool isUser)
        {

            //var CurrentPage = PageNo ?? 1;

            if (Request.IsAjaxRequest())
            {
                //Thread.Sleep(3000);
                return PartialView("_FeedPartial", new Pet2Share_Web.Models.FeedModel() { PostList = isUser ? PostManager.GetPostsByUser(UserId, 3, PageNo) : PostManager.GetPostsByPet(UserId, 3, PageNo), IsUser = isUser });
            }
            return View();
            //return View("Index", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 10, CurrentPage));

        }

    }
}
