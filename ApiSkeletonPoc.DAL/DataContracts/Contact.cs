using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Contact
    {
        public Contact()
        {
            Address = new HashSet<Address>();
            ContactNotes = new HashSet<ContactNotes>();
            CustomFieldValue = new HashSet<CustomFieldValue>();
            Job = new HashSet<Job>();
            Message = new HashSet<Message>();
            Phone = new HashSet<Phone>();
            SocialMedia = new HashSet<SocialMedia>();
            Visit = new HashSet<Visit>();
        }

        public int ContactId { get; set; }
        public int? IndividualId { get; set; }
        public int? OrgId { get; set; }
        public int ContactTypeId { get; set; }
        public int ClientId { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
        public string Email { get; set; }

        public virtual Client Client { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual Individual Individual { get; set; }
        public virtual Organization Org { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<ContactNotes> ContactNotes { get; set; }
        public virtual ICollection<CustomFieldValue> CustomFieldValue { get; set; }
        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual ICollection<SocialMedia> SocialMedia { get; set; }
        public virtual ICollection<Visit> Visit { get; set; }
    }
}
