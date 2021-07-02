using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class CustomFieldModel
    {
        public int CustomFieldId { get; set; }
        public int? CustomFieldTypeId { get; set; }
        public int? VisibleToId { get; set; }
        public int? FieldOrder { get; set; }
        public int? ClientId { get; set; }
        public string CustomFieldName { get; set; }
        public string VisibleTo { get; set; }
        public string CustomFieldType { get; set; }
        public string CustomFieldControlType { get; set; }
        public string CustomFieldKey { get; set; }
        public bool? IsRequired { get; set; }
        public string Value { get; set; }
        public int ContactId { get; set; }
        public string Options { get; set; }
        //public CustomFieldValueModel CustomFieldValueModels { get; set; }
    }
}
