using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;

namespace ApiSkeletonPoc.Core.Services
{
    public class AddressService : IAddressService
    {
        private readonly IBaseService<DAL.DataContracts.Address> _baseAddressService;
        private readonly ILogService _logService;

        public AddressService(IBaseService<DAL.DataContracts.Address> baseAddressService, ILogService logService)
        {
            _baseAddressService = baseAddressService;
            _logService = logService;
        }

        public int Add(AddressModel address)
        {
            int addressId = 0;

            var addedAddress = _baseAddressService.AddOrUpdate(Mapper.MapAddressModelWithAddress(address), 0);
            if (addedAddress != null)
            {
                addressId = addedAddress.AddressId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Address with Address Id {address.AddressId} added" });
            }

            return addressId;
        }

        public int Update(AddressModel address, int id)
        {
            int addressId = id;
            var data = _baseAddressService.GetById(id);
            if (data != null)
            {
                //data.AddressTypeId = address.AddressTypeId;
                data.City = address.City;
                data.Country = address.Country;
                data.HouseNumberName = address.HouseNumberName;
                data.LineOne = address.LineOne;
                data.Postcode = address.Postcode;
                data.RecordUpdatedDate = DateTime.Now;
                _baseAddressService.AddOrUpdate(data, id);
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"An Address with Address Id {address.AddressId} updated" });
            }

            return addressId;
        }

        public AddressModel Get(int id)
        {
            return Mapper.MapAddressWithAddressModel(_baseAddressService.GetById(id));
        }

        public bool Remove(int addressId)
        {
            _baseAddressService.Remove(addressId);
            return true;
        }

        public IEnumerable<DAL.DataContracts.Address> GetAddressWithContact(string address,int clientId)
        {
            string[] navigationalProps = { "Contact", "Contact.ContactType", "AddressType", "Contact.Client", "Contact.Individual", "Contact.Org", "Contact.Address", "Contact.Phone", "Contact.SocialMedia", "Contact.Visit", "Contact.Address.AddressType", "Contact.Individual.Title", "Contact.Phone.PhoneType", "Contact.SocialMedia.SocialMediaType", "Contact.ContactType", "Contact.Individual.Org", "Contact.CustomFieldValue", "Contact.CustomFieldValue.CustomField", "Contact.CustomFieldValue.CustomField.VisibleTo" };
            return _baseAddressService.Where(a => a.LineOne.Contains(address)&&a.Contact.ClientId==clientId, navigationalProps).ToList();
        }
    }
}
