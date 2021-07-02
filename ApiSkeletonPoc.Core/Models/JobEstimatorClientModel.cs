using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class JobEstimatorClientModel
    {
        public int ClientId { get; set; }
        public Guid? UserId { get; set; }
        public string ClientKey { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CategoryId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
        public string Category { get; set; }
        public string Recipients { get; set; }
        public string Disclaimer { get; set; }
    }
}
