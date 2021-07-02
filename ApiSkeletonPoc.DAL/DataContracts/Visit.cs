using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class Visit
    {
        public int VisitId { get; set; }
        public int? EmployeeId { get; set; }
        public string VisitBookedFlg { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitDue { get; set; }
        public int ContactId { get; set; }
        public DateTime RecordCreatedDate { get; set; }
        public DateTime RecordUpdatedDate { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
