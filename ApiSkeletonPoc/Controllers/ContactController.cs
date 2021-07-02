using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IVisitsService _visitsService;

        public ContactController(IContactService contactService, IVisitsService visitsService)
        {
            _contactService = contactService;
            _visitsService = visitsService;
        }
        // GET: api/Contact
        //Complete
        [HttpGet]
        public IActionResult Get([FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            int clientId = Convert.ToInt32(User.GetClientId());
            return Ok(new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = new { items = _contactService.GetAll(clientId, out int count, pageNum, pageSize), total_count = count } });
        }

        // GET: api/Contact/5
        //Complete
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            int clientId = Convert.ToInt32(User.GetClientId());
            return Ok(_contactService.Get(id, clientId));
        }

        // POST: api/Contact
        //Complete
        [HttpPost]
        public IActionResult Post([FromBody] ContactModel contactModel)
        {
            contactModel.ClientId = Convert.ToInt32(User.GetClientId());
            var contactId = _contactService.Add(contactModel);
            return Ok(contactId);
        }

        [HttpPost("visit")]
        public IActionResult CreateContactVisit([FromBody] VisitModel model)
        {
            return Ok(_visitsService.Add(model));
        }

        // PUT: api/Contact/5
        //Complete
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactModel contactModel)
        {
            contactModel.ContactId = id;
            return Ok(_contactService.Update(contactModel));
        }

        // DELETE: api/Contact/5
        //Complete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_contactService.Remove(id));
        }


        [HttpPost]
        [Route("import")]
        public IActionResult Post([FromBody] List<ContactModel> contactModels)
        {
            return Ok(_contactService.Import(contactModels, Convert.ToInt32(User.GetClientId())));
        }

        [HttpPost]
        [Route("delete-bulk")]
        public IActionResult BulkDeleteContacts(List<int> contactIds)
        {
            _contactService.BulkDelete(contactIds);
            return Ok(true);
        }
        [HttpGet]
        [Route("search-contact-by-address")]
        public IActionResult SearchAddressByContact(string addressLineOne)
        {
            int clientId = Convert.ToInt32(User.GetClientId());
            var data = _contactService.SearchContactByAddress(addressLineOne,clientId);
            var count = data.Count();
            return Ok(new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = new { items = data, total_count = count } });
        }
        //[HttpGet]
        //[Route("get-sweep-dues")]
        //public IActionResult GetSweepDues()
        //{
        //    return Ok(_contactService.GetSweepDueContacts(Convert.ToInt32(User.GetClientId())));
        //}
    }
}
