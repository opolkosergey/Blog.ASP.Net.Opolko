using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;
using DalToWeb.DTO;

namespace Bll.Mappers
{
    public static class CommentMappers
    {
        public static CommentEntity ToBllComment(this DalComment comment)
        {
            return new CommentEntity()
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                CommentText = comment.TextComment,
                DateAdded = comment.DateAdded,
                UserId = comment.UserId
            };
        }

        public static DalComment ToDalComment(this CommentEntity comment)
        {
            return new DalComment()
            {
                ArticleId = comment.ArticleId,
                TextComment = comment.CommentText,
                DateAdded = comment.DateAdded,
                UserId = comment.UserId
            };
        }
    }
}
