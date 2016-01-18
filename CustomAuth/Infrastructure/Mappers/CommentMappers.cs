using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using CustomAuth.ViewModels;
using DalToWeb.ORM;

namespace CustomAuth.Infrastructure.Mappers
{
    public static class CommentMappers
    {
        public static CommentEntity ToCommentEntity(this CommentModel comment,int id)
        {
            return new CommentEntity()
            {
                ArticleId = comment.ArticleId,
                CommentText = comment.TextComment,
                DateAdded =  DateTime.Parse(comment.Date),
                UserId = id
            };
        }
    }
}