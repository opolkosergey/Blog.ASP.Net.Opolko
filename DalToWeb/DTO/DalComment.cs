using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace DalToWeb.DTO
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; } 
        public DateTime DateAdded { get; set; }
        public string TextComment { get; set; }
    }
}
