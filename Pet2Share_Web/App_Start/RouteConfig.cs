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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PicUplod",
                url: "Upload/UploadImage/{id}/{isUser}/{isCover}",
                defaults: new { controller = "Upload", action = "UploadImage", id = UrlParameter.Optional, isUser = UrlParameter.Optional, isCover = UrlParameter.Optional }
            );

        }
    }
}