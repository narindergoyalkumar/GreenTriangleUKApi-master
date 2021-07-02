using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobType
    {
        public JobType()
        {
            Job = new HashSet<Job>();
        }

        public int JobTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
