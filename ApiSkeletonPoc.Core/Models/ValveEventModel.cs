using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class ValveEventModel
    {
        public int EventId { get; set; }
        public int EventType { get; set; }
        public DateTime? DateTimeStamp { get; set; }
        public string EventDescription { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string QrId { get; set; }
        public Guid ValveId { get; set; }
        public string Type { get; set; }
        public string EngineerId { get; set; }
    }
}
