using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class SocialMediaReqestModel
    {
        public int SocialMediaId { get; set; }
        
        [MaxLength(20)]
        public string Social_media_type { get; set; }
        
        [MaxLength(200)]
        public string Link { get; set; }
        public byte[] Image { get; set; }
    }
}
