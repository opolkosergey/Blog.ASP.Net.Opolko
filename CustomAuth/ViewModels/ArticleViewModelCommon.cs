using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DalToWeb.ORM;

namespace CustomAuth.ViewModels
{
    public class ArticleViewModelCommon
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public int Viewed { get; set; }
        public int CommentCount { get; set; }
    }
}