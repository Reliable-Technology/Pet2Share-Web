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
        [Authorize]
        public ActionResult Index(int? id)
        {
            try
            {
                var result = new UserProfileManager(id ?? BL.BLAuth.Instance.GetUserID());
                if (result.user.Id == (id ?? BL.BLAuth.Instance.GetUserID()))
                {
                    return View(new PetsListModel() { SUser = new SmallUser(result.user), PetsList = result.user.Pets.ToList() });
                }
                else
                {
                    return RedirectToAction("NotFound", "Error");
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
        [Authorize]
        public ActionResult Details(int id)
        {
            var PetDetails = Pet.GetById(id);
            if (PetDetails != null && PetDetails.Id > 0)
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
                    PetProfileCover = PetDetails.CoverPictureURL,
                    SUser = new SmallUser(PetDetails.UserId ?? 0)
                };

                return View(PDetails);

            }
            return RedirectToAction("NotFound", "Error");
        }

        //
        // GET: /Pets/Create
        [Authorize]
        public ActionResult Create()
        {
            PetModel petDetails = new PetModel();
            petDetails.PetId = 0;
            petDetails.UserId = BLAuth.Instance.GetUserID();
            return View(petDetails);
        }

        [Authorize]
        public ActionResult VirtualPet()
        {
            return View();
        }

        //
        // GET: /Pets/CreateVirtualPet
        [Authorize]
        public ActionResult CreateVirtualPet()
        {
            var VPetResult = PetProfileManager.AddVirtualProfile(new Pet2Share_API.Domain.User(BL.BLAuth.Instance.GetUserID()));
            if (VPetResult.IsSuccessful)
            {
                return RedirectToAction("ChangeCurrentPet", "Index", new { @id = VPetResult.UpdatedId });
            }

            PetModel petDetails = new PetModel();
            petDetails.PetId = 0;
            petDetails.UserId = BLAuth.Instance.GetUserID();
            return View(petDetails);

        }
        //
        // POST: /Pets/Create

        [HttpPost]
        [Authorize]
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
        [Authorize]
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

            return RedirectToAction("NotFound", "Error");

        }

        //
        // POST: /Pets/Edit/5

        [HttpPost]
        [Authorize]
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
