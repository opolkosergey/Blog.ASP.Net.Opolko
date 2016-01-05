﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Infrastructure.Mappers;
using CustomAuth.Utils;
using CustomAuth.ViewModels;

namespace CustomAuth.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public CommentController(IUserService service, ICommentService commentService)
        {
            _userService = service;
            _commentService = commentService;
        }
        [HttpPost]
        public ActionResult NewComment(string textComment)
        {
            int id = (int) TempData["CurrentArticle"];
            if (!string.IsNullOrEmpty(textComment))
            {
                var user = _userService.GetUserEntity(User.Identity.Name);
                var comment = new CommentModel
                {
                    Author = User.Identity.Name,
                    Date = DateTime.Now,
                    TextComment = textComment,
                    ArticleId = id,
                    AvatarPath = user.AvatarPath
                };
                _commentService.CreateComment(comment.ToCommentEntity(user.Id));

                if (Request.IsAjaxRequest())
                {
                    comment.Id = _commentService.GetLastId();
                    return Json(StringHelper.ParseComment(comment));
                }
            }
            return RedirectToAction("Details", "Article", new {id = id});
        }
    }
}