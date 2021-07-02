using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientModuleController : ControllerBase
    {
        private readonly IClientModuleMappingService _clientModuleMappingService;
        public ClientModuleController(IClientModuleMappingService clientModuleMappingService)
        {
            _clientModuleMappingService = clientModuleMappingService;
        }
        [HttpGet]
        [Route("check-client-module-access")]
        public IActionResult CheckClientModuleSubscriptionAccess(int moduleId)
        {
            int clientId = Convert.ToInt32(User.GetClientId());
            return Ok(_clientModuleMappingService.IsClientSubscribedToModule(moduleId, clientId));
        }
        [HttpGet]
        [Route("modules")]
        public IActionResult GetModules()
        {
            return Ok(_clientModuleMappingService.GetModules());
        }
    }
}