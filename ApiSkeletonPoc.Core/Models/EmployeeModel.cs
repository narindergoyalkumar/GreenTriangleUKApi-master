using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class EmployeeModel : BaseModel
    {
     
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NiNumber { get; set; }
        public List<VisitModel>  VisitModels { get; set; }
        public int ClientId { get; set; }
    }
}
