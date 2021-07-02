using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class IndividualModel : BaseModel
    {
        public int IndividualId { get; set; }
        public int? TitleId { get; set; }
        public int? OrgId { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string AffiliateKey { get; set; }
   
        public string JobTitle { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string OrgName { get; set; }
    }
}
