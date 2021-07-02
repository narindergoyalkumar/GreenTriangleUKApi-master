using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int RoleId { get; set; }
        public string Role { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
