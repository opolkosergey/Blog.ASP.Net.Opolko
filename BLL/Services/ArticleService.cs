using System;
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

        public IEnumerable<ArticleEntity> FindArticlesBySubstring(string s)
        {
            return repository.SearchBySubstring(s).Select(art => art.ToBllArticle());
        }

        public IEnumerable<ArticleEntity> TakeLastArticleEntities(int page, int count)
        {
            return repository.GetLastArticles(page, count).Select(art => art.ToBllArticle());
        }
        public void IncrementViews(int id)
        {
            repository.IncViews(id);
            uow.Commit();
        }

        public ArticleEntity GetArticleEntity(int id)
        {
            var articleEntity = repository.GetById(id);
            return (articleEntity == null) ? null : articleEntity.ToBllArticle();
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

        public void DeleteArticle(int id)
        {
            repository.Delete(id);
            uow.Commit();
        }

        public void UpdateArticle(ArticleEntity article)
        {
            repository.Update(article.ToDalArticle());
            uow.Commit();
        }
    }
}
