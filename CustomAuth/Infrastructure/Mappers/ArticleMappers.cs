using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface.Entities;
using CustomAuth.ViewModels;
using DalToWeb.Migrations;
using DalToWeb.ORM;

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
                ImagePath = imgPath,
                Tags = model.Tags
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

        public static ArticleViewModel ToMvcViewArticle(this ArticleEntity model, string authorName)
        {
            var articleViewModel = new ArticleViewModel()
            {
                Content = model.Content,
                Id = model.Id,
                ImagePath = model.ImagePath,
                TimeAdded = model.DateAdded,
                Title = model.Name,
                Author = authorName
            };
            if (!string.IsNullOrEmpty(model.Tags))
            {
                articleViewModel.Tags = new List<Tag>();
                var tags = model.Tags.Split(',');
                foreach (var tag in tags)
                    articleViewModel.Tags.Add(new Tag(tag));
            }
            return articleViewModel;
        }

        public static ArticleViewModelCommon ToMvcViewArticleCommon(this ArticleEntity model, string authorName)
        {
            return new ArticleViewModelCommon()
            {
                Author = authorName,
                Content = model.Content,
                ImagePath = model.ImagePath,
                Id = model.Id,
                Title = model.Name
            };
        }
    }
}