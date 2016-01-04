using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.ORM
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagField { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

        public Tag() { }
        public Tag(string str)
        {
            TagField = str;
        }
    }
}
