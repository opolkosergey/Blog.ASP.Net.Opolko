using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.DTO;
using DalToWeb.Interfacies;
using DalToWeb.ORM;
using DalToWeb.Repositories;
using DalToWeb;

namespace DalToWeb.Concrete
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly UserContext _context = new UserContext();
        public IEnumerable<DalArticle> GetAll()
        {
            return _context.Set<Article>().Select(art => new DalArticle()
            {
               TimeAdded = art.TimeAdded,
               BlogId = art.BlogId,
               Content = art.Content,
               Id = art.Id,
               ImagePath = art.ImagePath,
               Title = art.Title,
               Tags = art.Tags,
               Comments = art.Comments.Count,
               Viewed = art.Viewed
            });
        }

        public void IncViews(int id)
        {
            var propertyViewed = typeof(Article).GetProperty("Viewed");
            var article = _context.Set<Article>().Find(id);
            propertyViewed.SetValue(article, ++article.Viewed);
            _context.SaveChanges();
        }

        public IEnumerable<DalArticle> SearchBySubstring(string subsrting)
        {
            return _context.Set<Article>()
                .Where(a => a.Content.Contains(subsrting))
                .Select(art => new DalArticle()
            {
                TimeAdded = art.TimeAdded,
                BlogId = art.BlogId,
                Content = art.Content,
                Id = art.Id,
                ImagePath = art.ImagePath,
                Title = art.Title,
                Tags = art.Tags,
                Comments = art.Comments.Count,
                Viewed = art.Viewed
            });
        }

        public DalArticle GetById(int key)
        {
            var art = _context.Set<Article>().Find(key);
            return new DalArticle()
            {
                Id = art.Id,
                BlogId = art.BlogId,
                Content = art.Content,
                ImagePath = art.ImagePath,
                Title = art.Title,
                Tags = art.Tags,
                TimeAdded = art.TimeAdded,
                Comments = art.Comments.Count,
                Viewed = art.Viewed
            };
        }

        public DalArticle GetByPredicate(Expression<Func<DalArticle, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalArticle e)
        {
            var art = new Article()
            {
                Title = e.Title,
                TimeAdded = DateTime.Now,
                BlogId = e.BlogId,
                Content = e.Content,
                ImagePath = e.ImagePath,
                Tags = e.Tags
            };
            _context.Set<Article>().Add(art);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var article = _context.Set<Article>().Find(id);
            _context.Set<Article>().Remove(article);
            _context.SaveChanges();
        }

        public void Update(DalArticle entity)
        {
            var propertyContent = typeof (Article).GetProperty("Content");
            var article = _context.Set<Article>().Find(entity.Id);
            propertyContent.SetValue(article,entity.Content);
            _context.SaveChanges();
        }
    }
}
