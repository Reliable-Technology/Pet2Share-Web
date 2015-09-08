using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet2share.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ViewResult Index()
        {
            ViewBag.Message = "";

            return View();
        }

    }
}
