using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.ORM;

namespace DalToWeb
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeAdded { get; set; }
        public virtual int UserId { get; set; }
        public virtual ICollection<Article> Articles { get; set; } 
    }
}
