using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobNotesController : ControllerBase
    {
        private readonly IJobNotesService _jobNotesService;
        public JobNotesController(IJobNotesService jobNotesService)
        {
            _jobNotesService = jobNotesService;
        }
        [HttpGet]
        [Route("get-notes")]
        public IActionResult Get(int jobId)
        {
            return Ok(_jobNotesService.GetAll(jobId));
        }


        // POST api/<ContactNotesController>
        [HttpPost]
        [Route("add-note")]
        public IActionResult Post([FromBody] JobNotesModel jobNotesModel)
        {
            return Ok(_jobNotesService.SaveJobNote(jobNotesModel));
        }

        // PUT api/<ContactNotesController>/5
        [HttpPut]
        [Route("update-note/{id}")]
        public IActionResult Put(int id, [FromBody] JobNotesModel jobNotesModel)
        {
            return Ok(_jobNotesService.UpdateJobNote(id, jobNotesModel));
        }

        // DELETE api/<JobNotesController>/5
        [HttpDelete]
        [Route("delete-note")]
        public void Delete(int id, int jobId)
        {
            _jobNotesService.DeleteJobNote(jobId, id);
        }
    }
}
