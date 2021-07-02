using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class EmployeeRequestModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(200)]
        public string NI_Number { get; set; }
    }
}
