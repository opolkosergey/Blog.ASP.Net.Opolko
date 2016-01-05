using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DalToWeb.DTO;
using DalToWeb.Interfacies;
using DalToWeb.Repositories;

namespace DalToWeb.Concrete
{
    public class BlogRepository : IBlogRepository
    {
        private readonly UserContext _context = new UserContext();
        public IEnumerable<DalBlog> GetAll()
        {
            return _context.Set<Blog>().Select(blog => new DalBlog()
            {
                Id = blog.Id,
                Title = blog.Title,
                TimeAdded = blog.TimeAdded,
                UserId = blog.UserId
            });
        }

        public DalBlog GetById(int key)
        {
            var blog = _context.Blogs.Find(key);
            return new DalBlog()
            {
                Id = blog.Id,
                TimeAdded = blog.TimeAdded,
                Title = blog.Title,
                UserId = blog.UserId
            };
        }

        public DalBlog GetByPredicate(Expression<Func<DalBlog, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalBlog e)
        {
            var blog = new Blog()
            {
                Title = e.Title,
                TimeAdded = DateTime.Now,
                UserId = e.UserId
            };
            _context.Set<Blog>().Add(blog);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DalBlog entity)
        {
            throw new NotImplementedException();
        }
    }
}
