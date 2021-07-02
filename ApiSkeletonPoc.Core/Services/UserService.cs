using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseService<User> _baseService;
        private readonly IUserRoleMappingService _userRoleMappingService;
        public UserService(IBaseService<User> baseService, ILogService logService, IUserRoleMappingService userRoleMappingService)
        {
            _baseService = baseService;
            _userRoleMappingService = userRoleMappingService;
        }
        public UserModel Authenticate(string username, string password)
        {
            string[] navgationProps = { "UserRoleMapping", "UserRoleMapping.UserRole", "Client", "Client.ClientModuleMapping", "Client.ClientModuleMapping.Module" };
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _baseService.Where(a => a.UserName == username, navgationProps).SingleOrDefault();

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return Mapper.MapUserWithUserModel(user);
        }

        public BaseResponseModel Create(UserModel userModel, string password)
        {
            if (_baseService.Where(a => a.UserName == userModel.UserName).Any())
                return new BaseResponseModel
                {
                    IsSuccess = false,
                    Message = "User already exists.",
                    Response = null
                };
            var user = Mapper.MapUserModelWithUser(userModel);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Id = Guid.NewGuid();
            var _user = _baseService.AddOrUpdate(user, user.Id);
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = "User added successfully.",
                Response = Mapper.MapUserWithUserModel(_user)
            };
        }

        public BaseResponseModel Delete(Guid id)
        {
            var user = _baseService.Where(u => u.Id == id, "UserRoleMapping").FirstOrDefault();
            if (user != null)
            {
                if (user.UserRoleMapping.Any())
                {
                    foreach (var item in user.UserRoleMapping.ToList())
                    {
                        _userRoleMappingService.RemoveUserRoleMapping(item.UserRoleMappingId);
                    }
                }
                _baseService.Remove(id);
                return new BaseResponseModel { IsSuccess = true, Response = id, Message = "User deleted successfully." };
            }
            return new BaseResponseModel { IsSuccess = false, Response = null, Message = "User not found." };
        }

        public BaseResponseModel GetAll()
        {
            return new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = _baseService.GetAll().Select(u => Mapper.MapUserWithUserModel(u)).ToList() };
        }

        public UserModel GetById(Guid id)
        {
            var user = _baseService.GetById(id);
            return Mapper.MapUserWithUserModel(user);
        }

        public void Update(UserModel user, string password = null)
        {
            throw new NotImplementedException();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        public bool UpdateEmail(Guid userId, string email)
        {
            bool isEmailUpdated = false;
            var user = _baseService.GetById(userId);
            if (user == null)
            {
                return isEmailUpdated;
            }
            user.Email = email;
            _baseService.AddOrUpdate(user, userId);
            return isEmailUpdated;
        }
    }
}
