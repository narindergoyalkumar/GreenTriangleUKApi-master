using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactNotesController : ControllerBase
    {
        private readonly IContactNotesService _contactNotesService;
        public ContactNotesController(IContactNotesService contactNotesService)
        {
            _contactNotesService = contactNotesService;
        }
        [HttpGet]
        [Route("get-notes")]
        public IActionResult Get(int contactId)
        {
            return Ok(_contactNotesService.GetAll(contactId));
        }


        // POST api/<ContactNotesController>
        [HttpPost]
        [Route("add-note")]
        public IActionResult Post([FromBody] ContactNotesModel contactNotesModel)
        {
            return Ok(_contactNotesService.SaveContactNote(contactNotesModel));
        }

        // PUT api/<ContactNotesController>/5
        [HttpPut]
        [Route("update-note/{id}")]
        public IActionResult Put(int id, [FromBody] ContactNotesModel contactNotesModel)
        {
            return Ok(_contactNotesService.UpdateContactNote(id, contactNotesModel));
        }

        // DELETE api/<ContactNotesController>/5
        [HttpDelete]
        [Route("delete-note")]
        public void Delete(int id, int contactId)
        {
            _contactNotesService.DeleteContactNote(contactId, id);
        }
    }
}
