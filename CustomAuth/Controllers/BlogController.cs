using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Entities;
using Bll.Interface.Services;
using BLL;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Pagination;
using CustomAuth.ViewModels;
using DalToWeb;
using WebGrease.Css.Extensions;

namespace CustomAuth.Controllers
{
    [Authorize]
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
                return RedirectToAction("Blogs", "Blog");
            }
            return View();
        }

        public ActionResult Blogs(int uId = 0, int page = 1)
        {
            var currentUser = _userService.GetUserEntity(User.Identity.Name);
            int userId = (uId == 0) ? currentUser.Id : uId;
            var blogs = _blogService
                .GetAllBlogEntities()
                .Where(b => b.UserId == userId)
                .ToList();

            var models = blogs
                .Skip((page - 1) * 10)
                .Take(10)
                .Select(bl => bl.ToMvcBlog())
                .ToList();

            foreach (var m in models)
            {
                m.ArticleCount = _articleService
                    .GetAllArticleEntities(m.Id)
                    .Count();
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 10, TotalItems = blogs.Count() };
            var bvm = new BlogsViewModel { PageInfo = pageInfo, BlogViewModels = models };
            bvm.UserId = userId;

            return View(bvm);
        }

        public ActionResult Details(string id, int page = 1)
        {
            try
            {
                var blog = GetBlog(id);
                TempData["BlogId"] = blog.Id;
                ViewBag.IsModerating =
                    (blog.UserId == _userService.GetUserEntity(User.Identity.Name).Id)
                    || User.IsInRole("Moderator") || User.IsInRole("Admin");

                ViewBag.Id = id;
                ViewBag.DateCreated = blog.DateAdded;
                ViewBag.BlogName = blog.Name;

                var articles = _articleService
                    .GetAllArticleEntities(blog.Id)
                    .ToList();

                var models = articles
                    .Skip((page - 1) * 15)
                    .Take(15)
                    .Select(a => a.ToMvcArticle())
                    .ToList();

                var pageInfo = new PageInfo { PageNumber = page, PageSize = 15, TotalItems = articles.Count() };
                var model = new ArticleViewModelPagination { ArticleViewModels = models, PageInfo = pageInfo };

                return View(model);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Error", "Home");
            }
            catch (ArgumentOutOfRangeException)
            {
                return RedirectToAction("Error", "Home");
            }       
        }

        private BlogEntity GetBlog(string id)
        {
            int parsedId;
            if (int.TryParse(id, out parsedId) == false)
                throw new NullReferenceException();

            if (parsedId < 0)
                throw new ArgumentOutOfRangeException();

            return _blogService.GetBlogEntity(parsedId);
        }
    }
}