using ApiSkeletonPoc.Core.Helpers;
using Newtonsoft.Json;
using System;

namespace ApiSkeletonPoc.Core.Models
{
    public abstract class BaseModel
    {
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime? RecordCreatedDate { get; set; }
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime? RecordUpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
