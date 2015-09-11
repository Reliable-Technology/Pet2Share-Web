using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Pet2Share_API.Service.AccountManagement;
using Pet2Share_API.DAL;

namespace pet2share.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        
        public ViewResult Index()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model, string returnUrl)
        {
            //if (ModelState.IsValid && Login(model.Username, model.Password))
            //{
            //    return RedirectToLocal(returnUrl);
            //}

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //Redirect to HomePage
                return RedirectToAction("Index", "Index");
            }
        }
        #endregion

    }
}
