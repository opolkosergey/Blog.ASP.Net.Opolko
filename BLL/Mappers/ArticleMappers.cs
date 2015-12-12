using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;
using DalToWeb.DTO;

namespace Bll.Mappers
{
    public static class ArticleMappers
    {
        public static ArticleEntity ToBllArticle(this DalArticle dalArticle)
        {
            return new ArticleEntity()
            {
                Id = dalArticle.Id,
                DateAdded = dalArticle.TimeAdded,
                Name = dalArticle.Title,
                BlogId = dalArticle.BlogId,
                Content = dalArticle.Content,
                ImagePath = dalArticle.ImagePath
            };
        }

        public static DalArticle ToDalArticle(this ArticleEntity blogEntity)
        {
            return new DalArticle()
            {
                Id = blogEntity.Id,
                TimeAdded = blogEntity.DateAdded,
                Title = blogEntity.Name,
                BlogId = blogEntity.BlogId,
                Content = blogEntity.Content,
                ImagePath = blogEntity.ImagePath
            };
        }
    }
}
