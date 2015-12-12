using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.DTO;
using DalToWeb.Interfacies;
using DalToWeb.ORM;
using DalToWeb.Repositories;

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
               Title = art.Title
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
                ImagePath = e.ImagePath
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
            throw new NotImplementedException();
        }
    }
}
