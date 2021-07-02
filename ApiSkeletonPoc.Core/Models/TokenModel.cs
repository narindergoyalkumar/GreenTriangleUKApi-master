using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string UserName { get; set; }
        public string Name { get;  set; }
        public bool TwoFactorEnabled { get; set; }
        public string ImagePath { get; set; }
        public IList<string> Roles { get;  set; }
        public string Role { get;  set; }
        public Guid UserId { get; set; }
        public List<int?> SubscribedModules { get; set; }
    }
}
