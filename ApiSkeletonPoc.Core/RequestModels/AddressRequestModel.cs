using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class AddressRequestModel
    {
        public int AddressId { get; set; }
        [Required]
        [MaxLength(50)]
        public string House_number_name { get; set; }
        [MaxLength(50)]
        public string Line_one { get; set; }
        
        [MaxLength(20)]
        public string City { get; set; }
        [MaxLength(20)]
        public string Country { get; set; }
        [Required]
        [MaxLength(50)]
        public string Postcode { get; set; }
        [MaxLength(10)]
        public string Address_type { get; set; }
    }
}
