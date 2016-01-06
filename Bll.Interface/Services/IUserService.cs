using System.Collections.Generic;

namespace Bll.Interface.Services
{
    public interface IUserService
    {
        UserEntity GetUserEntity(int id);
        UserEntity GetUserEntity(string name);
        IEnumerable<UserEntity> GetAllUserEntities();
        void CreateUser(UserEntity user);
        void DeleteUser(UserEntity user);
        void UpdateRole(int id, int role);
        //etc.
    }
}
