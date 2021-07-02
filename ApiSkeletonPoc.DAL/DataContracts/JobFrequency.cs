using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobFrequency
    {
        public JobFrequency()
        {
            Job = new HashSet<Job>();
        }

        public int JobFrequencyId { get; set; }
        public string Frequency { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
