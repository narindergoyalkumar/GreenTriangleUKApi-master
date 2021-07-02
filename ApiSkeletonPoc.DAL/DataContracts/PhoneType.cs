using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class PhoneType
    {
        public PhoneType()
        {
            Phone = new HashSet<Phone>();
        }

        public int PhoneTypeId { get; set; }
        public string PhoneType1 { get; set; }

        public virtual ICollection<Phone> Phone { get; set; }
    }
}
