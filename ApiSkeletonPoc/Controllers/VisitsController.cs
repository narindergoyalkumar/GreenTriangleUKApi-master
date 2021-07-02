using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitsService _visitService;

        public VisitsController(IVisitsService visitService)
        {
            _visitService = visitService;
        }

        // GET: api/visits
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_visitService.GetAll());
        }

        // GET api/visits/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var visitDetails = _visitService.GetById(id);
            if (visitDetails == null)
            {
                return NotFound();
            }

            return Ok(visitDetails);
        }

        // POST api/visits
        [HttpPost]
        public IActionResult Post([FromBody] VisitModel model)
        {
            return Ok(_visitService.Add(model));
        }

        // PUT api/visits/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VisitModel model)
        {
            model.VisitId = id;
            return Ok(_visitService.Update(model));
        }

        // DELETE api/visits/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var visitDetails = _visitService.GetById(id);
            if (visitDetails == null)
            {
                return NotFound();
            }
            if ((bool)_visitService.Remove(id).Response)
            {
                return Ok(id);
            }
            return BadRequest();
        }
    }
}
