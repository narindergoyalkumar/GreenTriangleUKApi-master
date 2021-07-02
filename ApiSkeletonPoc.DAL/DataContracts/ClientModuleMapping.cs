using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class ClientModuleMapping
    {
        public int ClientModuleMappingId { get; set; }
        public int? ClientId { get; set; }
        public int? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Module Module { get; set; }
    }
}
