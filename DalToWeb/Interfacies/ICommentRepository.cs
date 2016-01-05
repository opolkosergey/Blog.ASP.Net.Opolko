using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.DTO;

namespace DalToWeb.Interfacies
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        int GetLastId();
    }
}
