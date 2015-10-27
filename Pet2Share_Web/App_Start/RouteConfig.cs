using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pet2Share_Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "searchPets",
             url: "SearchPets",
             defaults: new { controller = "PetConnection", action = "SearchPets" }
         );

            routes.MapRoute(
              name: "searchUser",
              url: "Search",
              defaults: new { controller = "Connection", action = "SearchUsers" }
          );
            routes.MapRoute(
             name: "Login",
             url: "Login",
             defaults: new { controller = "Index", action = "Login" }
         );
            routes.MapRoute(
           name: "Register",
           url: "Register",
           defaults: new { controller = "Index", action = "Register" }
       );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );

            //for pic upload 
            routes.MapRoute(
                name: "PicUplod",
                url: "Upload/UploadImage/{id}/{isUser}/{isCover}",
                defaults: new { controller = "Upload", action = "UploadImage", id = UrlParameter.Optional, isUser = UrlParameter.Optional, isCover = UrlParameter.Optional }
            );



            // //for error 
            // routes.MapRoute(
            //    name: "Error500",
            //    url: "ServerError/",
            //    defaults: new { controller = "Error", action = "Error500" }
            //);
            // routes.MapRoute(
            //    name: "Error404",
            //    url: "NotFound/",
            //    defaults: new { controller = "Error", action = "Error404" }
            //);


        }
    }
}