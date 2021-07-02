using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "AmazonSalesAdmin,AmazonSalesUser")]
    public class AmazonMWSController : ControllerBase
    {
        private readonly IAmazonMwsService _amazonMwsService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;
        public AmazonMWSController(IAmazonMwsService amazonMwsService, IImageService imageService, IUserService userService)
        {
            _amazonMwsService = amazonMwsService;
            _imageService = imageService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var products = _amazonMwsService.GetProducts();
            var settings = _amazonMwsService.GetMwsUserWithSettings(Guid.Parse(User.GetUserId())).AmazonMwsProductsReOrderSettings.OrderBy(a => a.ReOrderDays);
            if (settings.Any())
            {
                if (products.Any())
                {
                    foreach (var (setting, product) in settings.SelectMany(setting => products.Where(product => product.ReOrderDays <= setting.ReOrderDays).Where(product => string.IsNullOrEmpty(product.ReOrderColorCode)).Select(product => product).Select(product => (setting, product))))
                    {
                        product.ReOrderColorCode = setting.ReOderDaysAlarmColorCode;
                    }
                }
            }
            return Ok(new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = products });
        }
        [HttpPut]
        [Route("assign-short-name-to-product/{id}")]
        public IActionResult AssignShortName(int id, [FromBody]string shortName)
        {
            return Ok(_amazonMwsService.AssignShortName(id, shortName));
        }
        [HttpPut]
        [Route("assign-image-to-product/{id}")]
        public IActionResult AssignImage(int id, IFormFile assignedImage)
        {
            string imageLink = null;
            var product = _amazonMwsService.Get(id);
            if (product != null)
            {
                imageLink = _imageService.UploadImage(assignedImage, product.Asin).Result;
                if (!string.IsNullOrEmpty(imageLink) && imageLink != "Invalid image file")
                {
                    _amazonMwsService.UpdateImageForProduct(id, imageLink);
                }
            }
            return Ok(imageLink);
        }
        [HttpPut]
        [Route("update-in-transit-count/{id}")]
        public IActionResult UpdateInTransitCount(int id, [FromBody]int InTransitCount)
        {
            return Ok(_amazonMwsService.UpdateInTransitProductsCount(id, InTransitCount));
        }
        [HttpPut]
        [Route("reset-product-short-name/{id}")]
        public IActionResult ResetProductShortName(int id)
        {
            return Ok(_amazonMwsService.ResetProductShortName(id));
        }
        [HttpPut]
        [Route("reset-product-image/{id}")]
        public IActionResult ResetProductImage(int id)
        {
            return Ok(_amazonMwsService.ResetProductImage(id));
        }
        [HttpPut]
        [Route("set-lead-time/{id}")]
        public IActionResult SetLeadTime(int id, [FromBody]int days)
        {
            return Ok(_amazonMwsService.SetLeadTime(id, days));
        }
        [HttpPost]
        [Route("set-re-order-days-configuration")]
        //[Authorize(Roles = "AmazonSalesAdmin")]
        public IActionResult ReOrderDaysConfiguration(List<AmazonMwsProductsReOrderSettingsModel> configurationList)
        {
            var userId = Guid.Parse(User.GetUserId());
            return Ok(_amazonMwsService.SetReOrderDaysConfiguration(configurationList, userId));
        }
        [HttpPut]
        [Route("get-re-order-days-configuration")]
        public IActionResult GetReOrderDaysConfiguration()
        {
            return Ok(_amazonMwsService.GetMwsUserWithSettings(Guid.Parse(User.GetUserId())).AmazonMwsProductsReOrderSettings);
        }
    }
}
