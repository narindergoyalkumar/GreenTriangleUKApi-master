using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Module
    {
        public Module()
        {
            ClientModuleMapping = new HashSet<ClientModuleMapping>();
        }

        public int ModuleId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DisplayName { get; set; }
        public string MatIcon { get; set; }
        public string ModuleRoute { get; set; }

        public virtual ICollection<ClientModuleMapping> ClientModuleMapping { get; set; }
    }
}
