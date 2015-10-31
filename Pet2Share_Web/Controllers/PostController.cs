using Pet2Share_API.Domain;
using Pet2Share_API.Service;
using Pet2Share_Web.BL;
using Pet2Share_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                var result = PostManager.AddPost(1, PostObj.PostMessage, PostObj.ProfileId, !PostObj.IsUser, PostObj.PostPrivacyID == 1 ? true : false);
                if (result.Id > 0)
                {
                    bool isSavedSuccessfully = true;
                    string fName = "";
                    try
                    {
                        foreach (string fileName in Request.Files)
                        {
                            HttpPostedFileBase file = Request.Files[fileName];
                            //Save file content goes here
                            fName = file.FileName;
                            if (file != null && file.ContentLength > 0)
                            {

                                var PostPicUpload = PostManager.UploadPostPicture(BL.ImageHelper.Instance.StreamToByte(file.InputStream), fName, Pet2Share_API.Utility.ImageType.png, result.Id);

                                isSavedSuccessfully = PostPicUpload.IsSuccessful;

                                //var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                                //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                                //var fileName1 = Path.GetFileName(file.FileName);

                                //bool isExists = System.IO.Directory.Exists(pathString);

                                //if (!isExists)
                                //    System.IO.Directory.CreateDirectory(pathString);

                                //var path = string.Format("{0}\\{1}", pathString, file.FileName);
                                //file.SaveAs(path);

                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        isSavedSuccessfully = false;
                    }


                    if (isSavedSuccessfully)
                    {
                        ViewBag.Success = "Post added successfully";
                        if (PostObj.IsUser)
                        {
                            if (Request.IsAjaxRequest())
                            {
                                //return RedirectToAction("Index", "Feed");
                                return Json(new { Message = Url.Action("Index", "Feed") });
                            }
                            else
                            {
                                return RedirectToAction("Index", "Feed");
                            }
                        }
                        else
                        {
                            if (Request.IsAjaxRequest())
                            {
                                //return RedirectToAction("Index", "Feed");
                                return Json(new { Message = Url.Action("Index", "PetFeed") });
                            }
                            else
                            {
                                return RedirectToAction("Index", "PetFeed");
                            }

                        }

                        //  return Json(new { Message = fName });
                    }
                    else
                    {
                        if (Request.IsAjaxRequest())
                        {
                            return Json(new { Message = "Error in saving file", Error = "Error" });
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Error in saving file");
                            return View(PostObj);
                        }

                    }

                }
                else
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { Message = "Error in saving file", Error = "Error" });
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Error in saving file");
                        return View(PostObj);
                    }

                }


            }
            catch (Exception ex)
            {

                //return View(PostObj);
                if (Request.IsAjaxRequest())
                {
                    //return RedirectToAction("Index", "Feed");
                    return Json(new { Message = ex.Message, Error = "Error" });
                }
                else
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View(PostObj);
                }


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

                PostManager PostMn= new PostManager(Pid);
                var postResult = 

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

        ////
        //// GET: /Post/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Post/Delete/5

        [HttpPost]
        public JsonResult DeletePost(int postId, bool IsPet)
        {
            try
            {
                var Result = Pet2Share_API.Service.PostManager.DeletePost(postId, IsPet ? BL.BLPetCookie.Instance.GetCurrentPetId() : BL.BLAuth.Instance.GetUserID(), IsPet);
                if (Result.IsSuccessful)
                {
                    return Json(new { Success = "Post deleted successfully" });
                }
                else
                { return Json(new { Error = Result.Message }); }
            }
            catch
            {
                return Json(new { Error = "Post not deleted." });
            }
        }

        [HttpPost]
        public JsonResult DeleteComment(int commentId)
        {
            try
            {
                var Result = Pet2Share_API.Service.PostManager.DeleteComment(commentId, BL.BLAuth.Instance.GetUserID());
                if (Result.IsSuccessful)
                {
                    return Json(new { Success = "Comment deleted successfully" });
                }
                else
                { return Json(new { Error = Result.Message }); }
            }
            catch
            {
                return Json(new { Error = "Comment not deleted." });
            }
        }

    }
}
