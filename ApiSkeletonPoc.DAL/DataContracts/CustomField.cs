using System;
using System.Collections.Generic;

namespace ApiSkeletonPoc.DAL.DataContracts
{
    public partial class CustomField
    {
        public CustomField()
        {
            CustomFieldValue = new HashSet<CustomFieldValue>();
        }

        public int CustomFieldId { get; set; }
        public string CustomFieldName { get; set; }
        public int? VisibleToId { get; set; }
        public int? FieldOrder { get; set; }
        public string CustomFieldKey { get; set; }
        public string Type { get; set; }
        public string ControlType { get; set; }
        public bool? IsRequired { get; set; }
        public int? ClientId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Options { get; set; }

        public virtual Client Client { get; set; }
        public virtual VisibleTo VisibleTo { get; set; }
        public virtual ICollection<CustomFieldValue> CustomFieldValue { get; set; }
    }
}
