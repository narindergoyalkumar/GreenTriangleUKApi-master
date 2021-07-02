using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobEstimatorProduct
    {
        public int ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string CalculationMethod { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual JobEstimatorCategory Category { get; set; }
    }
}
