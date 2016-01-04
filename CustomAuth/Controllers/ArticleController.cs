using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Utils;
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
            {
                var str = new StringBuilder();
                if (img != null)
                    str.Append(FileHelper.SaveFileToDisk(img, Server.MapPath("~/")));

                _articleService.CreateArticle(article.ToBllArticle(str.ToString()));
                return RedirectToAction("Index","Home");
            }
            
            return RedirectToAction("CreateArticle");
        }

        public ActionResult Details(string id)
        {
            int parsedId;
            if (int.TryParse(id, out parsedId) == false)
                return RedirectToAction("Error", "Home");

            var article = _articleService
                .GetArticleEntity(parsedId);

            if (article != null)
            {
                var model = article.ToMvcViewArticle();
                var blog = _blogService
                    .GetAllBlogEntities()
                    .FirstOrDefault(b => b.Id == article.BlogId);
                var authorName = _userService.GetUserEntity(blog.UserId).UserName;
                model.Author = authorName;

                return View(model);    
            }
            return RedirectToAction("Error", "Home");
        }
    }
}