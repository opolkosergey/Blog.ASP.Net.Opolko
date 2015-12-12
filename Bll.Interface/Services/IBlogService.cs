using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface.Services
{
    public interface IBlogService
    {
        BlogEntity GetBlogEntity(int id);
        IEnumerable<BlogEntity> GetAllBlogEntities();
        void CreateBlog(BlogEntity blog);
        void DeleteBlog(BlogEntity blog);
    }
}
