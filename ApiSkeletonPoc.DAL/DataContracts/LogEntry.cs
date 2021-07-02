using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class LogEntry
    {
        public int Id { get; set; }
        public string LogText { get; set; }
        public DateTime LoggedDateTime { get; set; }
    }
}
