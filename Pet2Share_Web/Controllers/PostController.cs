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
    public class PostController : ExtendedController
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddPost(PostModal PostObj)
        {
            try
            {
                // TODO: Add update logic here
                var result = PostManager.AddPost(1, PostObj.PostMessage, PostObj.ProfileId, !PostObj.IsUser);
                if (result.Id > 0)
                {
                    ViewBag.Success = "Post added successfully";
                    return RedirectToAction("Index", "Feed");
                }
                else
                {
                    ModelState.AddModelError("Error", "");


                }

                return View(PostObj);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(PostObj);
            }

        }

        [HttpPost]
        [Authorize]
        public JsonResult AddComment(string PostId, string CommentDesc)
        {
            try
            {
                int Pid = 0;
                int.TryParse(PostId, out Pid);

                // TODO: Add update logic here
                var result = PostManager.AddComment(Pid, BL.BLAuth.Instance.GetUserID(), false, CommentDesc);
                if (result.Id > 0)
                {
                    ViewBag.Success = "Comment added successfully";
                    //return View(result); //RedirectToAction("Index", "Feed");
                    return Json(new { CommentData = RenderPartialViewToString("_CommentItem", new List<Comment>() { result }) });
                }
                else
                {
                    ModelState.AddModelError("Error", "");


                }

                return Json(new { Error = "Comment not posted." });

            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult GetComments(string PostId, string CommentDesc)
        {
            try
            {
                int Pid = 0;
                int.TryParse(PostId, out Pid);

                // TODO: Add update logic here
                var result = PostManager.GetComments(Pid);
                if (result.Count > 0)
                {
                    return Json(new { CommentData = RenderPartialViewToString("_CommentItem", result) });
                }
                else
                {
                    ModelState.AddModelError("Error", "");
                }

                return Json(new { Error = "" });

            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }

        //
        // GET: /Post/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Post/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Post/Create

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
        // GET: /Post/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Post/Edit/5

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
        // GET: /Post/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Post/Delete/5

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
