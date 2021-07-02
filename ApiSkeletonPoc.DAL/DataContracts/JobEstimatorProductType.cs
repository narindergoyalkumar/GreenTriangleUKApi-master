using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobEstimatorProductType
    {
        public JobEstimatorProductType()
        {
            JobEstimatorProductStyle = new HashSet<JobEstimatorProductStyle>();
        }

        public int ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CalculationMethod { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual JobEstimatorCategory Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<JobEstimatorProductStyle> JobEstimatorProductStyle { get; set; }
    }
}
