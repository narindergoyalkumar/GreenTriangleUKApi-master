using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class JobNotes
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? JobId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Type { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public virtual Job Job { get; set; }
    }
}
