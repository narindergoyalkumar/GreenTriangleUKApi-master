using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public partial class OrganizationModel : BaseModel
    {
        public int OrgId { get; set; }
     
        public string OrgName { get; set; }
    }
}
