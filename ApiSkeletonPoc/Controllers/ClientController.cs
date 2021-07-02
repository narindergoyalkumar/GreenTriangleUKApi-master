using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ICustomFieldService _customFieldService;
        public ClientController(IClientService clientService, ICustomFieldService customFieldService)
        {
            _clientService = clientService;
            _customFieldService = customFieldService;
        }
        // GET: api/Client
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientService.GetAll());
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_clientService.Get(id));
        }

        // POST: api/Client
        [HttpPost]
        public IActionResult Post([FromBody] ClientModel clientModel)
        {
            if (clientModel == null)
            {
                return BadRequest();
            }
            return Ok(_clientService.Add(clientModel));
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ClientModel clientModel)
        {
            if (clientModel == null)
            {
                return BadRequest();
            }
            clientModel.ClientId = id;
            return Ok(_clientService.Update(id, clientModel));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_clientService.Remove(id));
        }

        [HttpGet("custom-fields")]
        public IActionResult GetCustomFields()
        {
            return Ok(_customFieldService.GetByClient(Convert.ToInt32(User.GetClientId())));
        }
    }
}
    