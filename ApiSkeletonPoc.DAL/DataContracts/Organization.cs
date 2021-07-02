using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Organization
    {
        public Organization()
        {
            Contact = new HashSet<Contact>();
            Individual = new HashSet<Individual>();
        }

        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
        public virtual ICollection<Individual> Individual { get; set; }
    }
}
