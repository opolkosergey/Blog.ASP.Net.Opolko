using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CustomAuth.ViewModels;
using DalToWeb.ORM;
using WebGrease.Css.Extensions;

namespace CustomAuth.Utils
{
    public static class ParseHelper
    {
        public static string ParseComment(CommentModel comment)
        {
            var sb = new StringBuilder();
            sb.Append(comment.Id);
            sb.Append('~');
            sb.Append(comment.Author);
            sb.Append('~');
            sb.Append(comment.AvatarPath);
            sb.Append('~');
            sb.Append(comment.Date);
            sb.Append('~');
            sb.Append(comment.TextComment);
            return sb.ToString();
        }

        public static string ParseArticle(this List<ArticleViewModelCommon> list)
        {
            var sb = new StringBuilder();
            foreach (var art in list)
            {
                sb.Append(art.Id);
                sb.Append('~');
                sb.Append(art.Title);
                sb.Append('~');
                sb.Append(art.Author);
                sb.Append('~');
                if (string.IsNullOrEmpty(art.ImagePath))
                {
                    sb.Append("no image");
                }
                else
                {
                    sb.Append(art.ImagePath);
                }
                sb.Append('~');
                sb.Append(art.Date);
                sb.Append('~');
                sb.Append(art.Viewed);
                sb.Append('~');
                sb.Append(art.Content);
                sb.Append('~');
                sb.Append(art.CommentCount);
                sb.Append('+');
            }
            if(sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            
            return sb.ToString();
        }
    }
}