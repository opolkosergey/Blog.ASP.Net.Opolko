using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.ORM
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeAdded { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public virtual int BlogId { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
