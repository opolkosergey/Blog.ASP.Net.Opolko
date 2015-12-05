using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DalToWeb;
using DalToWeb.Interfacies;
using DalToWeb.Repositories;

namespace CustomAuth.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserRepository UserRepository
            => (IUserRepository) DependencyResolver.Current.GetService(typeof (IUserRepository));

        public IRoleRepository RoleRepository
            => (IRoleRepository) DependencyResolver.Current.GetService(typeof (IRoleRepository));

        public override bool IsUserInRole(string email, string roleName)
        {

            var user = UserRepository.GetAll().FirstOrDefault(u => u.Name == email);

            if (user == null) return false;

            Role userRole = RoleRepository.GetById(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            using (var context = new UserContext())
            {
                var roles = new string[] {};
                var user = context.Users.FirstOrDefault(u => u.Email == email);

                if (user == null) return roles;

                var userRole = user.Role;

                if (userRole != null)
                {
                    roles = new string[] {userRole.Name};
                }
                return roles;
            }
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new Role() {Name = roleName};
            using (var context = new UserContext())
            {
                context.Roles.Add(newRole);
                context.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}