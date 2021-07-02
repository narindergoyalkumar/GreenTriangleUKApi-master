using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class CustomFieldValueModel
    {
        public int CustomFieldValueId { get; set; }
        public string CustomFieldValue { get; set; }
        public int? CustomFieldId { get; set; }
        public int? ContactId { get; set; }
        public string CustomFieldType { get; set; }
        public string VisibleTo { get; set; }
        public string CustomFieldName { get; set; }
    }
}
