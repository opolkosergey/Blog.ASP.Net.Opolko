using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuth.ViewModels
{
    //Кратко о статье
    public class ArticleViewModelDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CountComments { get; set; }
        public int CountViews { get; set; }
    }
}