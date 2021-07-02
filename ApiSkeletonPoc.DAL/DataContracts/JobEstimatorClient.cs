using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobEstimatorClient
    {
        public JobEstimatorClient()
        {
            JobEstimatorProductStyle = new HashSet<JobEstimatorProductStyle>();
        }

        public int ClientId { get; set; }
        public Guid? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string ClientKey { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Note { get; set; }
        public string Recipients { get; set; }
        public string Disclaimer { get; set; }

        public virtual JobEstimatorCategory Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<JobEstimatorProductStyle> JobEstimatorProductStyle { get; set; }
    }
}
