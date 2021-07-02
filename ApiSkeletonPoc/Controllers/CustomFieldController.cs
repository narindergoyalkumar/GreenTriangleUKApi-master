using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CustomFieldController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;
        public CustomFieldController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }
        // GET: api/CustomField
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customFieldService.Get());
        }


        // POST: api/CustomField
        [HttpPost]
        public IActionResult Post([FromBody] CustomFieldModel customFieldModel)
        {
            if (customFieldModel.ClientId > 0)
            {
                return Ok(_customFieldService.Add(customFieldModel));
            }
            return BadRequest(new BaseResponseModel { IsSuccess = false, Message = "Please choose a valid client.", Response = null });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_customFieldService.Delete(id));
        }
    }
}
