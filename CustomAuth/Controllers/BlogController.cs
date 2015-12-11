using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DalToWeb;

namespace CustomAuth.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            return View();
        }
    }
}