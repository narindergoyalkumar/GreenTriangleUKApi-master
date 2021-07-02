using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class ValveEvent
    {
        public int EventId { get; set; }
        public int? EventType { get; set; }
        public DateTime? DateTimeStamp { get; set; }
        public string EventDescription { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public Guid? ValveId { get; set; }
        public string EngineerId { get; set; }

        public virtual ValveEventType EventTypeNavigation { get; set; }
        public virtual Valve Valve { get; set; }
    }
}
