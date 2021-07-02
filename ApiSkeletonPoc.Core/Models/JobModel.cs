using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class JobModel
    {
        public int JobId { get; set; }
        public string Reference { get; set; }
        public int? JobTypeId { get; set; }
        public string JobType { get; set; }
        public int? JobStatusId { get; set; }
        public string JobStatus { get; set; }
        public int? JobFrequencyId { get; set; }
        public string JobFrequency { get; set; }
        public string Name { get; set; }
        public int? ContactId { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Day { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EstimateDays { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
