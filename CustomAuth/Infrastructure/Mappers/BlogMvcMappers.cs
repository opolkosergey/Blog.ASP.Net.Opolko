using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface;
using Bll.Interface.Entities;
using CustomAuth.ViewModels;

namespace CustomAuth.Infrastructure.Mappers
{
    public static class BlogMvcMappers
    {
        public static BlogViewModel ToMvcBlog(this BlogEntity blogEntity)
        {
            return new BlogViewModel()
            {
                Id = blogEntity.Id,
                Title = blogEntity.Name,
                CreationDate = blogEntity.DateAdded
            };
        }

        public static BlogEntity ToBllBlog(this UserBlogModel userBlogViewModel, int id)
        {
            return new BlogEntity()
            {
                Id = userBlogViewModel.Id,
                Name = userBlogViewModel.Title,
                UserId = id
            };
        }
    }
}