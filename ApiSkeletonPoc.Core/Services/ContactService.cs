using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.Repositories;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly IBaseService<Contact> _baseContactService;
        private readonly IOrganizationService _organizationService;
        private readonly IAddressService _addressService;
        private readonly IPhone_NumbersService _phoneService;
        private readonly ISocial_MediaService _socialMediaService;
        private readonly IVisitsService _visitsService;
        private readonly ILogService _logService;
        private readonly IIndividualService _individualService;
        private readonly IClientService _clientService;
        private readonly ICustomFieldValueService _customFieldValueService;
        private readonly IContactNotesService _contactNotesService;
        private readonly IJobService _jobService;
        public ContactService(ILogService logService,
            IBaseService<Contact> baseContactService,
            IOrganizationService organizationService,
            IAddressService addressService,
            IPhone_NumbersService phoneService,
            ISocial_MediaService socialMediaService, IIndividualService individualService, IVisitsService visitsService, IClientService clientService, ICustomFieldValueService customFieldValueService, IContactNotesService contactNotesService, IJobService jobService)
        {
            _logService = logService;
            _baseContactService = baseContactService;
            _organizationService = organizationService;
            _addressService = addressService;
            _phoneService = phoneService;
            _socialMediaService = socialMediaService;
            _individualService = individualService;
            _visitsService = visitsService;
            _clientService = clientService;
            _customFieldValueService = customFieldValueService;
            _contactNotesService = contactNotesService;
            _jobService = jobService;
        }
        public BaseResponseModel Add(ContactModel contactModel)
        {
            if (contactModel.ClientId > 0)
            {
                if (_clientService.IsClientExists(contactModel.ClientId))
                {
                    int contactId = 0;
                    Contact contact = null;
                    if (contactModel != null)
                    {
                        if (contactModel.ContactTypeId > 0)
                        {
                            if (contactModel.ContactTypeId == Convert.ToInt32(Enums.ContactType.Individual))
                            {
                                int individualId = _individualService.Add(contactModel.Individual);
                                if (individualId > 0)
                                {
                                    contactModel.IndividualId = individualId;
                                    contactModel.ContactTypeId = Convert.ToInt32(Enums.ContactType.Individual);
                                    contact = _baseContactService.AddOrUpdate(Mapper.MapTblContactWithContactModel(contactModel), 0);
                                    contactId = contact.ContactId;
                                }
                            }
                            else
                            {
                                int orgId = _organizationService.Add(contactModel.Org);
                                if (orgId > 0)
                                {
                                    contactModel.OrgId = orgId;
                                    contactModel.ContactTypeId = Convert.ToInt32(Enums.ContactType.Organisation);
                                    contact = _baseContactService.AddOrUpdate(Mapper.MapTblContactWithContactModel(contactModel), 0);
                                    contactId = contact.ContactId;
                                }
                            }
                            #region Custom fields value update
                            contactModel.ContactId = contactId;
                            AddUpdateCustomFieldValues(contactModel);
                            #endregion
                            return new BaseResponseModel { IsSuccess = true, Message = "Contact added successfully.", Response = contactId };
                        }

                    }
                    return new BaseResponseModel { IsSuccess = false, Message = "Contact type is required.", Response = null };
                }
            }
            return new BaseResponseModel { IsSuccess = false, Message = "Client Doesn't exist", Response = null };
        }

        public IEnumerable<object> GetAll(int clientId, out int count, int pageNum, int pageSize, bool? sortdirection = null, string sortString = null)
        {
            string[] navigationalProps = { "Client", "Individual", "Org", "Address", "Phone", "SocialMedia", "Visit", "Address.AddressType", "Individual.Title", "Phone.PhoneType", "SocialMedia.SocialMediaType", "ContactType", "Individual.Org", "CustomFieldValue", "Client.CustomField", "Client.CustomField.VisibleTo", "ContactNotes", "Client.CustomField.CustomFieldValue" };
            var contacts = _baseContactService.Get(out count, a => a.ClientId == clientId, navigationalProps, pageNum, pageSize).Select(c => Mapper.MapContactModelWithTblContact(c)).ToList().OrderByDescending(a => a.RecordCreatedDate);
            if (contacts.Any())
            {
                foreach (var item in contacts)
                {
                    item.DueDate = item.CustomFieldModel.Where(a => a.CustomFieldKey.ToLower() == "duedate").Select(a => a.Value).FirstOrDefault();
                    string isBooked = item.CustomFieldModel.Where(a => a.CustomFieldKey.ToLower() == "booked").Select(a => a.Value).FirstOrDefault();
                    //if (!string.IsNullOrEmpty(dueDate))
                    //{
                    //    item.DueDate = Convert.ToDateTime(dueDate);
                    //}
                    if (!string.IsNullOrEmpty(isBooked))
                    {
                        item.IsBooked = Convert.ToBoolean(isBooked);
                    }
                }

            }
            return contacts;
        }
        public BaseResponseModel Remove(int contactId)
        {
            bool isContactRemoved = false;
            var contact = _baseContactService.Where(a => a.ContactId == contactId, "Address", "Phone", "SocialMedia", "Visit", "ContactNotes", "Job").FirstOrDefault();
            BaseResponseModel baseResponseModel = null;
            if (contact != null)
            {
                #region Individual Deletion
                if (contact.IndividualId != null)
                {
                    _individualService.Remove(contact.IndividualId.GetValueOrDefault());
                }
                #endregion

                #region Organisatin deletion
                if (contact.OrgId != null)
                {
                    _organizationService.Remove(contact.OrgId.GetValueOrDefault());
                }
                #endregion

                //delete custom field values
                var customFiledValuesList = _customFieldValueService.CustomFieldValuesByContact(contact.ContactId);
                if (customFiledValuesList.Any())
                {
                    foreach (var customfieldValue in customFiledValuesList)
                    {
                        _customFieldValueService.Delete(customfieldValue);
                    }
                }
                //delete contact addresses
                if (contact.Address.Any())
                {
                    foreach (var address in contact.Address.ToList())
                    {
                        _addressService.Remove(address.AddressId);
                        _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"An address with address Id {address.AddressId} associated with contact {contactId} deleted" });
                    }
                }

                if (contact.Phone.Any())
                {
                    foreach (var phone in contact.Phone.ToList())
                    {
                        _phoneService.Remove(phone.PhoneId);
                        _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A phone with phone Id {phone.PhoneId} associated with contact {contactId} deleted" });
                    }
                }



                if (contact.SocialMedia.Any())
                {
                    foreach (var socialMedia in contact.SocialMedia.ToList())
                    {
                        _socialMediaService.Remove(socialMedia.SocialMediaId);
                        _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A social media with Id {socialMedia.SocialMediaId} associated with contact {contactId} deleted" });
                    }
                }
                if (contact.Visit.Any())
                {
                    foreach (var visit in contact.Visit.ToList())
                    {
                        _visitsService.Remove(visit.VisitId);
                        _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A visit with Id {visit.VisitId} associated with contact {contactId} deleted" });
                    }
                }

                if (contact.ContactNotes.Any())
                {
                    _contactNotesService.DeleteAllContactNotes(contact.ContactNotes.ToList());
                }
                if (contact.Job.Any())
                {
                    foreach (var job in contact.Job)
                    {
                        _jobService.Delete(job.JobId);
                    }
                }
                _baseContactService.Remove(contact.ContactId);
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new contact with contact Id {contactId} deleted" });
                isContactRemoved = true;
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Response = isContactRemoved, Message = "Contact removed successfully." };
            }
            return baseResponseModel;
        }
        public BaseResponseModel Get(int id, int clientId)
        {
            string[] navigationalProps = { "Client", "Individual", "Org", "Address", "Phone", "SocialMedia", "Visit", "Address.AddressType", "Individual.Title", "Phone.PhoneType", "SocialMedia.SocialMediaType", "ContactType", "Individual.Org", "CustomFieldValue", "Client.CustomField", "Client.CustomField.VisibleTo", "ContactNotes", "Client.CustomField.CustomFieldValue" };
            return new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = Mapper.MapContactModelWithTblContact(_baseContactService.Where(a => a.ContactId == id && a.ClientId == clientId, navigationalProps).FirstOrDefault()) };
        }

        public BaseResponseModel Update(ContactModel contactModel)
        {

            if (contactModel.ContactTypeId == Convert.ToInt32(Enums.ContactType.Individual))
            {
                if (contactModel.Individual?.IndividualId > 0)
                {
                    _individualService.Update(contactModel.Individual, contactModel.Individual.IndividualId);
                }
            }
            else
            {
                if (contactModel.Org?.OrgId > 0)
                {
                    _organizationService.Update(contactModel.Org, contactModel.Org.OrgId);
                }
            }
            var contact = _baseContactService.GetById(contactModel.ContactId);
            if (contact != null)
            {
                contact.Email = contactModel.Email;
                _baseContactService.AddOrUpdate(contact, contact.ContactId);

                #region Address update
                if (contactModel.Address.Any())
                {
                    foreach (var address in contactModel.Address)
                    {
                        if (address.AddressId > 0)
                        {
                            //address.ContactId = contact.ContactId;
                            _addressService.Update(address, address.AddressId);
                        }
                    }
                }
                #endregion

                #region Phone update

                if (contactModel.Phone.Any())
                {
                    foreach (var phone in contactModel.Phone)
                    {
                        if (phone.PhoneId > 0)
                        {
                            //phone.ContactId = contact.ContactId;
                            _phoneService.Update(phone, phone.PhoneId);
                        }
                    }
                }
                #endregion

                #region Social Media update
                if (contactModel.SocialMedia != null)
                {
                    if (contactModel.SocialMedia.Any())
                    {
                        foreach (var socialMedia in contactModel.SocialMedia)
                        {
                            //socialMedia.ContactId = contact.ContactId;
                            if (socialMedia.SocialMediaId > 0)
                            {
                                _socialMediaService.Update(socialMedia, socialMedia.SocialMediaId);
                            }

                        }
                    }
                }

                #endregion

                #region Custom fields value update
                AddUpdateCustomFieldValues(contactModel);
                #endregion
                return new BaseResponseModel { IsSuccess = true, Message = "Contact updated successfully.", Response = true };
            }




            return new BaseResponseModel { };
        }

        private void AddUpdateCustomFieldValues(ContactModel contactModel)
        {
            if (contactModel.CustomFieldModel != null)
            {
                if (contactModel.CustomFieldModel.Any())
                {
                    foreach (var cusFldVal in contactModel.CustomFieldModel)
                    {
                        var customFieldValue = _customFieldValueService.CustomFieldValuesByCustomFieldIdAndContactId(cusFldVal.CustomFieldId, contactModel.ContactId);
                        if (customFieldValue != null)
                        {
                            if (customFieldValue.Any())
                            {
                                foreach (var item in customFieldValue)
                                {
                                    item.CustomFieldValue1 = cusFldVal.Value;
                                    _customFieldValueService.Update(Mapper.MapCustomFieldValueModelWithCustomFieldValue(item), item.CustomFieldValueId);
                                }

                            }
                            else
                            {
                                CustomFieldValueModel customField = new CustomFieldValueModel
                                {
                                    CustomFieldValue = cusFldVal.Value,
                                    CustomFieldId = cusFldVal.CustomFieldId,
                                    ContactId = contactModel.ContactId,
                                };
                                _customFieldValueService.Add(customField);
                            }
                        }
                        //if (cusFldVal.cus > 0)
                        //{

                    }
                }
            }
        }

        public BaseResponseModel Import(List<ContactModel> contactModels, int clientId)
        {
            if (contactModels.Count > 0)
            {
                contactModels.ForEach(c => c.ClientId = clientId);
                IEnumerable<Contact> contacts = contactModels.Select(a => Mapper.MapTblContactWithContactModel(a)).ToList();
                _baseContactService.InsertBulk(contacts);
                int importedContactsCount = contactModels.Count();
                return new BaseResponseModel { IsSuccess = true, Message = $"{importedContactsCount} contacts imported.", Response = importedContactsCount };
            }
            return new BaseResponseModel { IsSuccess = true, Message = "No contacts imported.", Response = 0 };
        }

        public void BulkDelete(List<int> ids)
        {
            foreach (var id in ids)
            {
                Remove(id);
            }
        }

        public IEnumerable<object> SearchContactByAddress(string address, int clientId)
        {
            var addressWithContact = _addressService.GetAddressWithContact(address, clientId);
            List<ContactModel> contactModels = new List<ContactModel>();
            foreach (var item in addressWithContact)
            {
                contactModels.Add(Mapper.MapContactModelWithTblContact(item.Contact));
            }
            return contactModels;
        }

        public IEnumerable<object> GetCapsuleContacts(int pageNum, int pageSize)
        {
            string[] navigationalProps = { "Client", "Individual", "Org", "Address", "Phone", "SocialMedia", "Visit", "Address.AddressType", "Individual.Title", "Phone.PhoneType", "SocialMedia.SocialMediaType", "ContactType", "Individual.Org", "CustomFieldValue", "Client.CustomField", "Client.CustomField.VisibleTo", "ContactNotes", "Client.CustomField.CustomFieldValue" };
            var contacts = _baseContactService.Get(out int count, a=>a.ContactTypeId==(int)Enums.ContactType.Individual, navigationalProps, pageNum, pageSize).Select(c => Mapper.MapContactWithCapsule(c)).ToList().OrderByDescending(a => a.CreatedAt);
            return contacts;
        }
    }
}