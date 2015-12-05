using System.Collections.Generic;
using BLL;
using DalToWeb.Repositories;

namespace DalToWeb.Interfacies
{
    public interface IUserRepository : IRepository<DalUser>//Add user repository methods!
    {
    }
}