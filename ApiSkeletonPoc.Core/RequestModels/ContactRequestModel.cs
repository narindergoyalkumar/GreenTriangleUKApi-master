using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApiSkeletonPoc.Core.RequestModels
{
    public class ContactRequestModel
    {
        //Title model
        public int TitleId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        //Contact Model
        public int IndividualContactId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        public string AffiliateKey { get; set; }
        [Required]
        [MaxLength(20)]
        public string ContactType { get; set; }
        
        [MaxLength(30)]
        public string JobTitle { get; set; }


        //Organization Model
        public int OrgId { get; set; }
        [Required]
        [MaxLength(50)]
        public string OrganizationName { get; set; }


        //Client Model
        public int ClientId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientName { get; set; }

        //Visit Model
        //public int VisitId { get; set; }
        //[MaxLength(1)]
        //public string VisitBookedFlag { get; set; }
        //public int? VisitOrgId { get; set; }
        //public int? VisitIndividualContactId { get; set; }
        //public int? VisitEmployeeId { get; set; }
        //public DateTime? VisitDate { get; set; }
        //public DateTime? VisitDue { get; set; }
        //[MaxLength(20)]
        //public string VisitType { get; set; }

        //Organization address Model
        public AddressRequestModel[] OrganizationAddresses { get; set; }

        //Individual contact Address Model
        public AddressRequestModel[] ContactAddresses { get; set; }

        //Oranization social media Model
        public SocialMediaReqestModel[] OrganizationSocialMedias { get; set; }

        //Individual social media model
        public SocialMediaReqestModel[] ContactSocialMedias { get; set; }


        //Organization phone number
        public PhoneNumberRequestModel[] OrganizationPhoneNumbers { get; set; }
        // Individual phone number
        public PhoneNumberRequestModel[] ContactPhoneNumbers { get; set; }

    }
}
