using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CustomAuth.Pagination;

namespace CustomAuth.ViewModels
{
    public class BlogsViewModel
    {
        public int UserId { get; set; }
        public IEnumerable<BlogViewModel> BlogViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class BlogViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Blog title")]
        public string Title { get; set; }

        [Display(Name = "Date created")]
        public DateTime CreationDate { get; set; }

        public int ArticleCount { get; set; }
        public string UserName { get; set; }
    }
}