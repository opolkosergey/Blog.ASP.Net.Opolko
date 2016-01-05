using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interface.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime DateAdded { get; set; }
        public int ArticleId { get; set; }
    }
}
