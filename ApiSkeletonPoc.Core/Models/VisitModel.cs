using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class VisitModel : BaseModel
    {
        public int VisitId { get; set; }
        public int? EmployeeId { get; set; }
        public string VisitBookedFlg { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitDue { get; set; }
        [Required]
        public int ContactId { get; set; }
        public EmployeeModel employeeModel { get; set; }
    }
}
