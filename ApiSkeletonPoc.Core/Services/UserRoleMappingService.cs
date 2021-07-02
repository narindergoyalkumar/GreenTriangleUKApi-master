using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class UserRoleMappingService : IUserRoleMappingService
    {
        private readonly IBaseService<UserRoleMapping> _baseService;
        public UserRoleMappingService(IBaseService<UserRoleMapping> baseService)
        {
            _baseService = baseService;
        }

        public void MapUserRole(UserRoleMappingModel userRoleMappingModel)
        {
            _baseService.AddOrUpdate(Mapper.MapUserRoleMappingModelWithUserRoleMapping(userRoleMappingModel), 0);
        }

        public void RemoveUserRoleMapping(int userId)
        {
            _baseService.Remove(userId);
        }
    }
}
