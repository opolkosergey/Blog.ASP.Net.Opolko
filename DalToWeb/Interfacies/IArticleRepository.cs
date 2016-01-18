using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.DTO;

namespace DalToWeb.Interfacies
{
    public interface IArticleRepository : IRepository<DalArticle>
    {
        void IncViews(int id);
        IEnumerable<DalArticle> SearchBySubstring(string subsrting);
        IEnumerable<DalArticle> GetLastArticles(int page, int count);
    }
}
