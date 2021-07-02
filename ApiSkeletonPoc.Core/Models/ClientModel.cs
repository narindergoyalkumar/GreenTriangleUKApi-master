using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.Core.Models
{
    public class ClientModel : BaseModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public int RemainingTextsCount { get; set; }
    }
}
