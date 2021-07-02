using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class ContactNotesModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ContactId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string ContactName { get; set; }
        public string Type { get; set; }
    }
}
