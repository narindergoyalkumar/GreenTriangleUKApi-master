using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class ValveEventType
    {
        public ValveEventType()
        {
            ValveEvent = new HashSet<ValveEvent>();
        }

        public int ValveEventTypeId { get; set; }
        public string Type { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<ValveEvent> ValveEvent { get; set; }
    }
}
