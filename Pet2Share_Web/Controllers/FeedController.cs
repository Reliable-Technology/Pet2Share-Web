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


        

        //
        // GET: /Feed/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Feed/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Feed/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Feed/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Feed/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Feed/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Feed/Delete/5

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
