using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class DeliveryStatusType
    {
        public DeliveryStatusType()
        {
            Message = new HashSet<Message>();
        }

        public int DeliveryStatusTypeId { get; set; }
        public string DeliveryStatus { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Message> Message { get; set; }
    }
}
