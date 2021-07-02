using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        private readonly IUserRoleMappingService _userRoleMappingService;
        public TokenOption TokenOption { get; }
        private readonly IClientService _clientService;
        public UsersController(
           IUserService userService,
           IOptions<AppSettings> appSettings, IOptions<TokenOption> optionsAccessor, IUserRoleMappingService userRoleMappingService, IClientService clientService)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            TokenOption = optionsAccessor.Value;
            _userRoleMappingService = userRoleMappingService;
            _clientService = clientService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(Security.GenerateJwtToken(user.UserName, user, TokenOption, user.Role));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            //if (!_clientService.IsClientExists(model.ClientId.Value))
            //{
            //    return BadRequest(new { message = "Client doesn't exist." });
            //};
            var userModel = new UserModel
            {
                ClientId = model.ClientId==0?null:model.ClientId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username
            };
            // create user
            var response = _userService.Create(userModel, model.Password);
            if (response.Response != null)
            {
                var _user = (UserModel)response.Response;
                _userRoleMappingService.MapUserRole(new UserRoleMappingModel { UserId = _user.Id, UserRoleId = model.RoleId });
            }
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_userService.Delete(id));
        }
    }
}