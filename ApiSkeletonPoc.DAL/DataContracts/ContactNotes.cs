using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class ContactNotes
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ContactId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Type { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
