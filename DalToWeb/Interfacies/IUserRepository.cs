using System.Collections.Generic;
using BLL;
using DalToWeb.Repositories;

namespace DalToWeb.Interfacies
{
    public interface IUserRepository : IRepository<DalUser>
    {
        void UpdateRole(int id, int roleId);
        DalUser GetUserByName(string name);
    }
}