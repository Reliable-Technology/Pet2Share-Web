using Pet2Share_API.Service;
using Pet2Share_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pet2Share_Web.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        [Authorize]
        public ActionResult Index(int? id)
        {

            try
            {
                UserProfileManager result;
                result = new UserProfileManager(id ?? BL.BLAuth.Instance.GetUserID());
                if (result.user.Id == (id ?? BL.BLAuth.Instance.GetUserID()))
                {
                    return View(result.user);
                }

                return RedirectToAction("NotFound", "Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                //redirect to error page
                return View();
            }

        }

        //
        // GET: /Profile/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {



            return View();
        }


        //
        //Get for updating loggedin user profile
        [Authorize]
        public ActionResult Edit()
        {
            UserProfileManager userObj = new UserProfileManager(BL.BLAuth.Instance.GetUserID());
            if (userObj.user.Id == BL.BLAuth.Instance.GetUserID())
            {
                UserProfileModel UserDetails = new UserProfileModel
                {
                    UserId = userObj.user.Id,
                    FirstName = userObj.user.P.FirstName,
                    LastName = userObj.user.P.LastName,
                    AboutMe = userObj.user.P.AboutMe,
                    AddressLine1 = userObj.user.P.Addr == null ? "" : userObj.user.P.Addr.AddressLine1,
                    AddressLine2 = userObj.user.P.Addr == null ? "" : userObj.user.P.Addr.AddressLine2,
                    AlternateEmail = userObj.user.P.AlternateEmail,
                    City = userObj.user.P.Addr == null ? "" : userObj.user.P.Addr.City,
                    Country = userObj.user.P.Addr == null ? "" : userObj.user.P.Addr.Country,
                    DateOfBirth = userObj.user.P.DOB,
                    Email = userObj.user.Email,
                    PhoneNumber = userObj.user.Phone,
                    SecondaryPhone = userObj.user.P.SecondaryPhone,
                    State = userObj.user.P.Addr == null ? "" : userObj.user.P.Addr.State,
                    ZipCode = userObj.user.P.Addr == null ? "" : userObj.user.P.Addr.ZipCode
                };

                return View(UserDetails);
            }
            else
            {
                ModelState.AddModelError("Error", "Profile not found");
            }
            return View(new UserProfileModel());
        }

        //
        // POST: /Profile/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserProfileModel userObj)
        {
            try
            {
                // TODO: Add update logic here
                var result = UserProfileManager.UpdateProfile(userObj.UserId, userObj.FirstName, userObj.LastName, userObj.Email, userObj.AlternateEmail, userObj.DateOfBirth,
                      userObj.PhoneNumber, userObj.SecondaryPhone, "", userObj.AboutMe, userObj.AddressLine1, userObj.AddressLine2, userObj.City, userObj.State, userObj.Country, userObj.ZipCode);

                if (result.IsSuccessful)
                {
                    ViewBag.Success = "Profile updated successfully.";
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    ModelState.AddModelError("Error", result.Message);


                }

                return View(userObj);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(userObj);
            }
        }


    }
}
