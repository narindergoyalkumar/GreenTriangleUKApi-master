using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class MwsreportRequestInfo
    {
        public int ReportRequestInfoId { get; set; }
        public string ReportType { get; set; }
        public string ReportProcessingStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsScheduled { get; set; }
        public string ReportRequestId { get; set; }
        public string GeneratedReportId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
    }
}
