using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class AddressType
    {
        public AddressType()
        {
            Address = new HashSet<Address>();
        }

        public int AddressTypeId { get; set; }
        public string AddressType1 { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
