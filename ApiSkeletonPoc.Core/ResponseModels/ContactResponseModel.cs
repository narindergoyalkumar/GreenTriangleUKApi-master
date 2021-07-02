using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.ResponseModels
{
    public class ContactResponseModel
    {
        //Title
        public TitleModel Title { get; set; }

        //Contact
        //public IndividualContactModel Contact { get; set; }

        // Organization
        public OrganizationModel Organization { get; set; }

        // Client
        public ClientModel Client { get; set; }

        //Visit Model
        public VisitModel Visit { get; set; }

        // Organization Address
        public IEnumerable<AddressModel> OrganizationAddresses { get; set; }

        // Inidividual Address
        public IEnumerable<AddressModel> IndividualAddresses { get; set; }

        // Organization Social media
        public IEnumerable<SocialMediaModel> OrganizationSocialMedia { get; set; }

        // Individual Social media
        public IEnumerable<SocialMediaModel> IndividualSocialMedia { get; set; }

        //Organization phone number
        public IEnumerable<PhoneModel> OrganizationPhoneNumbers { get; set; }
        // Individual phone number
        public IEnumerable<PhoneModel> ContactPhoneNumbers { get; set; }

    }
}
