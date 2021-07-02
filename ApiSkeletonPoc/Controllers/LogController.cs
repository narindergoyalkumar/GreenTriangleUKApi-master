using ApiSkeletonPoc.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }
        // GET: api/Log
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_logService.GetAll());
        }
    }
}
