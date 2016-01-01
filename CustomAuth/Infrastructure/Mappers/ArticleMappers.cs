using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using CustomAuth.ViewModels;

namespace CustomAuth.Infrastructure.Mappers
{
    public static class ArticleMappers
    {
        //public static BlogViewModel ToMvcBlog(this A blogEntity)
        //{
        //    return new BlogViewModel()
        //    {
        //        Id = blogEntity.Id,
        //        Title = blogEntity.Name,
        //        CreationDate = blogEntity.DateAdded
        //    };
        //}

        public static ArticleEntity ToBllArticle(this ArticleViewModelCreate model, string imgPath)
        {
            return new ArticleEntity()
            {
                Id = model.Id,
                Name = model.Title,
                Content = model.Content,
                BlogId = int.Parse(model.Blog),
                DateAdded = DateTime.Now,
                ImagePath = imgPath
            };
        }

        public static ArticleViewModelDetails ToMvcArticle(this ArticleEntity model)
        {
            return new ArticleViewModelDetails()
            {
                Id = model.Id,
                Title = model.Name
            };
        }

        public static ArticleViewModel ToMvcViewArticle(this ArticleEntity model)
        {
            return new ArticleViewModel()
            {
                Content = model.Content,
                Id = model.Id,
                ImagePath = model.ImagePath,
                TimeAdded = model.DateAdded,
                Title = model.Name
            };
        }
    }
}