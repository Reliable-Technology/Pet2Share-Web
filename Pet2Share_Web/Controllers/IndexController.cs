using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pet2Share_API.DAL;
using Pet2Share_Web.Models;


namespace Pet2Share_Web.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            //if (_LoginModel.Username != null)
            //    Login(_LoginModel);
            //if (_RegisterModel.Email != null)
            //    Register(_RegisterModel);

            var IndexM = new IndexModel();
            IndexM.Login = new LoginModel();
            IndexM.Register = new RegisterModel();

            return View(IndexM);
        }

        //
        // POST: Login
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            var obj = Pet2Share_API.Service.AccountManagement.Login(model.Username, model.Password);
            if (obj != null && obj.Id > 0)
            {
                FormsAuthentication.SetAuthCookie(obj.Id.ToString() + "$" + obj.Username + "$" + obj.P.FirstName + " " + obj.P.LastName + "$" + obj.P.ProfilePictureURL, true);

                return Json(new { result = "Redirect", url = Url.Action("Index", "Feed") });
                //return RedirectToLocal("");
            }
            else
            {
                ModelState.AddModelError("Error", "Username or password not correct.");
                //return View("Index", model);
                return PartialView("_Login", model);
            }
            //return View();
        }

        //
        // POST: Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                var obj = Pet2Share_API.Service.AccountManagement.RegisterNewUser(model.Email, model.Password, model.FirstName, model.LastName, model.Email, null);
                if (obj != null && obj.Id > 0)
                {
                    FormsAuthentication.SetAuthCookie(obj.Id.ToString() + "$" + obj.Username + "$" + obj.P.FirstName + " " + obj.P.LastName + obj.P.AvatarURL, true);
                    return Json(new { result = "Redirect", url = Url.Action("Index", "Dashboard") });
                    //return RedirectToLocal("");
                }
                else
                {
                    ModelState.AddModelError("Error", "Could not register your profile, Please try again.");
                    return PartialView("_Register", model);
                }
                //
                ViewData["Message"] = "You have been successfully registered and logged in.";
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }

            return RedirectToLocal("");
        }


        [HttpPost]
        public JsonResult doesUserExist(string Email)
        {
            bool IsReferenceExists = !Pet2Share_API.Service.AccountManagement.IsExistingUser(Email);

            return Json(IsReferenceExists, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
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
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

    }
}
