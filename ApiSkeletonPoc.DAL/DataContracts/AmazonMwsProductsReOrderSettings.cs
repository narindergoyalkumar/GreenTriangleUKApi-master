using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class AmazonMwsProductsReOrderSettings
    {
        public int ReOrderConfigId { get; set; }
        public int ReOrderDays { get; set; }
        public string ReOderDaysAlarmColorCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
