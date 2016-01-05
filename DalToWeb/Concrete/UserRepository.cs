using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL;
using DalToWeb.Interfacies;
using DalToWeb.Repositories;

namespace DalToWeb.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context = new UserContext();

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<User>().Select(user => new DalUser()
            {
                Id = user.Id,
                Name = user.Email,
                RoleId = (int) user.RoleId,
                Password = user.Password,
                DateAdded = user.CreationDate,
                PathAvatar = user.Avatar,
               
            });
        }

        public DalUser GetById(int key)
        {
            var ormuser = _context.Set<User>().FirstOrDefault(user => user.Id == key);
            return new DalUser()
            {
                Id = ormuser.Id,
                Name = ormuser.Email,
                PathAvatar = ormuser.Avatar
            };
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalUser e)
        {
            var user = new User()
            {
                Email = e.Name,
                RoleId = 2,
                CreationDate = DateTime.Now,
                Password = e.Password,
                Avatar = e.PathAvatar
            };
            _context.Set<User>().Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }

        public DalUser GetUserByName(string name)
        {
            var user = _context.Set<User>().FirstOrDefault(u => u.Email == name);
            return new DalUser()
            {
                Id = user.Id,
                Name = user.Email,
                PathAvatar = user.Avatar
            };
        }
    }
}