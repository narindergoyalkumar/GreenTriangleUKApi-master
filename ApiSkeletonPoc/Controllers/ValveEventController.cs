using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValveEventController : ControllerBase
    {
        private readonly IValveEventService _valveEventService;
        private readonly IValveService _valveService;
        public ValveEventController(IValveEventService valveEventService, IValveService valveService)
        {
            _valveEventService = valveEventService;
            _valveService = valveService;
        }

        /// <summary>
        /// Creates a valve.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ValveEvent
        ///     {
        ///               "eventId": 0,
        ///               "eventType": "test event,
        ///               "dateTimeStamp": "2021-06-14T14:14:25.709Z",
        ///               "eventDescription": "test description",
        ///               "qrId": "6656446",
        ///               "engineerId":"1131131313"
        ///     }
        ///
        /// </remarks>
        /// <param name="valveEventModel"></param>
        /// <returns>true</returns>
        /// <response code="201">Returns the true/false</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [Authorize(Roles = "WaterEngineer,WaterAdmin")]
        public IActionResult Post(ValveEventModel valveEventModel)
        {
            var valve = _valveService.GetValveDetailByQR(valveEventModel.QrId);
            if (valve == null)
                return BadRequest("Invalid QR Id");
            valveEventModel.ValveId = valve.Id;
            return Ok(_valveEventService.AddEvent(valveEventModel));
        }

        /// <summary>
        /// get the qr specific events
        /// </summary>
        /// <param name="qrId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "WaterEngineer,WaterAdmin")]
        public IActionResult Get(string qrId)
        {
            var valve = _valveService.GetValveDetailByQR(qrId);
            if (valve == null)
                return BadRequest("Invalid QR Id");
            return Ok(_valveEventService.GetEventByQRId(qrId, valve.Id));
        }

        /// <summary>
        /// get the event types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("event-types")]
        [Authorize(Roles = "WaterEngineer,WaterAdmin")]
        public IActionResult GetEventTypes()
        {
            return Ok(_valveEventService.GetEventTypes());
        }
    }
}
