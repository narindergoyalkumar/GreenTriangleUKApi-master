using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class AmazonMwsConfig
    {
        public Guid MwsConfigId { get; set; }
        public string MwsSellerName { get; set; }
        public string MwsSellerId { get; set; }
        public string MwsAccessKeyId { get; set; }
        public string MwsSecretKeyId { get; set; }
        public string MwsauthToken { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
