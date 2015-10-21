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
        ////
        //// GET: /Connection/

        //public ActionResult Index()
        //{
        //    var UserConnections = Pet2Share_API.Service.ConnectionManager.GetMyConnection(new Pet2Share_API.Domain.User() { Id = BL.BLAuth.Instance.GetUserID() });

        //    return View(new ConnectionModel() { UsersList = UserConnections.ToList(), SearchQuery = "" });
        //}

        ////
        //// GET: /Connection/Details/5

        //public ActionResult Details(int id)
        //{
        //    try
        //    {
        //        Pet2Share_API.Domain.User result;

        //        ConnectionType conType;

        //        result = UserProfileManager.GetOtherUserProfile(BL.BLAuth.Instance.GetUserID(), id, out conType);


        //        if (result.Id == (id))
        //        {
        //            int ConnCount = ConnectionManager.GetMyConnectionCount(new Pet2Share_API.Domain.User() { Id = id });
        //            return View(new ConnectionViewModel() { ConnectionCount = ConnCount, UserDetails = result, UserConnStatus = conType });
        //        }

        //        return RedirectToAction("NotFound", "Error");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("Error", ex.Message);
        //        //redirect to error page
        //        return View();
        //    }

        //}

        //[Authorize]
        //public ActionResult SearchUsers()
        //{
        //    var RecommendedUsers = ConnectionManager.GetConnectionSuggestion(new Pet2Share_API.Domain.User() { Id = BL.BLAuth.Instance.GetUserID() });
        //    return View(new ConnectionModel() { SearchQuery = "", UsersList = RecommendedUsers.ToList() });
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult SearchUsers(ConnectionModel SearchObj)
        //{
        //    var SearchResults = ConnectionManager.SearchUser(SearchObj.SearchQuery);

        //    SearchObj.UsersList = SearchResults.ToList();

        //    return View(SearchObj);

        //}

        //[Authorize]
        //public ActionResult Connect(int id)
        //{
        //    var ConnectResult = ConnectionManager.AskToConnect(new Pet2Share_API.Domain.User() { Id = BL.BLAuth.Instance.GetUserID() }, new Pet2Share_API.Domain.User() { Id = id });
        //    if (ConnectResult.IsSuccessful)
        //    {
        //        return RedirectToAction("Details", new { @id = id });
        //    }
        //    return View();
        //}

        //[Authorize]
        //public ActionResult DeleteConnect(int id)
        //{
        //    var ConnectResult = ConnectionManager.DeleteConnection(new Pet2Share_API.Domain.User() { Id = BL.BLAuth.Instance.GetUserID() }, new Pet2Share_API.Domain.User() { Id = id });
        //    if (ConnectResult.IsSuccessful)
        //    {
        //        return RedirectToAction("Details", new { @id = id });
        //    }
        //    return View();
        //}

        //[Authorize]
        //public ActionResult ApproveConnect(int id)
        //{
        //    var ConnectResult = ConnectionManager.ApproveConnection(new Pet2Share_API.Domain.User() { Id = BL.BLAuth.Instance.GetUserID() }, new Pet2Share_API.Domain.User() { Id = id });
        //    if (ConnectResult.IsSuccessful)
        //    {
        //        return RedirectToAction("Details", new { @id = id });
        //    }
        //    return View();
        //}
    }
}
