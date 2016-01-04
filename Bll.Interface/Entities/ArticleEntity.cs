using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interface.Entities
{
    public class ArticleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string Tags { get; set; }
        public DateTime DateAdded { get; set; }
        public int BlogId { get; set; }
    }
}
