﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Utils;
using CustomAuth.ViewModels;
using DalToWeb.ORM;

namespace CustomAuth.Controllers
{
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
                return RedirectToAction("MyBlogs", "Blog");
            }

            return RedirectToAction("CreateArticle");
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

        public ActionResult Details(string id)
        {
            var art = GetArticle(id);
            if(art == null)
                return RedirectToAction("Error", "Home");
            art.Comments = new List<CommentModel>();
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
                return RedirectToAction("Error", "Home");
            TempData["Tags"] = art.Tags;
            return View(art);
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            model.Tags = (ICollection<Tag>)TempData["Tags"];
            _articleService.UpdateArticle(model.ToArticleEntity());
            return RedirectToAction("MyBlogs", "Blog");
        }

        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult Delete(int id)
        {
            _articleService.DeleteArticle(id);
            if (Request.IsAjaxRequest())
            {
                return Content("ok");
            }
            return RedirectToAction("Details", "Blog", new {id = TempData["BlogId"].ToString()});
        }
    }
}