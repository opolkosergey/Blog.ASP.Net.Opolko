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
    public class CommentRepository : ICommentRepository
    {
        private readonly UserContext _context = new UserContext();
        public IEnumerable<DalComment> GetAll()
        {
            return _context.Set<Comment>().Select(c => new DalComment()
            {
                Id = c.Id,
                DateAdded = c.Date,
                TextComment = c.TextComment,
                UserId = c.UserId,
                ArticleId = c.ArticleId
            });
        }

        public DalComment GetById(int key)
        {
            var comment = _context.Set<Comment>().Find(key);
            return new DalComment()
            {
                Id = comment.Id,
                DateAdded = comment.Date,
                TextComment = comment.TextComment,
                UserId = comment.UserId,
                ArticleId = comment.ArticleId
            };
        }

        public int GetLastId() => _context.Comments.Max(c => c.Id);

        public DalComment GetByPredicate(Expression<Func<DalComment, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalComment e)
        {
            var art = _context.Set<Article>().Find(e.ArticleId);
            var c = new Comment()
            {
                TextComment = e.TextComment,
                Date = e.DateAdded,
                UserId = e.UserId
            };
            art.Comments.Add(c);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DalComment entity)
        {
            var propertyText = typeof(Comment).GetProperty("TextComment");
            var comment = _context.Set<Comment>().Find(entity.Id);
            propertyText.SetValue(comment, entity.TextComment);
            _context.SaveChanges();
        }
    }
}
