﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface.Services
{
    public interface IArticleService
    {
        ArticleEntity GetArticleEntity(int id);
        IEnumerable<ArticleEntity> GetAllArticleEntities();
        IEnumerable<ArticleEntity> GetAllArticleEntities(int blogId);
        IEnumerable<ArticleEntity> FindArticlesBySubstring(string s);
        void IncrementViews(int id);
        void CreateArticle(ArticleEntity article);
        void DeleteArticle(int id);
        void UpdateArticle(ArticleEntity article);
    }
}
