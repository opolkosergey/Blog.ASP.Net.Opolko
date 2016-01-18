using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuth.ViewModels
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string AvatarPath { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string TextComment { get; set; }
        public CommentModel() { }
    }
}