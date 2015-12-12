using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using BLL;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Pagination;
using CustomAuth.ViewModels;
using DalToWeb;
using WebGrease.Css.Extensions;

namespace CustomAuth.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _service;
        private readonly IUserService _userService;

        public BlogController(IBlogService service, IUserService userservice)
        {
            this._service = service;
            _userService = userservice;
        }
        [HttpGet]
        public ActionResult CreateBlog() => View();


        [HttpPost]
        public ActionResult CreateBlog(UserBlogModel blog)
        {
            var currentUser = _userService.GetUserEntity(User.Identity.Name);
            int userId = currentUser.Id;
            var anyBlogs = _service
                .GetAllBlogEntities()
                .Where(b => b.Name == blog.Title);

            if (anyBlogs.FirstOrDefault(b => b.UserId == userId) != null)
            {
                ModelState.AddModelError("", "У вас уже есть блог с таким названием");
                return View(blog);
            }

            if (ModelState.IsValid)
                _service.CreateBlog(blog.ToBllBlog(userId));

            return RedirectToAction("MyBlogs","Blog");
        }

        public ActionResult MyBlogs(int page =1)
        {
            var currentUser = _userService.GetUserEntity(User.Identity.Name);
            int userId = currentUser.Id;
            var blogs = _service
                .GetAllBlogEntities()
                .Where(b => b.UserId == userId)
                .Select(bl => bl.ToMvcBlog()).ToList();

            var models = blogs
                .Skip((page - 1) * 5)
                .Take(5);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 5, TotalItems = blogs.Count() };
            var bvm = new BlogsViewModel { PageInfo = pageInfo,BlogViewModels = models };

            return View(bvm);
        } 
    }
}