using System.Collections.Generic;
using DalToWeb.Repositories;

namespace DalToWeb.Interfacies
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        bool CreateNewRole(Role role);
        Role GetById(int? roleId);
    }
}
