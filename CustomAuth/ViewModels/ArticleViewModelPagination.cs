using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomAuth.Pagination;

namespace CustomAuth.ViewModels
{
    public class ArticleViewModelPagination
    {
        public IEnumerable<ArticleViewModelDetails> ArticleViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}