using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Entities;
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
                    TempData["CurrentArticle"] = id;
                    comment.Id = _commentService.GetLastId();
                    return Json(ParseHelper.ParseComment(comment));
                }
            }
            return RedirectToAction("Details", "Article", new {id = id});
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public ActionResult EditComment(string data)
        {
            var array = data.Split('~');
            int id = int.Parse(array[0]);
            string text = array[1];
            _commentService.UpdateComment(new CommentEntity {Id = id,CommentText = text});
            return Content("ok");
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public ActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return Content("ok");
        }
    }
}