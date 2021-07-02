using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class UserModuleMappingModel
    {
        public int UserModuleMappingId { get; set; }
        public int? UserId { get; set; }
        public int? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModuleName { get; set; }
    }
}
