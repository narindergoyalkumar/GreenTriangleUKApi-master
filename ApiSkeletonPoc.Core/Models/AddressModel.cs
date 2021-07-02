using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public class AddressModel : BaseModel
    {
        public int AddressId { get; set; }
       
        public string HouseNumberName { get; set; }
        public string LineOne { get; set; }
       
        public string City { get; set; }
        public string Country { get; set; }
        
        public string Postcode { get; set; }
        public int ContactId { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressType { get; set; }
    }
}
