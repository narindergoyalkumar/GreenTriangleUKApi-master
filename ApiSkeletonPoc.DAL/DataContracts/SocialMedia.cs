using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string Link { get; set; }
        public byte[] Image { get; set; }
        public int SocialMediaTypeId { get; set; }
        public int ContactId { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual SocialMediaType SocialMediaType { get; set; }
    }
}
