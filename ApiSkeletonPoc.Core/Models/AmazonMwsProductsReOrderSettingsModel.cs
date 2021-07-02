using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class AmazonMwsProductsReOrderSettingsModel
    {
        public int ReOrderConfigId { get; set; }
        public int ReOrderDays { get; set; }
        public string ReOderDaysAlarmColorCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public Guid UserId { get; set; }
    }
}
