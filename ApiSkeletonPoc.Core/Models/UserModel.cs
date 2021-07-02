using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class UserModel
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? ClientId { get; set; }
        public string Role { get; set; }
        public string Client { get; set; }

        public bool? IsActive { get; set; }
        public List<int?> SubscribedModules { get; set; }
    }
}
