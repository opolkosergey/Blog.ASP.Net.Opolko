using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface;
using Bll.Interface.Services;
using Bll.Mappers;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Pagination;
using CustomAuth.ViewModels;
using DalToWeb.Interfacies;

namespace CustomAuth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       
        private readonly IUserService _service;
        private readonly IBlogService _blogservice;

        public HomeController( IUserService service, IBlogService blogService)
        {
            this._service = service;
            this._blogservice = blogService;
        }

        public ActionResult Error() => View();
        
        public ActionResult Index(int page = 1)
        {
            var users = _service
                .GetAllUserEntities()
                .Select(v => v.ToMvcUser())
                .ToList();  

            var models = users
                .Skip((page - 1) * 1)
                .Take(1)
                .ToList();
      
            foreach (var m in models)
            {
                m.BlogsCount = _blogservice
                    .GetAllBlogEntities()
                    .Count(b => b.UserId == m.Id);
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 1, TotalItems = users.Count() };
            BloggersViewModel bvm = new BloggersViewModel { PageInfo = pageInfo, UserViewModels = models };

            return View(bvm);
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