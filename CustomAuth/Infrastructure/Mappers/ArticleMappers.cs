using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Bll.Interface.Entities;
using CustomAuth.ViewModels;
using DalToWeb.Migrations;
using DalToWeb.ORM;

namespace CustomAuth.Infrastructure.Mappers
{
    public static class ArticleMappers
    {
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
                Title = model.Name,
                CountComments = model.Comments,
                CountViews = model.Viewed
            };
        }

        public static ArticleEntity ToArticleEntity(this ArticleViewModel model)
        {
            var art = new ArticleEntity()
            {
                Id = model.Id,
                BlogId = model.BlogId,
                Content = model.Content,
                DateAdded = model.TimeAdded,
                ImagePath = model.ImagePath,
                Name = model.Title
            };

            if (model.Tags.Count != 0)
            {
                var sb = new StringBuilder();
                foreach (var tag in model.Tags)
                {
                    sb.Append(tag.TagField);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);
                art.Tags = sb.ToString();
            }
            return art;
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
                Author = authorName,
                BlogId = model.BlogId,
                Views = model.Viewed
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
                Content = (model.Content.Length > 50) ? model.Content.Substring(0,50) + "..." : model.Content,
                ImagePath = model.ImagePath,
                Id = model.Id,
                Title = model.Name,
                CommentCount = model.Comments,
                Viewed = model.Viewed,
                Date = model.DateAdded
            };
        }
    }
}