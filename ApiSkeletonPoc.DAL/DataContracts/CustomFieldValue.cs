using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class CustomFieldValue
    {
        public int CustomFieldValueId { get; set; }
        public string CustomFieldValue1 { get; set; }
        public int? CustomFieldId { get; set; }
        public int? ContactId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual CustomField CustomField { get; set; }
    }
}
