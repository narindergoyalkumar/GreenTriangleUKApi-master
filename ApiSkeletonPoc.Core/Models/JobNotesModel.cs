using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class JobNotesModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? JobId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Type { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
