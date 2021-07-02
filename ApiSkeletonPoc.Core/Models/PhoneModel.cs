using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public  class PhoneModel : BaseModel
    {
        public int PhoneId { get; set; }
        public string Number { get; set; }
        public int PhoneTypeId { get; set; }
        public int ContactId { get; set; }
        public string PhoneType { get; set; }
    }
}
