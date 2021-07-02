using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Individual
    {
        public Individual()
        {
            Contact = new HashSet<Contact>();
        }

        public int IndividualId { get; set; }
        public int? TitleId { get; set; }
        public int? OrgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AffiliateKey { get; set; }
        public string JobTitle { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }

        public virtual Organization Org { get; set; }
        public virtual Title Title { get; set; }
        public virtual ICollection<Contact> Contact { get; set; }
    }
}
