using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobEstimatorCategory
    {
        public JobEstimatorCategory()
        {
            JobEstimatorClient = new HashSet<JobEstimatorClient>();
            JobEstimatorProductType = new HashSet<JobEstimatorProductType>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<JobEstimatorClient> JobEstimatorClient { get; set; }
        public virtual ICollection<JobEstimatorProductType> JobEstimatorProductType { get; set; }
    }
}
