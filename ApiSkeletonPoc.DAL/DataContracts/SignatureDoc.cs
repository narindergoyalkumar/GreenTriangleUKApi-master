using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class SignatureDoc
    {
        public Guid SignatureDocId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Link { get; set; }
        public string SignatureBoxConfig { get; set; }
    }
}
