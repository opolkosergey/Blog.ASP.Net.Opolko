using System;
using System.Collections;
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

        [AllowAnonymous]
        public ActionResult Index(int page = 1)
        {
            var articles = _articleService
                .TakeLastArticleEntities(page, 10)
                .Select(a => a.ToMvcViewArticleCommon())
                .ToList(); ;

            if (page == 1)
                return View(articles);
            
            var itsAll = (articles.Count == 10) ? "no" : "yes";
            
            var dataForJson = new ArrayList {itsAll};
            dataForJson.AddRange(articles);
            return Json(dataForJson, JsonRequestBehavior.AllowGet);
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
            try
            {
                var art = GetArticle(id);
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
                        Date = comment.DateAdded.ToString(),
                        Author = user.UserName,
                        AvatarPath = user.AvatarPath
                    };
                    art.Comments.Add(c);
                }
                return View(art);
            }
            catch (ArgumentOutOfRangeException)
            {
                return RedirectToAction("Error", "Home");
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                var art = GetArticle(id);
                TempData["Tags"] = art.Tags;
                return View(art);
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

        [Authorize(Roles = "Moderator,Admin")]
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
            if (filePath != null)
                FileHelper.RemoveFileFromDisk(Server.MapPath("~/"), filePath);

            if (Request.IsAjaxRequest())
                return Content("ok");

            return RedirectToAction("Details", "Blog", new { id = TempData["BlogId"].ToString() });
        }
        #region Private methods
        private ArticleViewModel GetArticle(string id)
        {
            int parsedId;
            if (int.TryParse(id, out parsedId) == false)
                throw new NullReferenceException();

            if (parsedId < 0)
                throw new ArgumentOutOfRangeException();

            TempData["CurrentArticle"] = parsedId;
            return _articleService.GetArticleEntity(parsedId).ToMvcViewArticle();  
        }
        #endregion
    }
}