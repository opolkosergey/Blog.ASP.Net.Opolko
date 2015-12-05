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
        public static UserViewModel ToMvcUser(this DalUser userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                Email = userEntity.Name,
                Role = (Role)userEntity.RoleId,
                CreationDate = userEntity.DateAdded
            };
        }

        public static DalUser ToBllUser(this UserViewModel userViewModel)
        {
            return new DalUser()
            {
                Id = userViewModel.Id,
                Name = userViewModel.Email,
                RoleId = (int)userViewModel.Role,
                DateAdded = userViewModel.CreationDate
            };
        }
    }
}