using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Client
    {
        public Client()
        {
            ClientModuleMapping = new HashSet<ClientModuleMapping>();
            Contact = new HashSet<Contact>();
            CustomField = new HashSet<CustomField>();
            Employee = new HashSet<Employee>();
            User = new HashSet<User>();
        }

        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }
        public int TotalTextsCount { get; set; }
        public int RemainingTextsCount { get; set; }

        public virtual ICollection<ClientModuleMapping> ClientModuleMapping { get; set; }
        public virtual ICollection<Contact> Contact { get; set; }
        public virtual ICollection<CustomField> CustomField { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
