using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace DalToWeb.DTO
{
    public class DalBlog : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeAdded { get; set; }
        public int UserId { get; set; }
    }
}
