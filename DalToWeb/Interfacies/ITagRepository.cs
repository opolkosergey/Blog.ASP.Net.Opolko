using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.ORM;

namespace DalToWeb.Interfacies
{
    public interface ITagRepository
    {
        ICollection<Tag> GetAll();
    }
}
