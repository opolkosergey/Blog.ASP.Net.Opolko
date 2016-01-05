using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface.Services
{
    public interface ICommentService
    {
        CommentEntity GetCommentEntity(int id);
        IEnumerable<CommentEntity> GetAllCommentEntities();
        IEnumerable<CommentEntity> GetAllCommentEntities(int articleId);
        int GetLastId();
        void CreateComment(CommentEntity comment);
        void DeleteComment(int id);
        void UpdateComment(CommentEntity comment);
    }
}
