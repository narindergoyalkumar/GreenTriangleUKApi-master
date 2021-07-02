using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class UserRoleMapping
    {
        public int UserRoleMappingId { get; set; }
        public int? UserRoleId { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsActive { get; set; }

        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
