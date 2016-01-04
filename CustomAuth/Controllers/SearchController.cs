using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
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
                        var blog = _blogService
                                    .GetAllBlogEntities()
                                    .FirstOrDefault(b => b.Id == art.BlogId);
                        var authorName = _userService.GetUserEntity(blog.UserId).UserName;
                        model.Add(art.ToMvcViewArticleCommon(authorName));
                    }
            return View(model);
        }
    }
}