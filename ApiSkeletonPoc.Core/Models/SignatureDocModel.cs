using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class SignatureDocModel
    {
        public Guid SignatureDocId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Link { get; set; }
        public IFormFile Document { get; set; }
        public string SignatureBoxConfig { get; set; }
        public string Base64Pdf { get; set; }
    }
}
