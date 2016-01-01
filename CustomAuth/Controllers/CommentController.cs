using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface.Services;
using CustomAuth.Utils;
using CustomAuth.ViewModels;

namespace CustomAuth.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUserService _userService;

        public CommentController(IUserService service)
        {
            _userService = service;
        }
        [HttpPost]
        public ActionResult NewComment(string textComment)
        {
            var comment = new CommentModel
            {
                Author = User.Identity.Name,
                AvatarPath = _userService.GetUserEntity(User.Identity.Name).AvatarPath,
                Date = DateTime.Now,
                TextComment = textComment
            };

            if (Request.IsAjaxRequest())
                return Json(StringHelper.ParseComment(comment));
            else
                return Content(StringHelper.ParseComment(comment));
        }
    }
}