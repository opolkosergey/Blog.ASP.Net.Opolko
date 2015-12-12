using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.ViewModels;

namespace CustomAuth.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly IArticleService _articleService;

        public ArticleController(IUserService service, IBlogService blogService, IArticleService articleService)
        {
            _userService = service;
            _blogService = blogService;
            _articleService = articleService;
        }
        [HttpGet]
        public ActionResult CreateArticle()
        {
            var currentUser = _userService.GetUserEntity(User.Identity.Name);
            int userId = currentUser.Id;
            var myBlogs = _blogService
                .GetAllBlogEntities()
                .Where(b => b.UserId == userId)
                .ToList();

            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(myBlogs.Select(b => new SelectListItem() {Value = b.Id.ToString(), Text = b.Name}));

            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        public ActionResult CreateArticle(ArticleViewModelCreate article, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
                _articleService.CreateArticle(article.ToBllArticle());

            return RedirectToAction("CreateArticle");
        }
    }
}