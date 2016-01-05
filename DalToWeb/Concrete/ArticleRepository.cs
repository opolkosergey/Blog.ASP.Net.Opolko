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
               Tags = art.Tags
            });
        }

        public DalArticle GetById(int key)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
