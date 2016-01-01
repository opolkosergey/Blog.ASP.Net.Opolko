using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuth.ViewModels
{
    public class CommentModel
    {
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string AvatarPath { get; set; }
        public string TextComment { get; set; }

        public CommentModel() { }
    }
}