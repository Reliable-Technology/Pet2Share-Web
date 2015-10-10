using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

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

                if (temp.Count() > 2)
                {
                    return temp[2];
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

                if (temp.Count() > 3)
                {
                    return temp[3];
                    //return GetValidImage("HomeFiles/Home_" + Pet2Share_Web.BL.BLAuth.Instance.GetUserID() + @"/" + temp[5]);
                }
            }
            return "/Images/NO_USER.png";
        }
        public string GetValidImage(object virtualFilePath, bool UseLabeled_NoUser_AsFallbackImage = false)
        {
            string fallbackImage = "/Images/NO_USER.png";
            if (UseLabeled_NoUser_AsFallbackImage)
            {
                fallbackImage = "/Images/NO_USER2.png";
            }

            if (virtualFilePath == null)
            {
                return fallbackImage;
            }
            else if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/" + virtualFilePath.ToString())))
            {
                return "~/" + virtualFilePath.ToString();
            }
            else
            {
                return fallbackImage;
            }
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
}