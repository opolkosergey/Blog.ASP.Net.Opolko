using System;
using System.Collections.Generic;
using System.Linq;
using DalToWeb.Interfacies;

namespace DalToWeb.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserContext _context = new UserContext();

        public bool CreateNewRole(Role role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetById(int? roleId)
        {
            return _context.Roles.Find(roleId);
        }
    }
}
