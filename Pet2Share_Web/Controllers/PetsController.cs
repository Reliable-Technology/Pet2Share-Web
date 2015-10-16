using Pet2Share_API.Domain;
using Pet2Share_API.Service;
using Pet2Share_Web.BL;
using Pet2Share_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pet2Share_Web.Controllers
{
    public class PetsController : Controller
    {
        //
        // GET: /Pets/

        public ActionResult Index()
        {
            try
            {
                var result = new UserProfileManager(BL.BLAuth.Instance.GetUserID());
                if (result.user.Id == BL.BLAuth.Instance.GetUserID())
                {
                    return View(result.user.Pets.ToList());
                }
            }
            catch (Exception ex)
            {
                //redirect to error page
            }

            return View();
        }

        //
        // GET: /Pets/Details/5

        public ActionResult Details(int id)
        {
            var PetDetails = Pet.GetById(id);
            if (PetDetails.UserId == BLAuth.Instance.GetUserID())
            {
                PetModel PDetails = new PetModel
                {
                    AboutMe = PetDetails.About,
                    DateOfBirth = PetDetails.DOB,
                    FamilyName = PetDetails.FamilyName,
                    FavFood = PetDetails.FavFood,
                    Name = PetDetails.Name,
                    PetId = PetDetails.Id,
                    PetTypeId = PetDetails.PetTypeId,
                    UserId = PetDetails.UserId,
                    PetProfilePic = PetDetails.ProfilePictureURL,
                    PetProfileCover = PetDetails.CoverPictureURL
                };

                return View(PDetails);

            }

            //redirect to error page
            return View();
        }

        //
        // GET: /Pets/Create

        public ActionResult Create()
        {
            PetModel petDetails = new PetModel();
            petDetails.PetId = 0;
            petDetails.UserId = BLAuth.Instance.GetUserID();
            return View(petDetails);
        }

        //
        // POST: /Pets/Create

        [HttpPost]
        public ActionResult Create(PetModel PetObj)
        {
            
                // TODO: Add insert logic here
                try
                {
                    // TODO: Add update logic here
                    var PetResult = PetProfileManager.AddProfile(PetObj.Name, PetObj.FamilyName, PetObj.UserId ?? 0, PetObj.PetTypeId, PetObj.DateOfBirth, "", "", PetObj.AboutMe, PetObj.FavFood);
                    if (PetResult.IsSuccessful)
                    {
                        return RedirectToAction("Details", new { id = PetObj.PetId });
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Error", "Pet could not be created right now.");
                    return View(PetObj);
                }
                
            
        }

        //
        // GET: /Pets/Edit/5

        public ActionResult Edit(int id)
        {
            var PetDetails = Pet.GetById(id);
            if (PetDetails.UserId == BLAuth.Instance.GetUserID())
            {
                PetModel PDetails = new PetModel
                {
                    AboutMe = PetDetails.About,
                    DateOfBirth = PetDetails.DOB,
                    FamilyName = PetDetails.FamilyName,
                    FavFood = PetDetails.FavFood,
                    Name = PetDetails.Name,
                    PetId = PetDetails.Id,
                    PetTypeId = PetDetails.PetTypeId,
                    UserId = PetDetails.UserId,
                    PetProfilePic = PetDetails.ProfilePictureURL,
                    PetProfileCover = PetDetails.CoverPictureURL
                };

                return View(PDetails);

            }

            ModelState.AddModelError("Error", "Profile not updated");
            return View();

        }

        //
        // POST: /Pets/Edit/5

        [HttpPost]
        public ActionResult Edit(PetModel PetObj)
        {
            try
            {
                // TODO: Add update logic here
                var PetResult = PetProfileManager.UpdateProfile(PetObj.PetId, PetObj.Name, PetObj.FamilyName, PetObj.UserId ?? 0, PetObj.PetTypeId, PetObj.DateOfBirth, "", "", PetObj.AboutMe, PetObj.FavFood);
                if (PetResult.IsSuccessful)
                {
                    return RedirectToAction("Details", new { id = PetObj.PetId });
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Error", "Profile not updated");
                return View(PetObj);
            }
        }

        //
        // GET: /Pets/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Pets/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
