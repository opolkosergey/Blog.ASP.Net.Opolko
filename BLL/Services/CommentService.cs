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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork uow;
        private readonly ICommentRepository repository;

        public CommentService(IUnitOfWork uow, ICommentRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
        }
        public CommentEntity GetCommentEntity(int id)
        {
            var commentEntity = repository.GetById(id);
            return (commentEntity == null) ? null : commentEntity.ToBllComment();
        }

        public IEnumerable<CommentEntity> GetAllCommentEntities()
        {
            return repository.GetAll().Select(c => c.ToBllComment());
        }

        public int GetLastId() => repository.GetLastId();

        public IEnumerable<CommentEntity> GetAllCommentEntities(int articleId)
        {
            return repository.GetAll().Where(a => a.ArticleId == articleId).Select(c => c.ToBllComment());
        }

        public void CreateComment(CommentEntity comment)
        {
            repository.Create(comment.ToDalComment());
            uow.Commit();
        }

        public void DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(CommentEntity comment)
        {
            throw new NotImplementedException();
        }
    }
}
