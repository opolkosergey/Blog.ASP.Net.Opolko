using System;
using System.Collections.Generic;
using DalToWeb.ORM;

namespace CustomAuth.ViewModels
{
    //Конкретно о статье
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime TimeAdded { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Tag> Tags { get; set; } 
    }
}