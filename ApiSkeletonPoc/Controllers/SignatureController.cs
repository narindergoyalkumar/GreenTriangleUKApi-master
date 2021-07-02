using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Extensions;
using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;

namespace ApiSkeletonPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ISignatureService _signatureService;
        private readonly IDocumentService _documentService;
        public SignatureController(IEmailService emailService, IConfiguration configuration, ISignatureService signatureService, IDocumentService documentService)
        {
            _emailService = emailService;
            _configuration = configuration;
            _signatureService = signatureService;
            _documentService = documentService;
        }
        [HttpPost]
        [Route("send-disclaimer-email-with-signature")]
        public IActionResult SendDisclaimerAsPdf([FromBody]SendDocAsMailModel sendDisclaimerAsMailModel)
        {
            var bytes = sendDisclaimerAsMailModel.PdfBase64String == null ? null : Convert.FromBase64String(sendDisclaimerAsMailModel.PdfBase64String.Replace("\"", string.Empty).Trim());
            EmailModel emailModel = new EmailModel
            {
                Attachment = bytes == null ? null : new MemoryStream(bytes),
                Body = "<p>Hi,</p><br />Please find attached the disclaimer for " + sendDisclaimerAsMailModel.MailSubject + ".<br /><br />Regards,<br /><br />Green Triangle UK",
                From = _configuration.GetSection("EmailSettings:From").Value,
                To = _configuration.GetSection("EmailSettings:To").Value,
                Host = _configuration.GetSection("EmailSettings:Smtp").Value,
                Password = _configuration.GetSection("EmailSettings:Password").Value,
                Port = Convert.ToInt32(_configuration.GetSection("EmailSettings:Port").Value),
                Subject = "Disclaimer: " + sendDisclaimerAsMailModel.MailSubject,
                ContentType = "application/pdf"
            };
            _emailService.SendEmail(emailModel);
            return Ok();
        }
        [HttpPost]
        [Route("send-email-to-review-document")]
        public IActionResult SendEmailToReview([FromForm]SignatureDocModel signatureDocModel)
        {
            var data = _documentService.UploadDoc(signatureDocModel.Document);
            if (data != null)
            {
                signatureDocModel.Link = data.Result.Item1;
                signatureDocModel.SignatureDocId = data.Result.Item2;
                signatureDocModel.Name = Path.GetFileNameWithoutExtension(signatureDocModel.Document.FileName);
                signatureDocModel.Extension = "." + signatureDocModel.Document.FileName.Split('.')[^1];
                var isDocAdded = _signatureService.InsertSignatureDoc(signatureDocModel);
                if (isDocAdded)
                {
                    string reviewDocUrl = _configuration.GetValue<string>("WebBaseUrl") + "review-doc/" + data.Result.Item2;
                    EmailModel emailModel = new EmailModel
                    {
                        Body = string.Format(Constants.SignatureReviewEmailContent, signatureDocModel.ReceiverName, User.GetClientName(), reviewDocUrl, signatureDocModel.OwnerName),
                        From = _configuration.GetSection("EmailSettings:From").Value,
                        To = signatureDocModel.ReceiverEmail,
                        Host = _configuration.GetSection("EmailSettings:Smtp").Value,
                        Password = _configuration.GetSection("EmailSettings:Password").Value,
                        Port = Convert.ToInt32(_configuration.GetSection("EmailSettings:Port").Value),
                        Subject = "Please sign: " + signatureDocModel.Name,
                    };
                    _emailService.SendEmail(emailModel);
                }
            }
            return Ok();
        }
        [HttpGet]
        [Route("{docId}")]
        public IActionResult Get(Guid docId)
        {
            var model = _signatureService.GetSingle(docId);
            if (model != null)
            {
                string pdfUrl = model.Link;
                using WebClient client = new WebClient();
                var bytes = client.DownloadData(pdfUrl);
                model.Base64Pdf = Convert.ToBase64String(bytes);
            }
            return Ok(model);
        }
        [HttpPost]
        [Route("send-signed-doc-to-receiver")]
        public IActionResult SendSignedDocumentToReceiver([FromBody]SendDocAsMailModel sendDocAsMailModel)
        {
            var bytes = sendDocAsMailModel.PdfBase64String == null ? null : Convert.FromBase64String(sendDocAsMailModel.PdfBase64String.Replace("\"", string.Empty).Trim());
            EmailModel emailModel = new EmailModel
            {
                Attachment = bytes == null ? null : new MemoryStream(bytes),
                Body = "<p>Hi "+sendDocAsMailModel.ReceiverName + ",</p><br />Signing of the document has been complete and a copy of the signed document is attached.<br /><br />Many thanks,<br /><br />" + sendDocAsMailModel.OwnerName,
                From = _configuration.GetSection("EmailSettings:From").Value,
                To = sendDocAsMailModel.ReceiverEmail,
                Host = _configuration.GetSection("EmailSettings:Smtp").Value,
                Password = _configuration.GetSection("EmailSettings:Password").Value,
                Port = Convert.ToInt32(_configuration.GetSection("EmailSettings:Port").Value),
                Subject = "Document Sign Completed: " + sendDocAsMailModel.MailSubject,
                ContentType = "application/pdf",
                Cc=sendDocAsMailModel.Cc
            };
            _emailService.SendEmail(emailModel);
            return Ok();
        }
    }
}
