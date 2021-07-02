using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class SocialMediaModel : BaseModel
    {
        public int SocialMediaId { get; set; }
        [MaxLength(200)]
        public string Link { get; set; }
        public byte[] Image { get; set; }
        public int SocialMediaTypeId { get; set; }
        public int ContactId { get; set; }
        public string SocialMediaType { get; set; }
    }
}
