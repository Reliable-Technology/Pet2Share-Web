using Pet2Share_Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pet2Share_Web.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Home/UploadImage
        [Authorize]
        public ActionResult UploadImage(int id, int isUser, int isCover)
        {
            ////Just to distinguish between ajax request (for: modal dialog) and normal request
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView();
            //}

            UploadImageModel imgModel = new UploadImageModel();
            imgModel.IsUser = isUser;
            imgModel.IsCover = isCover;
            imgModel.UploadedForId = id;
            return View(imgModel);
        }

        //
        // POST: /Home/UploadImage

        [HttpPost]
        [Authorize]
        public ActionResult UploadImage(UploadImageModel model)
        {
            //Check if all simple data annotations are valid
            if (ModelState.IsValid)
            {
                //Prepare the needed variables
                Bitmap original = null;
                var name = "newimagefile";
                var errorField = string.Empty;


                if (model.File != null) // model.IsFile
                {
                    errorField = "File";
                    name = Path.GetFileNameWithoutExtension(model.File.FileName);
                    original = Bitmap.FromStream(model.File.InputStream) as Bitmap;
                }

                //If we had success so far
                if (original != null)
                {
                    var img = BL.ImageHelper.Instance.CreateImage(original, model.X, model.Y, model.Width, model.Height);

                    Pet2Share_API.Utility.BoolExt UploadResult;

                    if (model.IsUser > 0)
                    {
                        if (model.IsCover <= 0)
                        {
                            UploadResult = Pet2Share_API.Service.UserProfileManager.UpdateProfilePicture(img, name + "." + Pet2Share_API.Utility.ImageType.png.ToString(), Pet2Share_API.Utility.ImageType.png, model.UploadedForId);
                        }
                        else
                        {
                            UploadResult = Pet2Share_API.Service.UserProfileManager.UpdateCoverPicture(img, name + "." + Pet2Share_API.Utility.ImageType.png.ToString(), Pet2Share_API.Utility.ImageType.png, model.UploadedForId);
                        }
                    }
                    else
                    {
                        if (model.IsCover <= 0)
                        {
                            UploadResult = Pet2Share_API.Service.PetProfileManager.UpdateProfilePicture(img, name + "." + Pet2Share_API.Utility.ImageType.png.ToString(), Pet2Share_API.Utility.ImageType.png, model.UploadedForId);

                        }
                        else
                        {
                            UploadResult = Pet2Share_API.Service.PetProfileManager.UpdateCoverPicture(img, name + "." + Pet2Share_API.Utility.ImageType.png.ToString(), Pet2Share_API.Utility.ImageType.png, model.UploadedForId);
                        }
                    }
                    if (UploadResult.IsSuccessful)
                    {
                        if (model.IsUser > 0)
                        {
                            if (model.IsCover <= 0)
                            {
                                HttpCookie cookie = FormsAuthentication.GetAuthCookie(User.Identity.Name, true);
                                var ticket = FormsAuthentication.Decrypt(cookie.Value);

                                string[] Userarray = User.Identity.Name.Split('$');

                                Userarray[3] = UploadResult.Message;


                                var newticket = new FormsAuthenticationTicket(ticket.Version,
                                                                               string.Join("$", Userarray),
                                                                              ticket.IssueDate,
                                                                              ticket.Expiration,
                                                                              true,
                                                                             string.Join("$", Userarray),
                                                                              ticket.CookiePath);

                                cookie.Value = FormsAuthentication.Encrypt(newticket);
                                cookie.Expires = newticket.Expiration.AddHours(24);
                                Response.Cookies.Set(cookie);


                            }


                            return RedirectToAction("Index", "Profile");
                        }
                        else
                        {
                            return RedirectToAction("Details", "Pets", new { id = model.UploadedForId });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(errorField, "Your upload did not seem valid. Please try again using only correct images!");

                    }

                }
                else //Otherwise we add an error and return to the (previous) view with the model data
                    ModelState.AddModelError(errorField, "Your upload did not seem valid. Please try again using only correct images!");
            }

            return View(model);
        }




    }
}
