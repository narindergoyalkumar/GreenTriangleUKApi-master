using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.Services;
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
    public class ValveController : ControllerBase
    {
        private readonly IValveService _valveService;
        public ValveController(IValveService valveService)
        {
            _valveService = valveService;
        }

        /// <summary>
        /// returns a list of valves with pagination
        /// </summary>
        /// <param name="pageNum">Number of page</param>
        /// <param name="pageSize">Items per page</param>
        /// <returns>List of valves</returns>
        [HttpGet]
        [Authorize(Roles = "WaterEngineer,WaterAdmin")]
        public IActionResult Get([FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            return Ok(new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = new { items = _valveService.GetAll(out int count, pageNum, pageSize), total_count = count } });
        }
        [HttpGet]
        [Route("{qrId}")]
        [Authorize(Roles = "WaterEngineer,WaterAdmin")]
        public ActionResult<ValveModel> GetByQr(string qrId)
        {
            if (!_valveService.IsQrIdExist(qrId))
                return BadRequest("Invalid QR id");
            return Ok(new { valveDetail = _valveService.GetValveDetailByQR(qrId) });
        }
        /// <summary>
        /// Creates a valve.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Valve
        ///     {
        ///               "qrid": "70707",
        ///               "dmaName": "test dma",
        ///               "longitude": "00907909",
        ///               "latitude": "2242",
        ///               "assetId": "65464",
        ///               "bvId": "64665",
        ///               "valveSize": "55",
        ///               "direction": "test",
        ///               "comment": "test comment",
        ///               "bvcontrolNumber": "655656",
        ///               
        ///     }
        ///
        /// </remarks>
        /// <param name="valveModel"></param>
        /// <returns>A newly created valve</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [Authorize(Roles = "WaterEngineer,WaterAdmin")]
        public IActionResult Post(ValveModel valveModel)
        {
            if (valveModel == null)
                return BadRequest();
            if (_valveService.IsQrIdExist(valveModel.Qrid))
                return BadRequest("QR id already exists.");
            return Ok(new { item = _valveService.InsertValveDtails(valveModel) });
        }

        /// <summary>
        /// Updates a valve.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Valve/3fa85f64-5717-4562-b3fc-2c963f66afa6
        ///     {
        ///               "valveId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///               "qrid": "70707",
        ///               "dmaName": "test dma",
        ///               "longitude": "00907909",
        ///               "latitude": "2242",
        ///               "assetId": "65464",
        ///               "bvId": "64665",
        ///               "valveSize": "55",
        ///               "direction": "test",
        ///               "comment": "test comment",
        ///               "bvcontrolNumber": "655656",
        ///              
        ///     }
        ///
        /// </remarks>
        /// <param name="qrid"></param>
        /// <param name="valveModel"></param>
        /// <returns>An updated valve</returns>
        /// <response code="201">Returns the updated item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPut]
        [Route("{qrid}")]
        [Authorize(Roles = "WaterAdmin,WaterEngineer")]
        public IActionResult Put(string qrid, ValveModel valveModel)
        {
            var valveDetails = _valveService.GetValveDetailByQR(qrid);
            if (valveDetails == null)
                return BadRequest("Invalid qr id.");
            if (!_valveService.IsValveExist(valveDetails.Id.ToString()))
                return BadRequest("Invalid valve id.");
            return Ok(new { item = _valveService.UpdateValveDetails(valveDetails.Id, valveModel) });
        }


        /// <summary>
        /// returns 
        /// </summary>
        /// <param name="valveId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search-valve-by-id")]
        [Authorize(Roles = "WaterAdmin,WaterEngineer")]
        public IActionResult SearchValveById(string valveId)
        {
            
            var data = _valveService.SearchById(valveId);
            var count = data.Count();
            return Ok(new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = new { items = data, total_count = count } });
        }
    }
}
