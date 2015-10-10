using Pet2Share_API.Service;
using Pet2Share_Web.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pet2Share_Web.Controllers
{
    public class DashBoardController : Controller
    {
        //
        // GET: /DashBoard/
        [Authorize]
        public ActionResult Index()
        {

            int UserId = BLAuth.Instance.GetUserID();
            UserProfileManager userObj = new UserProfileManager(UserId);
            if (userObj.user.Id > 0)
            {
                return View(userObj.user);
            }
            else
            {
                //ModelState.AddModelError("Error", "");
                ViewData["Error"] = "User Not found";
                return View("Index");
            }
        }

    }
}
