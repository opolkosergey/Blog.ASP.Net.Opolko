using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CustomAuth.ViewModels;

namespace CustomAuth.Utils
{
    public static class StringHelper
    {
        public static string ParseComment(CommentModel comment)
        {
            var sb = new StringBuilder();
            sb.Append(comment.Author);
            sb.Append('~');
            sb.Append(comment.AvatarPath);
            sb.Append('~');
            sb.Append(comment.Date);
            sb.Append('~');
            sb.Append(comment.TextComment);
            return sb.ToString();
        }
    }
}