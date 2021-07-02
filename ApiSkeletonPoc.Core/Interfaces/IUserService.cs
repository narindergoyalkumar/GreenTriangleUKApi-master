using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IUserService
    {
        UserModel Authenticate(string username, string password);
        BaseResponseModel GetAll();
        UserModel GetById(Guid id);
        BaseResponseModel Create(UserModel user, string password);
        void Update(UserModel user, string password = null);
        BaseResponseModel Delete(Guid id);
        bool UpdateEmail(Guid userId, string email);
    }
}
