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
                return PartialView("_FeedPartial", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 3, CurrentPage));
            }
            return View();
            //return View("Index", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 10, CurrentPage));

        }

        [Authorize]
        public ActionResult UserFeed(int UserId, int PageNo)
        {

            //var CurrentPage = PageNo ?? 1;

            if (Request.IsAjaxRequest())
            {
                //Thread.Sleep(3000);
                return PartialView("_FeedPartial", PostManager.GetPostsByUser(UserId, 3, PageNo));
            }
            return View();
            //return View("Index", PostManager.GetPostsByUser(BL.BLAuth.Instance.GetUserID(), 10, CurrentPage));

        }

    }
}
