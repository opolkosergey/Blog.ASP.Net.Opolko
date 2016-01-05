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
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;

        public BlogController(IBlogService blogService, IUserService userservice, IArticleService articleService)
        {
            this._blogService = blogService;
            _userService = userservice;
            _articleService = articleService;
        }

        [HttpGet]
        public ActionResult CreateBlog() => View();


        [HttpPost]
        public ActionResult CreateBlog(UserBlogModel blog)
        {
            var currentUser = _userService.GetUserEntity(User.Identity.Name);
            int userId = currentUser.Id;
            var anyBlogs = _blogService
                .GetAllBlogEntities()
                .Where(b => b.Name == blog.Title);

            if (anyBlogs.FirstOrDefault(b => b.UserId == userId) != null)
            {
                ModelState.AddModelError("", "У вас уже есть блог с таким названием");
                return View(blog);
            }

            if (ModelState.IsValid)
            {
                _blogService.CreateBlog(blog.ToBllBlog(userId));
                return RedirectToAction("MyBlogs", "Blog");
            }
             
            return View();
        }

        public ActionResult MyBlogs(int page = 1)
        {
            var currentUser = _userService.GetUserEntity(User.Identity.Name);
            int userId = currentUser.Id;
            var blogs = _blogService
                .GetAllBlogEntities()
                .Where(b => b.UserId == userId)
                .Select(bl => bl.ToMvcBlog())
                .ToList();

            var models = blogs
                .Skip((page - 1)*5)
                .Take(5)
                .ToList();

            foreach (var m in models)
            {
                m.ArticleCount = _articleService
                    .GetAllArticleEntities(m.Id)
                    .Count();
            }
            PageInfo pageInfo = new PageInfo {PageNumber = page, PageSize = 5, TotalItems = blogs.Count()};
            var bvm = new BlogsViewModel {PageInfo = pageInfo, BlogViewModels = models};

            return View(bvm);
        }

        public ActionResult Details(string id, int page = 1)
        {
            int parsedId;
            if (int.TryParse(id, out parsedId) == false)
                return RedirectToAction("Error", "Home");

            var blog = _blogService.GetBlogEntity(parsedId);
            if (blog != null)
            {
                TempData["BlogId"] = blog.Id;
                ViewBag.IsModerating =
                    (blog.UserId == _userService.GetUserEntity(User.Identity.Name).Id) 
                    || User.IsInRole("Moderator") || User.IsInRole("Admin");

                ViewBag.Id = id;
                ViewBag.DateCreated = blog.DateAdded;
                ViewBag.BlogName = blog.Name;

                var articles = _articleService
                    .GetAllArticleEntities(parsedId)
                    .Select(a => a.ToMvcArticle())
                    .ToList();

                var models = articles
                    .Skip((page - 1)*15)
                    .Take(15)
                    .ToList();

                var pageInfo = new PageInfo {PageNumber = page, PageSize = 15, TotalItems = articles.Count()};
                var model = new ArticleViewModelPagination {ArticleViewModels = models, PageInfo = pageInfo};

                return View(model);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}