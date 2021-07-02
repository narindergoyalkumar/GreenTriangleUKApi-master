using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IUserRoleMappingService
    {
        void MapUserRole(UserRoleMappingModel userRoleMappingModel);
        void RemoveUserRoleMapping(int userId);
    }
}
