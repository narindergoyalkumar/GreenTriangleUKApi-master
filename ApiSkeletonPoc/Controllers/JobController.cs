using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            return Ok(new { items = _jobService.GetAll(out int count, pageNum, pageSize), total_count = count });
        }

        // GET: api/Job/5
        //Complete
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_jobService.GetSingle(id));
        }
        // POST: api/Job
        //Complete
        [HttpPost]
        public IActionResult Post([FromBody] JobModel jobModel)
        {
            return Ok(_jobService.AddJob(jobModel));
        }

        // PUT: api/Job/5
        //Complete
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JobModel jobModel)
        {
            return Ok(_jobService.UpdateJob(id, jobModel));
        }

        // DELETE: api/Job/5
        //Complete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_jobService.Delete(id));
        }
        [HttpPost]
        [Route("delete-bulk")]
        public IActionResult BulkDeleteJobs(List<int> jobsIds)
        {
            _jobService.BulkDelete(jobsIds);
            return Ok(true);
        }

        [HttpGet]
        [Route("get-current-week-recurring-jobs")]
        public IActionResult GetWeeklyReccuringJobs()
        {
            //DateTime? startDate = Utility.GetStartDateOfWeek();
            //DateTime? endDate = Utility.GetEndDateOfWeek();
            //if (startWeekDate != null && endWeekDate != null)
            //{
            //    startDate = startWeekDate;
            //    endDate = endWeekDate;
            //}
            return Ok(_jobService.GetWeeklyRecurringJobs());
        }
    }
}
