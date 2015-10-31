using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Mvc;
using Pet2Share_API.Service;

namespace Pet2Share_Web.BL
{
    public sealed class BLAuth
    {
        private static readonly BLAuth instance = new BLAuth();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BLAuth()
        {
        }

        private BLAuth()
        {
        }

        public static BLAuth Instance
        {
            get
            {
                return instance;
            }
        }


        public string GetProfileUrl(UrlHelper url, int ViewerId, bool IsPet)
        {
            if (IsPet)
            {
                if (BLPetCookie.Instance.IsYourProfile(ViewerId))
                {
                    return url.Action("Details", "Pets", new { @id = ViewerId }).ToString();
                }
                else
                {
                    return url.Action("Details", "PetConnection", new { @id = ViewerId }).ToString();
                }
            }
            else
            {
                if (BL.BLAuth.Instance.IsYourProfile(ViewerId))
                {
                    return url.Action("Index", "Profile").ToString();
                }
                else
                {
                    return url.Action("Details", "Connection", new { @id = ViewerId }).ToString();
                }
            }
        }

        public bool IsYourPost(int ViewerId, bool isPet)
        {
            if (isPet)
            {
                return BLPetCookie.Instance.GetCurrentPetId() == ViewerId ? true : false;
            }
            else
            {
                return IsYourProfile(ViewerId);
            }
        }

        public bool IsYourComment(int ViewerId)
        {
            return IsYourProfile(ViewerId);
        }


        public int GetUserID()
        {
            HttpContext context = HttpContext.Current;

            if (context.User.Identity.IsAuthenticated)
            {
                string[] temp = context.User.Identity.Name.Split('$');

                int user_id = 0;

                int.TryParse(temp[0].ToString(), out user_id);

                return user_id;
            }
            else
            {
                return 0;
            }

        }

        public bool IsYourProfile(int ViewerId)
        {
            return GetUserID() == ViewerId ? true : false;
        }

        public string GetUserName()
        {
            HttpContext context = HttpContext.Current;

            if (context.User.Identity.IsAuthenticated)
            {
                string[] temp = context.User.Identity.Name.Split('$');

                if (temp.Count() > 1)
                {
                    return temp[1];
                }
            }
            return "Guest";
        }


        public string GetFullName()
        {
            HttpContext context = HttpContext.Current;

            if (context.User.Identity.IsAuthenticated)
            {
                string[] temp = context.User.Identity.Name.Split('$');

                if (temp.Count() > 3)
                {
                    return temp[3];
                }
            }
            return "Guest";
        }
        public string GetProfilePic()
        {
            HttpContext context = HttpContext.Current;

            if (context.User.Identity.IsAuthenticated)
            {
                string[] temp = context.User.Identity.Name.Split('$');

                if (temp.Count() > 2)
                {
                    return temp[2];
                    //return GetValidImage("HomeFiles/Home_" + Pet2Share_Web.BL.BLAuth.Instance.GetUserID() + @"/" + temp[5]);
                }
            }
            return "/Images/NoPet.png";
        }


        public string HashPassword(string pasword)
        {
            byte[] arrbyte = new byte[pasword.Length];
            using (SHA1 hash = new SHA1CryptoServiceProvider())
            {
                arrbyte = hash.ComputeHash(Encoding.UTF8.GetBytes(pasword));
            }
            return Convert.ToBase64String(arrbyte);
        }

        public string[] Roles()
        {
            List<string> Roles = new List<string>();

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string UserName = HttpContext.Current.User.Identity.Name;

                if (UserName.Contains("$"))
                {
                    string tempRoles = UserName.Split('$')[1];

                    return tempRoles.Split(',').ToArray();

                }
            }

            return Roles.ToArray();
        }


    }

    public sealed class BLPetViewBag : Controller
    {
        private static readonly BLPetViewBag instance = new BLPetViewBag();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BLPetViewBag()
        {
        }

        private BLPetViewBag()
        {
        }

        public static BLPetViewBag Instance
        {
            get
            {
                return instance;
            }
        }


        public List<Pet2Share_API.Domain.Pet> GetPets()
        {
            List<Pet2Share_API.Domain.Pet> PetsList = new List<Pet2Share_API.Domain.Pet>();
            if (ViewBag.CurrentPets != null)
            {
                PetsList = ViewBag.CurrentPets as List<Pet2Share_API.Domain.Pet>;
            }
            else
            {
                UserProfileManager UserPetsObj = new UserProfileManager(BLAuth.Instance.GetUserID());
                PetsList = UserPetsObj.user.Pets.ToList();

            }
            return PetsList;
        }

        //public int CurrentPet
        //{
        //    get
        //    {

        //    }
        //    set
        //    {


        //    }
        //}

    }

    public sealed class BLPetCookie
    {
        private static readonly BLPetCookie instance = new BLPetCookie();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BLPetCookie()
        {
        }

        private BLPetCookie()
        {
        }

        public static BLPetCookie Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsYourProfile(int viewerID)
        {
            return GetCurrentPetId() == viewerID ? true : false;
        }

        public string GetCurrentPetName()
        {
            var CurrentPet = BLPetViewBag.Instance.GetPets().Where(p => p.Id == GetCurrentPetId()).FirstOrDefault();
            if (CurrentPet != null)
            {
                return CurrentPet.Name;
            }
            return "Virtua";
        }

        public string GetCurrentPetProfilePic()
        {
            var CurrentPet = BLPetViewBag.Instance.GetPets().Where(p => p.Id == GetCurrentPetId()).FirstOrDefault();
            if (CurrentPet != null)
            {
                return CurrentPet.ProfilePictureURL;
            }
            return "";
        }

        public int CheckPetCookie(int UserId)
        {
            int CurrentPetId = 0;
            try
            {
                string CPetid;
                if (HttpContext.Current.Request.Cookies["UserSettings_" + UserId] != null)
                {

                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["UserSettings_" + UserId];
                    CPetid = Convert.ToString(myCookie["CurrentPet"]);
                }
                else
                {
                    return 0;
                }
                int.TryParse(CPetid, out CurrentPetId);
            }
            catch
            {

            }
            return CurrentPetId;
        }

        public int GetCurrentPetId()
        {
            int CurrentPetId = 0;
            try
            {
                string CPetid;
                if (HttpContext.Current.Request.Cookies["UserSettings_" + BLAuth.Instance.GetUserID()] != null)
                {

                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["UserSettings_" + BLAuth.Instance.GetUserID()];
                    CPetid = Convert.ToString(myCookie["CurrentPet"]);
                }
                else
                {
                    HttpCookie myCookie = new HttpCookie("UserSettings_" + BLAuth.Instance.GetUserID());
                    myCookie["CurrentPet"] = CPetid = BLPetViewBag.Instance.GetPets().FirstOrDefault().Id.ToString();
                    myCookie.Expires = DateTime.Now.AddDays(30d);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
                int.TryParse(CPetid, out CurrentPetId);
            }
            catch
            {

            }
            return CurrentPetId;
        }

        public void SetCurrentPet(int Id)
        {

            try
            {

                if (HttpContext.Current.Response.Cookies["UserSettings_" + BLAuth.Instance.GetUserID()] != null)
                {

                    HttpContext.Current.Response.Cookies["UserSettings_" + BLAuth.Instance.GetUserID()]["CurrentPet"] = Id.ToString();
                    //myCookie["CurrentPet"] = Id.ToString();
                }
                else
                {
                    HttpCookie myCookie = new HttpCookie("UserSettings_" + BLAuth.Instance.GetUserID());
                    myCookie["CurrentPet"] = Id.ToString();// BLPetViewBag.Instance.GetPets().FirstOrDefault().Id.ToString();
                    myCookie.Expires = DateTime.Now.AddDays(30d);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }

            }
            catch
            {

            }

        }

        //public int CurrentPet
        //{
        //    get
        //    {

        //    }
        //    set
        //    {


        //    }
        //}

    }

    public sealed class BLControllerExtended : Controller
    {
        private static readonly BLControllerExtended instance = new BLControllerExtended();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BLControllerExtended()
        {
        }

        private BLControllerExtended()
        {
        }

        public static BLControllerExtended Instance
        {
            get
            {
                return instance;
            }
        }


    }
}