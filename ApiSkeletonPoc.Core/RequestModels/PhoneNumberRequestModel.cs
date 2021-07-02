using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class PhoneNumberRequestModel
    {
        public int PhoneNumberId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Number { get; set; }
        [MaxLength(20)]
        public string Phone_number_type { get; set; }
    }
}
