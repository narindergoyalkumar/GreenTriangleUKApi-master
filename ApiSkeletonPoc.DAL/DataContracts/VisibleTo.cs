using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class VisibleTo
    {
        public VisibleTo()
        {
            CustomField = new HashSet<CustomField>();
        }

        public int VisibleToId { get; set; }
        public string VisibleTo1 { get; set; }

        public virtual ICollection<CustomField> CustomField { get; set; }
    }
}
