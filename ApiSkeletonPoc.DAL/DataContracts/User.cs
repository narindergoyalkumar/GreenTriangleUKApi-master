using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class User
    {
        public User()
        {
            AmazonMwsProductsReOrderSettings = new HashSet<AmazonMwsProductsReOrderSettings>();
            JobEstimatorClient = new HashSet<JobEstimatorClient>();
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? ClientId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<AmazonMwsProductsReOrderSettings> AmazonMwsProductsReOrderSettings { get; set; }
        public virtual ICollection<JobEstimatorClient> JobEstimatorClient { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
