using Pet2Share_API.Domain;
using Pet2Share_API.Service;
using Pet2Share_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pet2Share_Web.Controllers
{
    public class PetConnectionController : Controller
    {
        //
        // GET: /Connection/

        public ActionResult Index()
        {
            var PetConnections = Pet2Share_API.Service.ConnectionManager.GetMyConnections(new Pet2Share_API.Domain.Pet() { Id = BL.BLPetCookie.Instance.GetCurrentPetId() });

            return View(new PetConnectionModel() { PetsList = PetConnections.ToList(), SearchQuery = "" });
        }

        //
        // GET: /Connection/Details/5

        public ActionResult Details(int id)
        {
            try
            {
                //check if its your profile

                if (BL.BLPetCookie.Instance.IsYourProfile(id))
                {
                    return RedirectToAction("Details", "Pets", new { @id = id });
                }


                Pet2Share_API.Domain.Pet result;

                ConnectionType conType;

                result = PetProfileManager.GetOtherPetProfile(BL.BLPetCookie.Instance.GetCurrentPetId(), id, out conType);


                if (result.Id == (id))
                {
                    int ConnCount = ConnectionManager.GetMyConnectionCount(new Pet2Share_API.Domain.Pet() { Id = id });
                    return View(new PetConnectionViewModel() { ConnectionCount = ConnCount, PetDetails = result, UserConnStatus = conType });
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

        [Authorize]
        public ActionResult SearchPets()
        {
            var RecommendedPets = ConnectionManager.GetConnectionSuggestion(new Pet2Share_API.Domain.Pet() { Id = BL.BLAuth.Instance.GetUserID() });
            return View(new PetConnectionModel() { SearchQuery = "", PetsList = RecommendedPets.ToList() });
        }

        [HttpPost]
        [Authorize]
        public ActionResult SearchPets(PetConnectionModel SearchObj)
        {
            var SearchResults = ConnectionManager.SearchPet(SearchObj.SearchQuery);

            SearchObj.PetsList = SearchResults.ToList();

            return View(SearchObj);

        }

        [Authorize]
        public ActionResult Connect(int id)
        {
            var ConnectResult = ConnectionManager.AskToConnect(new Pet2Share_API.Domain.Pet() { Id = BL.BLPetCookie.Instance.GetCurrentPetId() }, new Pet2Share_API.Domain.Pet() { Id = id });
            if (ConnectResult.IsSuccessful)
            {
                return RedirectToAction("Details", new { @id = id });
            }
            return View();
        }

        [Authorize]
        public ActionResult DeleteConnect(int id)
        {
            var ConnectResult = ConnectionManager.DeleteConnection(new Pet2Share_API.Domain.Pet() { Id = BL.BLPetCookie.Instance.GetCurrentPetId() }, new Pet2Share_API.Domain.Pet() { Id = id });
            if (ConnectResult.IsSuccessful)
            {
                return RedirectToAction("Details", new { @id = id });
            }
            return View();
        }

        [Authorize]
        public ActionResult ApproveConnect(int id)
        {
            var ConnectResult = ConnectionManager.ApproveConnection(new Pet2Share_API.Domain.Pet() { Id = BL.BLPetCookie.Instance.GetCurrentPetId() }, new Pet2Share_API.Domain.Pet() { Id = id });
            if (ConnectResult.IsSuccessful)
            {
                return RedirectToAction("Details", new { @id = id });
            }
            return View();
        }
    }
}
