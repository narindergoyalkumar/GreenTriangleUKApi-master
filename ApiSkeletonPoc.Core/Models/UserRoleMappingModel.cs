using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class UserRoleMappingModel
    {
        public int? UserRoleId { get; set; }
        public Guid  UserId { get; set; }
        public bool? IsActive { get; set; }
    }
}
