using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapsuleApiController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<CapsuleApiController> _logger;

        public CapsuleApiController(IContactService contactService, ILogger<CapsuleApiController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }
        // GET: api/Contact
        //Complete
        [HttpGet]
        [Route("parties")]
        public IActionResult Get([FromQuery] int page, [FromQuery] int perPage)
        {
            return Ok(new { parties = _contactService.GetCapsuleContacts(page, perPage) });
        }
        [HttpPut]
        [Route("parties/{partyId}")]
        public IActionResult Put(int partyId, [FromBody] Party party)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(party));
            ContactModel contactModel = new ContactModel
            {
                Address = party.Addresses?.Select(a => new AddressModel
                {
                    AddressId = Convert.ToInt32(a.Id),
                    City = a.City,
                    Country = a.Country,
                    LineOne=a.Street,
                    Postcode=a.Zip
                }).ToList(),
                ContactId=partyId,
                CustomFieldModel=party.Fields?.Select(a=>new CustomFieldModel
                {
                    CustomFieldId=a.Id,
                    Value=a.Value
                }).ToList(),
                Email=party.EmailAddresses?.Select(a=>a.Address).FirstOrDefault(),
                Individual=new IndividualModel {FirstName=party.FirstName,JobTitle=party.JobTitle,LastName=party.LastName,OrgName=Convert.ToString(party.Organisation), Title=party.Title},
                Phone=party.PhoneNumbers?.Select(a=>new PhoneModel
                {
                    Number=a.Number,
                    PhoneType=Convert.ToString(a.Type),
                    PhoneId=a.Id
                }).ToList()
            };
            contactModel.ContactId = partyId;
            return Ok(_contactService.Update(contactModel));
        }
    }
}
