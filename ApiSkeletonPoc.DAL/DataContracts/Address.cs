using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string HouseNumberName { get; set; }
        public string LineOne { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public int ContactId { get; set; }
        public int AddressTypeId { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }

        public virtual AddressType AddressType { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
