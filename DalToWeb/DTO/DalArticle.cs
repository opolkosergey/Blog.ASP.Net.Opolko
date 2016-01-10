using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DalToWeb.ORM;

namespace DalToWeb.DTO
{
    public class DalArticle : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Viewed { get; set; }
        public DateTime TimeAdded { get; set; }
        public string Content { get; set; }
        public int Comments { get; set; }
        public  ICollection<Tag> Tags { get; set; }
        public string ImagePath { get; set; }
        public int BlogId { get; set; }
    }
}
