using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Phone
    {
        public int PhoneId { get; set; }
        public string Number { get; set; }
        public int PhoneTypeId { get; set; }
        public int ContactId { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual PhoneType PhoneType { get; set; }
    }
}
