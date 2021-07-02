using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class ContactType
    {
        public ContactType()
        {
            Contact = new HashSet<Contact>();
        }

        public int ContactTypeId { get; set; }
        public string ContactType1 { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
    }
}
