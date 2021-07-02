using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class VisitsRequestModel
    {
        public int Visit_ID { get; set; }
        public Nullable<int> Org_ID { get; set; }
        public Nullable<int> Individual_ID { get; set; }
        public Nullable<int> Employee_ID { get; set; }
        public string visit_booked_flg { get; set; }
        public Nullable<System.DateTime> visit_date { get; set; }
        public Nullable<System.DateTime> visit_due { get; set; }
        public string visit_type { get; set; }
    }
}
