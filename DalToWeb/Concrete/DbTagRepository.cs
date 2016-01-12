using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.Interfacies;
using DalToWeb.ORM;
using DalToWeb.Repositories;

namespace DalToWeb.Concrete
{
    public class DbTagRepository : ITagRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        public ICollection<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }
    }
}
