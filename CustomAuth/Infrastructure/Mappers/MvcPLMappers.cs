using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bll.Interface;
using BLL;
using CustomAuth.ViewModels;
using DalToWeb.Repositories;
using Role = CustomAuth.ViewModels.Role;

namespace CustomAuth.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.UserName,
                Role = (Role)userEntity.RoleId,
                CreationDate = userEntity.DateAdded,
               
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                UserName = userViewModel.Email,
                RoleId = (int)userViewModel.Role,
                DateAdded = userViewModel.CreationDate
            };
        }
    }
}