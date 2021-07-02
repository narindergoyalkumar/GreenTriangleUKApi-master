using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class JobEstimatorEmailModel
    {
        public string EstimateId { get; set; }
        public string VisitorFullName { get; set; }
        public string VisitorEmail { get; set; }
        public string VisitorPhone { get; set; }
        public string ProductType { get; set; }
        public string ProductStyle { get; set; }
        public string Dimension { get; set; }
        public string Area { get; set; }
        public string Cost { get; set; }
        public string Key { get; set; }
        public string EstimatedTime { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
    }
}
