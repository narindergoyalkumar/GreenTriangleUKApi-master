using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Job
    {
        public Job()
        {
            JobNotes = new HashSet<JobNotes>();
        }

        public int JobId { get; set; }
        public string Reference { get; set; }
        public int? JobTypeId { get; set; }
        public int? JobStatusId { get; set; }
        public int? JobFrequencyId { get; set; }
        public string Name { get; set; }
        public int? ContactId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? EstimateDays { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string Day { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual JobFrequency JobFrequency { get; set; }
        public virtual JobStatus JobStatus { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual ICollection<JobNotes> JobNotes { get; set; }
    }
}
