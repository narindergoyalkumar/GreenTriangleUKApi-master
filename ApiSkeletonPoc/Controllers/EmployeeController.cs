using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.RequestModels;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IVisitsService _visitsService;

        public EmployeeController(IEmployeeService employeeService, IVisitsService visitsService)
        {
            _employeeService = employeeService;
            _visitsService = visitsService;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeService.GetAll(Convert.ToInt32(User.GetClientId())));
        }

        // GET api/Employee/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound("Employee can not found");
            }

            return Ok(employee);
        }

        //GET api/employee/visits/employeeId
        [HttpGet("{employeeId}/visits")]
        public IActionResult GetEmployeeVisit(int employeeId)
        {
            return Ok(_visitsService.GetByEmployeeId(employeeId));
        }

        // POST api/Employee
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeModel model)
        {
            model.ClientId = Convert.ToInt32(User.GetClientId());
            return Ok(_employeeService.Add(model));
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EmployeeModel model)
        {
            model.EmployeeId = id;
            return Ok(_employeeService.Update(model));
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_employeeService.Remove(id));
        }
    }
}
