using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Entities;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Utils;
using CustomAuth.ViewModels;
using DalToWeb.ORM;

namespace CustomAuth.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        

        public ArticleController(IUserService service, IBlogService blogService,
            IArticleService articleService, ICommentService commentService)
        {
            _userService = service;
            _blogService = blogService;
            _articleService = articleService;
            _commentService = commentService;
        }
        
        [Authorize]
        public ActionResult Index(int page = 1)
        {
            var articles = _articleService.GetAllArticleEntities().ToList();
            articles.Reverse();
            
            if (page == 1)
            {
                var arts = articles.Take(10).ToList();
                var model = AddAuthors(arts);
                return View(model);
            }

            var nextArts = articles.Skip((page - 1) * 10).Take(10).ToList();
            var itsAll = (nextArts.Count == 10) ? "no" : "yes";
            var sb  = new StringBuilder();
            sb.Append(itsAll);
            if (nextArts.Count != 0)
                sb.Append('+');
            var newArtsList = AddAuthors(nextArts);
            sb.Append(newArtsList.ParseArticle());

            return Content(sb.ToString());
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
            list.AddRange(myBlogs.Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.Name }));

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
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Details(string id)
        {
            var art = GetArticle(id);
            if(art == null)
                return RedirectToAction("Error", "Home", new { error = $"article with id = {id} not found" });
            _articleService.IncrementViews(art.Id);
            art.Comments = new List<CommentModel>();
            art.Views++;
            var comments = _commentService.GetAllCommentEntities(art.Id).ToList();
            foreach (var comment in comments)
            {
                var user = _userService.GetUserEntity(comment.UserId);
                var c = new CommentModel()
                {
                    Id = comment.Id,
                    ArticleId = comment.ArticleId,
                    TextComment = comment.CommentText,
                    Date = comment.DateAdded,
                    Author = user.UserName,
                    AvatarPath = user.AvatarPath
                };
                art.Comments.Add(c);
            }
            return View(art);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var art = GetArticle(id);
            if (art == null)
                return RedirectToAction("Error", "Home",new { error = $"article with id = {id} not found" });
            TempData["Tags"] = art.Tags;
            return View(art);
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            model.Tags = (ICollection<Tag>)TempData["Tags"];
            _articleService.UpdateArticle(model.ToArticleEntity());
            return RedirectToAction("Blogs", "Blog");
        }

        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult Delete(int id)
        {
            var filePath = _articleService.GetArticleEntity(id).ImagePath;
            _articleService.DeleteArticle(id);
            if(filePath != null)
                FileHelper.RemoveFileFromDisk(Server.MapPath("~/"),filePath);

            if (Request.IsAjaxRequest())
                return Content("ok");

            return RedirectToAction("Details", "Blog", new {id = TempData["BlogId"].ToString()});
        }
        #region Private methods
        private List<ArticleViewModelCommon> AddAuthors(List<ArticleEntity> list)
        {
            var arts = new List<ArticleViewModelCommon>();
            foreach (var art in list)
            {
                var userId = _blogService.GetBlogEntity(art.BlogId).UserId;
                var authorName = _userService.GetUserEntity(userId).UserName;
                arts.Add(art.ToMvcViewArticleCommon(authorName));
            }
            return arts;
        }

        private ArticleViewModel GetArticle(string id)
        {
            int parsedId;
            if (int.TryParse(id, out parsedId) == false)
                return null;

            TempData["CurrentArticle"] = parsedId;

            var article = _articleService.GetArticleEntity(parsedId);

            if (article != null)
            {
                var blog = _blogService
                    .GetAllBlogEntities()
                    .FirstOrDefault(b => b.Id == article.BlogId);
                var authorName = _userService.GetUserEntity(blog.UserId).UserName;
                var model = article.ToMvcViewArticle(authorName);
                return model;
            }
            return null;
        }
        #endregion
    }
}