using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using EmailService;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobEstimatorController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleMappingService _userRoleMappingService;
        private readonly IJobEstimatorService _jobEstimatorService;
        private readonly IImageService _imageService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        public JobEstimatorController(
          IUserService userService, IUserRoleMappingService userRoleMappingService, IJobEstimatorService jobEstimatorService, IImageService imageService, IEmailService emailService, IConfiguration configuration, IFileService fileService)
        {
            _userService = userService;
            _userRoleMappingService = userRoleMappingService;
            _jobEstimatorService = jobEstimatorService;
            _imageService = imageService;
            _emailService = emailService;
            _configuration = configuration;
            _fileService = fileService;
        }

        [HttpPost("add-client")]
        [Authorize(Roles = "JobEstimatorAdmin")]
        public IActionResult AddClient([FromBody] JobEstimatorClientRequestModel model)
        {
            var userModel = new UserModel
            {
                ClientId = null,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
            string password = Utility.CreateRandomString();
            // create user
            var response = _userService.Create(userModel, password);
            if (response.Response != null)
            {
                if (response.IsSuccess)
                {
                    var _user = (UserModel)response.Response;
                    _userRoleMappingService.MapUserRole(new UserRoleMappingModel { UserId = _user.Id, UserRoleId = (int)Enums.UserRole.JobEstimatorClient });
                    if (_user != null)
                    {
                        string key = Utility.CreateRandomAlphabets();
                        JobEstimatorClientModel jobEstimatorClientModel = new JobEstimatorClientModel
                        {
                            ClientKey = key,
                            UserId = _user.Id,
                            CategoryId = model.CategoryId,
                            Note = model.Note
                        };
                        _jobEstimatorService.AddClient(jobEstimatorClientModel);
                        return Ok(new { key, password });
                    }
                }
                else
                {
                    return Ok(response.Message);
                }

            }
            return Ok(response);
        }
        [HttpDelete]
        [Authorize(Roles = "JobEstimatorAdmin")]
        [Route("remove-client/{id}")]
        public IActionResult RemoveClient(int id)
        {
            _jobEstimatorService.DeleteClient(id);
            return Ok();
        }
        [HttpGet]
        [Route("get-clients-products")]
        [Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        public IActionResult GetProductsByClient(string clientKey)
        {
            return Ok(_jobEstimatorService.GetClientProductsByKey(clientKey).OrderByDescending(a => a.ProductStyleId));
        }
        [HttpPost]
        [Route("add-product-style")]
        [Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        public IActionResult AddProductStyle([FromForm] JobEstimatorProductStyleModel jobEstimatorProductStyleModel)
        {
            var imageName = _imageService.UploadImage(jobEstimatorProductStyleModel.Image).Result;
            jobEstimatorProductStyleModel.StyleImage = imageName;
            return Ok(_jobEstimatorService.AddProductStyle(jobEstimatorProductStyleModel));
        }
        [HttpPut]
        [Route("update-product-style/{id}")]
        [Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        public IActionResult UpdateProductStyle(int id, [FromBody] JobEstimatorProductStyleModel jobEstimatorProductStyleModel)
        {
            //var imageName = _imageService.UploadImage(jobEstimatorProductStyleModel.Image).Result;
            //jobEstimatorProductStyleModel.StyleImage = imageName;
            _jobEstimatorService.UpdateProductStyle(id, jobEstimatorProductStyleModel);
            return Ok(true);
        }
        [HttpDelete]
        [Route("remove-product-style/{id}")]
        [Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        public IActionResult RemoveProductStyle(int id)
        {
            _jobEstimatorService.RemoveProductStyle(id);
            return Ok(true);
        }
        [HttpGet]
        //[Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        [AllowAnonymous]
        [Route("product-style-by-product-type-and-client/{productTypeId}")]
        public IActionResult GetClientProductsByProductType(int productTypeId, string key)
        {
            return Ok(_jobEstimatorService.GetClientProductsByProductType(productTypeId, key));
        }
        [HttpGet]
        //[Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        [Route("product-types/{categoryId}")]
        [AllowAnonymous]
        public IActionResult GetProductTypes(int categoryId)
        {
            return Ok(_jobEstimatorService.GetProductTypes(categoryId));
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("send-estimate-email")]
        public IActionResult SendEstimateEmail([FromBody] JobEstimatorEmailModel jobEstimatorEmailModel)
        {
            var client = _jobEstimatorService.GetClientByKey(jobEstimatorEmailModel.Key);
            if (client != null)
            {
                var recipients = client?.Recipients?.Split(';');
                if (jobEstimatorEmailModel.Category == "ByTheTonne")
                {
                    if(recipients.Any())
                    {
                        for (int i = 0; i < recipients.Length; i++)
                        {
                            EmailModel emailModel = new EmailModel
                            {
                                Body = @$"<p>Hello {client?.User?.FirstName} {client?.User?.LastName}</p>
                              <p>Please find below an estimate created on your website at {jobEstimatorEmailModel.EstimatedTime}.</p>
                              <p>
                                <strong>Name: </strong>{jobEstimatorEmailModel.VisitorFullName}
                                <br />
                                <strong>Email: </strong>{jobEstimatorEmailModel.VisitorEmail}
                                <br />
                                <strong>Phone: </strong>{jobEstimatorEmailModel.VisitorPhone}
                                <br />
                                <br />
                                <strong>Product Type: </strong>{jobEstimatorEmailModel.ProductType}
                                <br />
                                <strong>Product Style: </strong>{jobEstimatorEmailModel.ProductStyle}
                                <br />
                                <br />
                                <strong>Units: </strong>{jobEstimatorEmailModel.Unit}
                                <br />
                                <br />
                                <strong>Total Cost: </strong>&pound;{jobEstimatorEmailModel.Cost}
                                <br />
                                <br />
                                Regards,
                                <br />
                                <br />
                                <br />
                                costmyjob@greentriangleuk.com
                              </p>",
                                From = _configuration.GetSection("JobEstimatorEmailSettings:From").Value,
                                To = recipients[i],
                                Host = _configuration.GetSection("JobEstimatorEmailSettings:Smtp").Value,
                                Password = _configuration.GetSection("JobEstimatorEmailSettings:Password").Value,
                                Port = Convert.ToInt32(_configuration.GetSection("JobEstimatorEmailSettings:Port").Value),
                                Subject = $"{jobEstimatorEmailModel.EstimateId} - {jobEstimatorEmailModel.VisitorFullName}",
                                Cc = _configuration.GetSection("JobEstimatorEmailSettings:CC").Value,
                            };
                            _emailService.SendEmail(emailModel);
                        }
                    }
                    
                }
                else
                {
                    if(recipients.Any())
                    {
                        for (int i = 0; i < recipients.Length; i++)
                        {
                            EmailModel emailModel = new EmailModel
                            {
                                Body = @$"<p>Hello {client?.User?.FirstName} {client?.User?.LastName}</p>
                              <p>Please find below an estimate created on your website at {jobEstimatorEmailModel.EstimatedTime}.</p>
                              <p>
                                <strong>Name: </strong>{jobEstimatorEmailModel.VisitorFullName}
                                <br />
                                <strong>Email: </strong>{jobEstimatorEmailModel.VisitorEmail}
                                <br />
                                <strong>Phone: </strong>{jobEstimatorEmailModel.VisitorPhone}
                                <br />
                                <br />
                                <strong>Product Type: </strong>{jobEstimatorEmailModel.ProductType}
                                <br />
                                <strong>Product Style: </strong>{jobEstimatorEmailModel.ProductStyle}
                                <br />
                                <br />
                                <strong>Dimensions: </strong>{jobEstimatorEmailModel.Dimension}
                                <br />
                                <strong>Area: </strong>{jobEstimatorEmailModel.Area} 
                                <br />
                                <br />
                                <strong>Total Cost: </strong>&pound;{jobEstimatorEmailModel.Cost}
                                <br />
                                <br />
                                Regards,
                                <br />
                                <br />
                                <br />
                                costmyjob@greentriangleuk.com
                              </p>",
                                From = _configuration.GetSection("JobEstimatorEmailSettings:From").Value,
                                To = recipients[i],
                                Host = _configuration.GetSection("JobEstimatorEmailSettings:Smtp").Value,
                                Password = _configuration.GetSection("JobEstimatorEmailSettings:Password").Value,
                                Port = Convert.ToInt32(_configuration.GetSection("JobEstimatorEmailSettings:Port").Value),
                                Subject = $"{jobEstimatorEmailModel.EstimateId} - {jobEstimatorEmailModel.VisitorFullName}",
                                Cc = _configuration.GetSection("JobEstimatorEmailSettings:CC").Value,
                            };
                            _emailService.SendEmail(emailModel);
                        }
                    }
                    
                }


                return Ok(true);
            }
            return Ok(false);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("is-client-key-exists")]
        public IActionResult IsClientKeyExists(string key)
        {
            return Ok(_jobEstimatorService.IsClientKeyExists(key));
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("clients")]
        public IActionResult GetClients()
        {
            return Ok(_jobEstimatorService.GetClients());
        }
        [HttpPost("bulk-upload-carpet-product-styles")]
        //[Authorize(Roles = "JobEstimatorAdmin")]
        public async Task<IActionResult> ImportCarpetCsv(string key, IFormFile csvFile)
        {
            var client = _jobEstimatorService.GetClientByKey(key);
            if (client == null || string.IsNullOrEmpty(key))
            {
                return BadRequest("Client Key is invalid");
            }
            if (csvFile.Length > 0)
            {
                var fileModel = await _fileService.Upload(csvFile);
                if (fileModel != null)
                {
                    List<JobEstimatorProductStyleModel> jobEstimatorProductStyles = new List<JobEstimatorProductStyleModel>();
                    var csvTable = new DataTable();
                    using var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(fileModel.Path)), true);
                    csvTable.Load(csvReader);
                    for (int i = 0; i < csvTable.Rows.Count; i++)
                    {
                        int typeId = _jobEstimatorService.GetTypeIdByName(Convert.ToString(csvTable.Rows[i].ItemArray[0]), client.CategoryId.GetValueOrDefault());
                        JobEstimatorProductStyleModel jobEstimatorProductStyleModel = new JobEstimatorProductStyleModel
                        {
                            ProductTypeId = typeId,
                            StyleName = Convert.ToString(csvTable.Rows[i].ItemArray[1]),
                            MaterialCost = Convert.ToDecimal(csvTable.Rows[i].ItemArray[2]),
                            LabourCost = Convert.ToDecimal(csvTable.Rows[i].ItemArray[3]),
                            StyleImage = Convert.ToString(csvTable.Rows[i].ItemArray[4]),
                            UnitCost = Convert.ToDecimal(csvTable.Rows[i].ItemArray[5]),
                            JobEstimatorClientId = client.ClientId,
                            IsRollsCalculation = Convert.ToBoolean(csvTable.Rows[i].ItemArray[6]),
                            RollSize = csvTable.Rows[i].ItemArray[7] == DBNull.Value ? null : Convert.ToString(csvTable.Rows[i].ItemArray[7])
                        };
                        jobEstimatorProductStyles.Add(jobEstimatorProductStyleModel);
                    }
                    int productImportedCount = _jobEstimatorService.UploadBulk(jobEstimatorProductStyles, client.ClientId);
                    csvReader.Dispose();
                    _fileService.Delete(fileModel.Path);
                    return Ok($"{productImportedCount} products imported successfully");
                }

            }
            return BadRequest("Invalid file");
        }
        [HttpGet]
        [Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        [Route("get-client-by-user-id")]
        public IActionResult GetClientByUserId(string userId)
        {
            return Ok(_jobEstimatorService.GetClientByUserId(userId));
        }
        [HttpPut]
        [Route("update-style-image/{id}")]
        public IActionResult UpdateImage(int id, IFormFile image)
        {
            string imageLink = null;
            if (image != null)
            {
                imageLink = _imageService.UploadImage(image).Result;
                if (!string.IsNullOrEmpty(imageLink) && imageLink != "Invalid image file")
                {
                    _jobEstimatorService.UpdateProductStyleImage(id, imageLink);
                }
            }
            return Ok(imageLink);
        }
        [HttpPut]
        [Authorize(Roles = "JobEstimatorAdmin,JobEstimatorClient")]
        [Route("update-client/{id}")]
        public IActionResult UpdateClient(int id, JobEstimatorClientModel jobEstimatorClientModel)
        {
            //if (_jobEstimatorService.GetClientByUserId(jobEstimatorClientModel.UserId.ToString()) == null)
            //{
            //    return BadRequest("Client doesn't exist");
            //}
            //var user = _userService.GetById(jobEstimatorClientModel.UserId.GetValueOrDefault());
            //if (user == null)
            //{
            //    return BadRequest("Invalid user.");
            //}
            //if (jobEstimatorClientModel.Email != user.Email)
            //{
            //    _userService.UpdateEmail(jobEstimatorClientModel.UserId.GetValueOrDefault(), jobEstimatorClientModel.Email);
            //}
            _jobEstimatorService.UpdateClient(id, jobEstimatorClientModel);
            return Ok(true);
        }
    }
}
