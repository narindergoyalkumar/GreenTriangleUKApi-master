using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class UserModuleMapping
    {
        public int UserModuleMappingId { get; set; }
        public int? UserId { get; set; }
        public int? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Module Module { get; set; }
        public virtual User User { get; set; }
    }
}
