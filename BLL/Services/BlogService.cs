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
    public class BlogService :IBlogService
    {
        private readonly IUnitOfWork uow;
        private readonly IBlogRepository blogRepository;

        public BlogService(IUnitOfWork uow, IBlogRepository repository)
        {
            this.uow = uow;
            this.blogRepository = repository;
        }


        public BlogEntity GetBlogEntity(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogEntity> GetAllBlogEntities()
        {
            return blogRepository.GetAll().Select(b => b.ToBllBlog());
        }

        public void CreateBlog(BlogEntity blog)
        {
            blogRepository.Create(blog.ToDalBlog());
            uow.Commit();
        }

        public void DeleteBlog(BlogEntity blog)
        {
            throw new NotImplementedException();
        }
    }
}
