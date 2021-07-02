using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSkeletonPoc.Core.Models
{
    public class ContactModel:BaseModel
    {

        //[Required]
        //[RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Invalid date format.")]
        //public string DateOfBirth { get; set; }

        public int ContactId { get; set; }
        public int? IndividualId { get; set; }
        public int? OrgId { get; set; }
        [Required]
        public int ContactTypeId { get; set; }
        public int ClientId { get; set; }
        public string ContactType { get; set; }
        public IndividualModel Individual { get; set; }
        public OrganizationModel Org { get; set; }
        public List<AddressModel> Address { get; set; }
        public List<PhoneModel> Phone { get; set; }
        public List<SocialMediaModel> SocialMedia { get; set; }
        public List<VisitModel> Visit { get; set; }
        public string Email { get; set; }
        public List<CustomFieldValueModel> CustomFieldValueModel { get; set; }
        public List<CustomFieldModel> CustomFieldModel { get; set; }
        public string Contact { get; set; }
        public string DueDate { get; set; }
        public bool? IsBooked { get; set; }
    }
}
