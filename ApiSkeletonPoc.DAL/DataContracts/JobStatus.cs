using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobStatus
    {
        public JobStatus()
        {
            Job = new HashSet<Job>();
        }

        public int JobStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
