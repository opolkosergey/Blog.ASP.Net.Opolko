using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomAuth.Pagination;

namespace CustomAuth.ViewModels
{
    public class BloggersViewModel
    {
        public IEnumerable<UserViewModel> UserViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}