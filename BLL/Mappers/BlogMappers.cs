using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface;
using Bll.Interface.Entities;
using BLL;
using DalToWeb.DTO;

namespace Bll.Mappers
{
    public static class BlogMappers
    {
        public static BlogEntity ToBllBlog(this DalBlog dalBlog)
        {
            return new BlogEntity()
            {
                Id = dalBlog.Id,
                DateAdded = dalBlog.TimeAdded,
                Name = dalBlog.Title,
                UserId = dalBlog.UserId
            };
        }

        public static DalBlog ToDalBlog(this BlogEntity blogEntity)
        {
            return new DalBlog()
            {
                Id = blogEntity.Id,
                TimeAdded = blogEntity.DateAdded,
                Title = blogEntity.Name,
                UserId = blogEntity.UserId
            };
        }
    }
}
