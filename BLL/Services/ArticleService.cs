﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;
using Bll.Interface.Services;
using Bll.Mappers;
using DalToWeb.Interfacies;
using DalToWeb.Repositories;

namespace Bll.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork uow;
        private readonly IArticleRepository repository;

        public ArticleService(IUnitOfWork uow, IArticleRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public ArticleEntity GetArticleEntity(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleEntity> GetAllArticleEntities()
        {
            return repository.GetAll().Select(a => a.ToBllArticle());
        }

        public IEnumerable<ArticleEntity> GetAllArticleEntities(int blogId)
        {
            return repository.GetAll().Where(a => a.BlogId == blogId).Select(art => art.ToBllArticle());
        }

        public void CreateArticle(ArticleEntity article)
        {
            repository.Create(article.ToDalArticle());
            uow.Commit();
        }

        public void DeleteArticle(ArticleEntity article)
        {
            //
        }
    }
}