using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class ModuleModel
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DisplayName { get; set; }
        public string MatIcon { get; set; }
        public string Route { get; set; }
    }
}
