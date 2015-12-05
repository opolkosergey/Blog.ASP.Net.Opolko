using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.ViewModels;
using DalToWeb.Interfacies;

namespace CustomAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserRepository _repository;

        public HomeController(IUserRepository repository)
        {
            this._repository = repository;
        }
        
        public ActionResult Index()
        {
            var model = _repository.GetAll().Select(u => u.ToMvcUser());                
            
            return View(model);
        }

        public ActionResult About()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                ViewBag.AuthType = User.Identity.AuthenticationType;
            }
            ViewBag.Login = User.Identity.Name;
            ViewBag.IsAdminInRole = User.IsInRole("Administrator") ?
                "You have administrator rights." : "You do not have administrator rights.";

            return View();
            //HttpContext.Profile["FirstName"] = "Вася";
            //HttpContext.Profile["LastName"] = "Иванов";
            //HttpContext.Profile.SetPropertyValue("Age",23);
            //Response.Write(HttpContext.Profile.GetPropertyValue("FirstName"));
            //Response.Write(HttpContext.Profile.GetPropertyValue("LastName"));
        }

       // [Authorize(Roles = "Administrator")]
       // public ActionResult UsersEdit()
       // {
            //var model = _repository.GetAllUsers().Select(u => new UserViewModel
            //{
            //    Email = u.Email,
            //    CreationDate = u.CreationDate,
            //    Role = u.Role.Name
            //});

          //  return System.Web.UI.WebControls.View(null);
       // }
    }
}