using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class JobEstimatorClientRequestModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        public int CategoryId { get; set; }
        public string Note { get; set; }

    }
}
