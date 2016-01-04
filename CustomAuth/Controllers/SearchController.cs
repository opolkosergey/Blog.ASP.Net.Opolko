using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuth.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult ByTag(string tag)
        {
            return View();
        }
    }
}