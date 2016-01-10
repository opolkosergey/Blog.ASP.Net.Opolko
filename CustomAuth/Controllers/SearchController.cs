using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Pagination;
using CustomAuth.ViewModels;
using DalToWeb.Interfacies;
using WebGrease.Css.Extensions;
using ListExtensions = WebGrease.Css.Extensions.ListExtensions;

namespace CustomAuth.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly IArticleService _articleService;
        private readonly ITagRepository _tagRepository;

        public SearchController(IArticleService articleService, ITagRepository tagRepository,
            IBlogService blogService, IUserService userService)
        {
            _articleService = articleService;
            _tagRepository = tagRepository;
            _blogService = blogService;
            _userService = userService;
        }
        public ActionResult ByTag(string tag)
        {
            var tags = _tagRepository.GetAll().Where(t => t.TagField == tag).ToList();
            var listId = (from t in tags from art in t.Articles select art.Id).ToList();

            var articles = _articleService.GetAllArticleEntities().ToList();
            var model = new List<ArticleViewModelCommon>();
            foreach (var art in articles)
                foreach (var id in listId)
                    if (art.Id == id)
                    {
                        var blog = _blogService.GetBlogEntity(art.BlogId);
                        var authorName = _userService.GetUserEntity(blog.UserId).UserName;
                        model.Add(art.ToMvcViewArticleCommon(authorName));
                    }
            return View(model);
        }

        public ActionResult ByText(string substring)
        {
            var arts = _articleService.FindArticlesBySubstring(substring).ToList();
            var model = new List<ArticleViewModelCommon>();
            foreach (var art in arts)
            {
                var blog = _blogService.GetBlogEntity(art.BlogId);
                var authorName = _userService.GetUserEntity(blog.UserId).UserName;
                model.Add(art.ToMvcViewArticleCommon(authorName));
            }
            return View(model);
        }

        public ActionResult Blogs(string blogTitle,int page = 1)
        {
            var blogs = _blogService
                .GetAllBlogEntities()
                .Where(b => b.Name == blogTitle)
                .ToList();

            var models = blogs
                .Skip((page - 1) * 10)
                .Take(10)
                .Select(bl => bl.ToMvcBlog())
                .ToList();

            foreach (var m in models)
            {
                var uId = _blogService.GetBlogEntity(m.Id).UserId;
                m.ArticleCount = _articleService.GetAllArticleEntities(m.Id).Count();
                m.UserName = _userService.GetUserEntity(uId).UserName;
            }
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 10, TotalItems = blogs.Count() };
            var bvm = new BlogsViewModel { PageInfo = pageInfo, BlogViewModels = models };
            ViewBag.Title = blogTitle;

            return View(bvm);
        }
    }
}