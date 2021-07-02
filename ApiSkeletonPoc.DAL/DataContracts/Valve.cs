using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Valve
    {
        public Valve()
        {
            ValveEvent = new HashSet<ValveEvent>();
        }

        public Guid Id { get; set; }
        public string ValveId { get; set; }
        public string Qrid { get; set; }
        public string DmaName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string AssetId { get; set; }
        public string BvId { get; set; }
        public string ValveSize { get; set; }
        public string Direction { get; set; }
        public string Comment { get; set; }
        public string BvcontrolNumber { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? ModifedDate { get; set; }

        public virtual ICollection<ValveEvent> ValveEvent { get; set; }
    }
}
