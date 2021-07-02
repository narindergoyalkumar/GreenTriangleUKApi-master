using ApiSkeletonPoc.Core.Common;
using ApiSkeletonPoc.Core.Extensions;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Insfrastucture
{
    public class Mapper
    {
        public static Contact MapTblContactWithContactModel(ContactModel contactModel)
        {
            if (contactModel == null)
                return null;
            return new Contact
            {
                ClientId = contactModel.ClientId,
                RecordCreatedDate = DateTime.Now,
                RecordUpdatedDate = DateTime.Now,
                ContactTypeId = contactModel.ContactTypeId,
                IndividualId = contactModel.IndividualId,
                OrgId = contactModel.OrgId,
                Email = contactModel.Email,
                Address = contactModel.Address?.Select(a => new DAL.DataContracts.Address
                {
                    AddressTypeId = a.AddressTypeId,
                    City = a.City,
                    Country = a.Country,
                    HouseNumberName = a.HouseNumberName,
                    LineOne = a.LineOne,
                    Postcode = a.Postcode,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now
                }).ToList(),
                Individual = contactModel.ContactTypeId == Convert.ToInt32(Enums.ContactType.Individual) ? new Individual
                {
                    AffiliateKey = contactModel.Individual?.AffiliateKey,
                    RecordUpdatedDate = DateTime.Now,
                    RecordCreatedDate = DateTime.Now,
                    FirstName = contactModel.Individual?.FirstName,
                    JobTitle = contactModel.Individual?.JobTitle,
                    LastName = contactModel.Individual?.LastName,
                    OrgId = contactModel.Individual?.OrgId,
                    TitleId = contactModel.Individual?.TitleId
                } : null,
                Org = contactModel.ContactTypeId == Convert.ToInt32(Enums.ContactType.Organisation) ? new Organization
                {
                    OrgName = contactModel.Org?.OrgName,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                } : null,
                Phone = contactModel.Phone?.Select(p => new Phone
                {
                    ContactId = p.ContactId,
                    RecordUpdatedDate = DateTime.Now,
                    RecordCreatedDate = DateTime.Now,
                    Number = p.Number,
                    PhoneTypeId = p.PhoneTypeId
                }).ToList(),
                SocialMedia = contactModel.SocialMedia?.Select(s => new SocialMedia
                {
                    ContactId = s.ContactId,
                    Image = s.Image,
                    Link = s.Link,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                    SocialMediaTypeId = s.SocialMediaTypeId
                }).ToList(),
                Visit = contactModel.Visit?.Select(v => new Visit
                {
                    EmployeeId = v.EmployeeId,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                    VisitBookedFlg = v.VisitBookedFlg,
                    VisitDate = v.VisitDate,
                    VisitDue = v.VisitDue,
                    ContactId = v.ContactId
                }).ToList(),
                CustomFieldValue = contactModel.CustomFieldValueModel?.Select(c => new CustomFieldValue
                {
                    ContactId = c.ContactId,
                    CustomFieldId = c.CustomFieldId,
                    CustomFieldValue1 = c.CustomFieldValue
                }).ToList()
            };
        }

        public static Employee MapEmployeeModelWithEmployee(EmployeeModel employee)
        {
            return employee == null
                ? null
                : new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    NiNumber = employee.NiNumber,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                    ClientId = employee.ClientId
                };
        }

        public static EmployeeModel MapEmployeeWithEmployeeModel(Employee employee)
        {
            return employee == null
                ? null
                : new EmployeeModel
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    NiNumber = employee.NiNumber,
                    VisitModels = employee.Visit.Select(x => new VisitModel
                    {
                        ContactId = x.ContactId,
                        EmployeeId = x.EmployeeId,
                        RecordCreatedDate = x.RecordCreatedDate,
                        RecordUpdatedDate = x.RecordUpdatedDate,
                        VisitBookedFlg = x.VisitBookedFlg,
                        VisitDate = x.VisitDate,
                        VisitDue = x.VisitDue,
                        VisitId = x.VisitId
                    }).ToList(),
                    RecordCreatedDate = employee.RecordCreatedDate,
                    RecordUpdatedDate = employee.RecordUpdatedDate,
                    ClientId = employee.ClientId
                };
        }

        public static ContactModel MapContactModelWithTblContact(Contact contact)
        {
            if (contact == null)
                return null;
            return new ContactModel
            {
                ContactId = contact.ContactId,
                ClientId = contact.ClientId,
                ContactTypeId = contact.ContactTypeId,
                IndividualId = contact.IndividualId,
                OrgId = contact.OrgId,
                RecordCreatedDate = contact.RecordCreatedDate,
                RecordUpdatedDate = contact.RecordUpdatedDate,
                ContactType = contact.ContactType?.ContactType1,
                Email = contact.Email,
                Address = contact.Address?.Select(a => new AddressModel
                {
                    AddressId = a.AddressId,
                    AddressType = a.AddressType.AddressType1,
                    AddressTypeId = a.AddressTypeId,
                    City = a.City,
                    Country = a.Country,
                    HouseNumberName = a.HouseNumberName,
                    LineOne = a.LineOne,
                    Postcode = a.Postcode,
                    RecordCreatedDate = a.RecordCreatedDate,
                    RecordUpdatedDate = a.RecordUpdatedDate
                }).ToList(),
                Individual = contact.ContactTypeId == Convert.ToInt32(Enums.ContactType.Individual) ? new IndividualModel
                {
                    AffiliateKey = contact.Individual?.AffiliateKey,
                    RecordUpdatedDate = contact.Individual?.RecordUpdatedDate,
                    RecordCreatedDate = contact.Individual?.RecordCreatedDate,
                    FirstName = contact.Individual?.FirstName,
                    IndividualId = contact.Individual.IndividualId,
                    JobTitle = contact.Individual?.JobTitle,
                    LastName = contact.Individual?.LastName,
                    OrgId = contact.Individual?.Org?.OrgId,
                    Title = contact.Individual?.Title?.Title1,
                    FullName = contact.Individual?.FirstName + " " + contact.Individual?.LastName,
                    OrgName = contact.Individual?.Org?.OrgName,
                    TitleId = contact.Individual?.TitleId
                } : null,
                Org = contact.ContactTypeId == Convert.ToInt32(Enums.ContactType.Organisation) ? new OrganizationModel
                {
                    OrgId = contact.Org.OrgId,
                    OrgName = contact.Org.OrgName,
                    RecordCreatedDate = contact.Org.RecordCreatedDate,
                    RecordUpdatedDate = contact.Org.RecordUpdatedDate,
                } : null,
                Phone = contact.Phone?.Select(p => new PhoneModel
                {
                    ContactId = p.ContactId,
                    RecordUpdatedDate = p.RecordUpdatedDate,
                    RecordCreatedDate = p.RecordCreatedDate,
                    Number = p.Number,
                    PhoneType = p.PhoneType.PhoneType1,
                    PhoneId = p.PhoneId,
                    PhoneTypeId = p.PhoneTypeId
                }).ToList(),
                SocialMedia = contact.SocialMedia?.Select(s => new SocialMediaModel
                {
                    ContactId = s.ContactId,
                    Image = s.Image,
                    Link = s.Link,
                    RecordCreatedDate = s.RecordCreatedDate,
                    RecordUpdatedDate = s.RecordUpdatedDate,
                    SocialMediaId = s.SocialMediaId,
                    SocialMediaType = s.SocialMediaType.SocialMediaType1,
                }).ToList(),
                Visit = contact.Visit?.Select(v => new VisitModel
                {
                    EmployeeId = v.EmployeeId,
                    RecordCreatedDate = v.RecordCreatedDate,
                    RecordUpdatedDate = v.RecordUpdatedDate,
                    VisitBookedFlg = v.VisitBookedFlg,
                    VisitDate = v.VisitDate,
                    VisitDue = v.VisitDue,
                    VisitId = v.VisitId,
                    ContactId = v.ContactId
                }).ToList(),
                Contact = contact.ContactTypeId == Convert.ToInt32(Enums.ContactType.Individual) ? contact.Individual?.FirstName + " " + contact.Individual?.LastName : contact.Org?.OrgName,
                //CustomFieldValueModel = contact?.CustomFieldValue?.Select(cus => new CustomFieldValueModel
                //{
                //    CustomFieldType = cus.CustomField?.Type,
                //    CustomFieldValue = cus.CustomFieldValue1,
                //    VisibleTo = cus.CustomField?.VisibleTo?.VisibleTo1,
                //    CustomFieldName = cus.CustomField?.CustomFieldName,
                //    CustomFieldId=cus.CustomFieldId,
                //    CustomFieldValueId=cus.CustomFieldValueId,
                //    ContactId=cus.ContactId,
                //}).ToList()
                CustomFieldModel = contact?.Client?.CustomField?.Select(cus => new CustomFieldModel
                {
                    CustomFieldControlType = cus.ControlType,
                    CustomFieldId = cus.CustomFieldId,
                    CustomFieldName = cus.CustomFieldName,
                    CustomFieldType = cus.Type,
                    FieldOrder = cus.FieldOrder,
                    VisibleTo = cus.VisibleTo?.VisibleTo1,
                    VisibleToId = cus.VisibleToId,
                    //CustomFieldValueModels = MapCustomFieldValueModelWithCustomFieldValue(cus.CustomFieldValue?.Where(a => a.ContactId == contact.ContactId && a.CustomFieldId == cus.CustomFieldId).FirstOrDefault())
                    Value = cus.CustomFieldValue?.Where(a => a.ContactId == contact.ContactId && a.CustomFieldId == cus.CustomFieldId).FirstOrDefault()?.CustomFieldValue1,
                    IsRequired = cus.IsRequired,
                    CustomFieldKey = cus.CustomFieldKey,
                    ContactId = contact.ContactId,
                    Options = cus.Options
                }).ToList()
            };
        }

        public static Party MapContactWithCapsule(Contact contact)
        {
            if (contact == null)
                return null;
            return new Party
            {
                Addresses = contact.Address?.Select(a => new Models.Address
                {
                    Id = a.AddressId,
                    Type = a.AddressType?.AddressType1,
                    City = a.City,
                    Country = a.Country,
                    Street = a.LineOne,
                    Zip = a.Postcode
                }).ToArray(),
                PhoneNumbers = contact.Phone?.Select(p => new PhoneNumber
                {
                    Id = p.PhoneId,
                    Number = p.Number,
                    Type = p.PhoneType.PhoneType1
                }).ToArray(),
                CreatedAt = contact.RecordCreatedDate,
                Fields = contact?.Client?.CustomField?.Select(cus => new Field
                {
                    Definition = new Definition { Id = cus.CustomFieldId, Name = cus.CustomFieldName },
                    Value = cus.CustomFieldValue?.Where(a => a.ContactId == contact.ContactId && a.CustomFieldId == cus.CustomFieldId).FirstOrDefault()?.CustomFieldValue1,
                    Id = cus.CustomFieldId
                }).ToArray(),
                FirstName = contact.Individual.FirstName,
                Id = contact.ContactId,
                JobTitle = contact.Individual.JobTitle,
                LastName = contact.Individual.LastName,
                Organisation = contact.Individual?.Org?.OrgName,
                Title = contact.Individual.Title.Title1,
                Type = "person",
                UpdatedAt = contact.RecordUpdatedDate,
                EmailAddresses = new EmailAddress[] { new EmailAddress { Address = contact.Email } },
            };
        }

        public static Organization MapOrganizationWithOrganizationModel(OrganizationModel orgModel)
        {
            if (orgModel == null)
                return null;
            return new Organization
            {
                OrgName = orgModel.OrgName,
                RecordCreatedDate = DateTime.Now,
                RecordUpdatedDate = DateTime.Now
            };
        }


        private static DAL.DataContracts.Address MapOrgAddressWithOrgAddressModel(AddressModel address)
        {
            return address == null
                ? null
                : new DAL.DataContracts.Address
                {
                    AddressTypeId = address.AddressTypeId,
                    City = address.City,
                    ContactId = address.ContactId,
                    Country = address.Country,
                    HouseNumberName = address.HouseNumberName,
                    LineOne = address.LineOne,
                    Postcode = address.Postcode,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now
                };
        }

        private static AddressModel MapOrgAddressModelWithOrgAddress(DAL.DataContracts.Address address)
        {
            return address == null
                ? null
                : new AddressModel
                {
                    AddressId = address.AddressId,
                    AddressTypeId = address.AddressTypeId,
                    City = address.City,
                    ContactId = address.ContactId,
                    Country = address.Country,
                    HouseNumberName = address.HouseNumberName,
                    LineOne = address.LineOne,
                    Postcode = address.Postcode,
                    RecordCreatedDate = address.RecordCreatedDate,
                    RecordUpdatedDate = address.RecordUpdatedDate
                };
        }

        public static TitleModel MapTitleWithTitleModel(Title titleModel)
        {
            return titleModel == null
                ? null
                : new TitleModel
                {
                    Title = titleModel.Title1,
                    TitleId = titleModel.TitleId
                };
        }

        public static LogEntry MapTblLogEntryWithLogEntryModel(LogEntryModel logEntryModel)
        {
            if (logEntryModel == null)
                return null;
            return new LogEntry
            {
                LoggedDateTime = DateTime.Now,
                LogText = logEntryModel.LogText
            };
        }
        public static LogEntryModel MapLogEntryModelWithTblLogEntry(LogEntry log)
        {
            if (log == null)
                return null;
            return new LogEntryModel
            {
                LoggedDateTime = log.LoggedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                LogText = log.LogText
            };
        }
        public static UserModel MapUserWithUserModel(User user)
        {
            if (user == null)
                return null;
            return new UserModel
            {
                ClientId = user.ClientId,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                IsActive = user.IsActive,
                Role = user.UserRoleMapping?.ToList().FirstOrDefault()?.UserRole?.Role,
                SubscribedModules = user.Client?.ClientModuleMapping?.Select(a => a.ModuleId).ToList(),
                Client = user.Client?.Name
            };
        }
        public static User MapUserModelWithUser(UserModel user)
        {
            if (user == null)
                return null;
            return new User
            {
                ClientId = user.ClientId,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public static UserRoleMapping MapUserRoleMappingModelWithUserRoleMapping(UserRoleMappingModel userRoleMappingModel)
        {
            if (userRoleMappingModel == null)
                return null;
            return new UserRoleMapping
            {
                IsActive = true,
                UserId = userRoleMappingModel.UserId,
                UserRoleId = userRoleMappingModel.UserRoleId
            };
        }
        public static UserModuleMapping MapUserModuleMappingModelWithUserModuleMapping(UserModuleMappingModel userModuleMappingModel)
        {
            if (userModuleMappingModel == null)
                return null;
            return new UserModuleMapping
            {
                CreatedDate = DateTime.Now,
                ModuleId = userModuleMappingModel.ModuleId,
                UserId = userModuleMappingModel.UserId
            };
        }
        public static UserModuleMappingModel MapUserModuleMappingWithUserModuleMappingModel(UserModuleMapping userModuleMapping)
        {
            if (userModuleMapping == null)
                return null;
            return new UserModuleMappingModel
            {
                CreatedDate = userModuleMapping.CreatedDate,
                ModuleId = userModuleMapping.ModuleId,
                ModuleName = userModuleMapping.Module?.DisplayName,
                UserId = userModuleMapping.UserId,
                UserModuleMappingId = userModuleMapping.UserModuleMappingId
            };
        }

        public static OrganizationModel MapOrganizationModelWithOrganization(Organization organization)
        {
            return organization == null
                ? null
                : new OrganizationModel
                {
                    OrgId = organization.OrgId,
                    OrgName = organization.OrgName,
                    RecordCreatedDate = organization.RecordCreatedDate,
                    RecordUpdatedDate = organization.RecordUpdatedDate
                };
        }

        public static DAL.DataContracts.Address MapAddressModelWithAddress(AddressModel address)
        {
            return address == null
                ? null
                : new DAL.DataContracts.Address
                {
                    AddressTypeId = address.AddressTypeId,
                    City = address.City,
                    ContactId = address.ContactId,
                    Country = address.Country,
                    HouseNumberName = address.HouseNumberName,
                    LineOne = address.LineOne,
                    Postcode = address.Postcode,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                };
        }

        public static AddressModel MapAddressWithAddressModel(DAL.DataContracts.Address address)
        {
            return address == null
                ? null
                : new AddressModel
                {
                    Postcode = address.Postcode,
                    City = address.City,
                    Country = address.Country,
                    AddressId = address.AddressId,
                    AddressTypeId = address.AddressTypeId,
                    ContactId = address.ContactId,
                    HouseNumberName = address.HouseNumberName,
                    LineOne = address.LineOne,
                    RecordCreatedDate = address.RecordCreatedDate,
                    RecordUpdatedDate = address.RecordUpdatedDate
                };
        }

        public static Phone MapPhoneNumberModelWithPhoneNumber(PhoneModel phoneNumber)
        {
            return phoneNumber == null
                ? null
                : new Phone
                {
                    ContactId = phoneNumber.ContactId,
                    Number = phoneNumber.Number,
                    PhoneTypeId = phoneNumber.PhoneTypeId,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now
                };
        }

        public static PhoneModel MapPhoneNumberWithPhoneNumberModel(Phone phoneNumber)
        {
            return phoneNumber == null
                ? null
                : new PhoneModel
                {
                    PhoneId = phoneNumber.PhoneId,
                    Number = phoneNumber.Number,
                    PhoneTypeId = phoneNumber.PhoneTypeId,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                    ContactId = phoneNumber.ContactId
                };
        }

        public static SocialMedia MapSocialMediaModelWithSocialMedia(SocialMediaModel model)
        {
            return model == null
                ? null
                : new SocialMedia
                {
                    SocialMediaId = model.SocialMediaId,
                    Image = model.Image,
                    Link = model.Link,
                    SocialMediaTypeId = model.SocialMediaTypeId,
                    ContactId = model.ContactId,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now
                };
        }

        public static SocialMediaModel MapSocialMediaWithSocialMediaModel(SocialMediaModel model)
        {
            return model == null
                ? null
                : new SocialMediaModel
                {
                    SocialMediaId = model.SocialMediaId,
                    Image = model.Image,
                    Link = model.Link,
                    SocialMediaTypeId = model.SocialMediaTypeId,
                    RecordCreatedDate = DateTime.Now,
                    ContactId = model.ContactId,
                    RecordUpdatedDate = model.RecordUpdatedDate
                };
        }

        public static Client MapClientModelWithClient(ClientModel clientModel)
        {
            return clientModel == null
                ? null
                : new Client
                {
                    Name = clientModel.Name,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                    RemainingTextsCount = clientModel.RemainingTextsCount,
                    TotalTextsCount = clientModel.RemainingTextsCount
                };
        }

        public static ClientModel MapClientWithClientModel(Client client)
        {
            return client == null
                ? null
                : new ClientModel
                {
                    ClientId = client.ClientId,
                    Name = client.Name,
                    RecordCreatedDate = client.RecordCreatedDate,
                    RecordUpdatedDate = client.RecordUpdatedDate,
                    RemainingTextsCount = client.RemainingTextsCount,
                    // TotalTextsCount = client.TotalTextsCount
                    //RemainingTexts = client.TextsRemaining
                };
        }
        public static Title MapTitleModelWithTitle(TitleModel titleModel)
        {
            return titleModel == null
                ? null
                : new Title
                {
                    Title1 = titleModel.Title,
                };
        }

        public static Visit MapVisitModelWithVisit(VisitModel visit)
        {
            return visit == null
                ? null
                : new Visit
                {
                    ContactId = visit.ContactId,
                    EmployeeId = visit.EmployeeId,
                    RecordCreatedDate = DateTime.Now,
                    RecordUpdatedDate = DateTime.Now,
                    VisitBookedFlg = visit.VisitBookedFlg,
                    VisitDate = visit.VisitDate,
                    VisitDue = visit.VisitDue,
                };
        }

        public static VisitModel MapVisitWithVisitModel(Visit visit)
        {
            return visit == null
                ? null
                : new VisitModel
                {
                    ContactId = visit.ContactId,
                    EmployeeId = visit.EmployeeId,
                    RecordCreatedDate = visit.RecordCreatedDate,
                    RecordUpdatedDate = visit.RecordUpdatedDate,
                    VisitBookedFlg = visit.VisitBookedFlg,
                    VisitDate = visit.VisitDate,
                    VisitDue = visit.VisitDue,
                    VisitId = visit.VisitId
                };
        }
        public static Individual MapIndividualWithIndividualModel(IndividualModel individualModel)
        {
            return individualModel == null ? null : new Individual
            {
                AffiliateKey = individualModel.AffiliateKey,
                FirstName = individualModel.FirstName,
                JobTitle = individualModel.JobTitle,
                LastName = individualModel.LastName,
                OrgId = individualModel.OrgId,
                RecordCreatedDate = DateTime.Now,
                RecordUpdatedDate = DateTime.Now,
                TitleId = individualModel.TitleId
            };
        }
        public static IndividualModel MapIndividualModelWithIndividual(Individual individual)
        {
            return individual == null ? null : new IndividualModel
            {
                AffiliateKey = individual.AffiliateKey,
                FirstName = individual.FirstName,
                JobTitle = individual.JobTitle,
                LastName = individual.LastName,
                OrgId = individual.OrgId,
                RecordCreatedDate = individual.RecordCreatedDate,
                RecordUpdatedDate = individual.RecordUpdatedDate,
                TitleId = individual.TitleId,
                Title = individual.Title?.Title1
            };
        }
        public static Message MapMessageModelWithMessage(MessageModel messageModel)
        {
            return messageModel == null ? null : new Message
            {
                ContactId = messageModel.ContactId,
                MessageId = messageModel.MessageId,
                Receiver = messageModel.Receiver,
                ReferenceId = messageModel.ReferenceId,
                Sender = messageModel.Sender,
                SentDateTime = DateTime.Now,
                Text = messageModel.Text,
                TextMagicMessageId = messageModel.TextMagicMessageId
            };
        }

        public static CustomField MapCustomFieldModelWithCustomField(CustomFieldModel customFieldModel)
        {
            return customFieldModel == null ? null : new CustomField
            {
                ClientId = customFieldModel.ClientId,
                FieldOrder = customFieldModel.FieldOrder,
                VisibleToId = customFieldModel.VisibleToId,
                CustomFieldName = customFieldModel.CustomFieldName,
                CreatedDate = DateTime.Now,
                ControlType = customFieldModel.CustomFieldControlType,
                CustomFieldKey = customFieldModel.CustomFieldKey,
                IsRequired = customFieldModel.IsRequired,
                Type = customFieldModel.CustomFieldType,
                Options = customFieldModel.Options
            };
        }
        public static CustomFieldModel MapCustomFieldWithCustomFieldModel(CustomField customField)
        {
            return customField == null ? null : new CustomFieldModel
            {
                ClientId = customField.ClientId,
                CustomFieldType = customField?.Type,
                FieldOrder = customField.FieldOrder,
                CustomFieldName = customField.CustomFieldName,
                VisibleTo = customField.VisibleTo?.VisibleTo1,
                CustomFieldId = customField.CustomFieldId,
                //CustomFieldValueModels = MapCustomFieldValueModelWithCustomFieldValue(customField.CustomFieldValue.Where(a => a.CustomFieldId == customField.CustomFieldId).FirstOrDefault()),
                IsRequired = customField.IsRequired,
                CustomFieldControlType = customField.ControlType,
                CustomFieldKey = customField.CustomFieldKey,
                Value = customField.CustomFieldValue.Where(a => a.CustomFieldId == customField.CustomFieldId).FirstOrDefault()?.CustomFieldValue1,
                Options = customField.Options
            };
        }

        public static CustomFieldValueModel MapCustomFieldValueModelWithCustomFieldValue(CustomFieldValue customFieldValue)
        {
            return customFieldValue == null ? null : new CustomFieldValueModel
            {
                ContactId = customFieldValue.ContactId,
                CustomFieldId = customFieldValue.CustomFieldId,
                CustomFieldValue = customFieldValue.CustomFieldValue1
            };
        }
        public static CustomFieldValue MapCustomFieldValueWithCustomFieldValueModel(CustomFieldValueModel customFieldValue)
        {
            return customFieldValue == null ? null : new CustomFieldValue
            {
                ContactId = customFieldValue.ContactId,
                CustomFieldId = customFieldValue.CustomFieldId,
                CustomFieldValue1 = customFieldValue.CustomFieldValue,
            };
        }
        public static MwsProductsModel MapAmazonProductsWithMWSProductsModel(AmazonProducts amazonProducts)
        {
            return amazonProducts == null ? null : new MwsProductsModel
            {
                Asin = amazonProducts.Asin,
                Id = amazonProducts.Id,
                InvAge0To90Days = amazonProducts.InvAge0To90Days,
                InvAge181To270Days = amazonProducts.InvAge181To270Days,
                InvAge271To365Days = amazonProducts.InvAge271To365Days,
                InvAge365PlusDays = amazonProducts.InvAge365PlusDays,
                InvAge91to181Days = amazonProducts.InvAge91to181Days,
                UnitsShippedLast180Days = amazonProducts.UnitsShippedLast180Days,
                UnitsShippedLast24Hrs = amazonProducts.UnitsShippedLast24Hrs,
                UnitsShippedLast30Days = amazonProducts.UnitsShippedLast30Days,
                UnitsShippedLast365Days = amazonProducts.UnitsShippedLast365Days,
                UnitsShippedLast7Days = amazonProducts.UnitsShippedLast7Days,
                UnitsShippedLast90Days = amazonProducts.UnitsShippedLast90Days,
                Name = !string.IsNullOrEmpty(amazonProducts.ShortName) ? amazonProducts.ShortName : amazonProducts.Name,
                SellableQuantity = amazonProducts.SellableQuantity,
                WeeksOfCoverT180 = amazonProducts.WeeksOfCoverT180.ToMwsDecimalPrecision(),
                WeeksOfCoverT30 = amazonProducts.WeeksOfCoverT30.ToMwsDecimalPrecision(),
                WeeksOfCoverT365 = amazonProducts.WeeksOfCoverT365.ToMwsDecimalPrecision(),
                WeeksOfCoverT7 = amazonProducts.WeeksOfCoverT7.ToMwsDecimalPrecision(),
                WeeksOfCoverT90 = amazonProducts.WeeksOfCoverT90.ToMwsDecimalPrecision(),
                CreatedDate = amazonProducts.CreatedDate,
                Image = !string.IsNullOrEmpty(amazonProducts.CustomizedImage) ? amazonProducts.CustomizedImage : amazonProducts.OriginalImage,
                InTransit = amazonProducts.InTransit,
                ModifiedDate = amazonProducts.ModifiedDate,
                OutOfStockDate = DateTime.Now.AddDays(Convert.ToDouble(GetWeeksOfCoverAverage(amazonProducts.WeeksOfCoverT7.ToMwsDecimalPrecision(), amazonProducts.WeeksOfCoverT30.ToMwsDecimalPrecision(), amazonProducts.WeeksOfCoverT90.ToMwsDecimalPrecision())) * 7).AddDays(-Convert.ToDouble(amazonProducts.LeadTimeDays ?? 0)),
                ReOrderDays = (DateTime.Now.AddDays(Convert.ToDouble(GetWeeksOfCoverAverage(amazonProducts.WeeksOfCoverT7.ToMwsDecimalPrecision(), amazonProducts.WeeksOfCoverT30.ToMwsDecimalPrecision(), amazonProducts.WeeksOfCoverT90.ToMwsDecimalPrecision())) * 7).AddDays(-Convert.ToDouble(amazonProducts.LeadTimeDays ?? 0)) - DateTime.Now).Days,
                LeadTimeDays = amazonProducts.LeadTimeDays
            };
        }
        public static ModuleModel MapModuleModelWithModule(Module module)
        {
            return module == null ? null : new ModuleModel
            {
                DisplayName = module.DisplayName,
                MatIcon = module.MatIcon,
                ModuleId = module.ModuleId,
                Name = module.Name,
                Route = module.ModuleRoute
            };
        }
        public static SignatureDoc MapSignatureDocWithSignatureDocModel(SignatureDocModel signatureDocModel)
        {
            return signatureDocModel == null ? null : new SignatureDoc
            {
                Extension = signatureDocModel.Extension,
                SignatureDocId = signatureDocModel.SignatureDocId,
                OwnerName = signatureDocModel.OwnerName,
                OwnerEmail = signatureDocModel.OwnerEmail,
                ReceiverName = signatureDocModel.ReceiverName,
                ReceiverEmail = signatureDocModel.ReceiverEmail,
                Name = signatureDocModel.Name,
                Link = signatureDocModel.Link,
                SignatureBoxConfig = signatureDocModel.SignatureBoxConfig,
            };
        }
        public static SignatureDocModel MapSignatureDocModelWithSignatureDoc(SignatureDoc signatureDocModel)
        {
            return signatureDocModel == null ? null : new SignatureDocModel
            {
                Extension = signatureDocModel.Extension,
                SignatureDocId = signatureDocModel.SignatureDocId,
                OwnerName = signatureDocModel.OwnerName,
                OwnerEmail = signatureDocModel.OwnerEmail,
                ReceiverName = signatureDocModel.ReceiverName,
                ReceiverEmail = signatureDocModel.ReceiverEmail,
                Name = signatureDocModel.Name,
                Link = signatureDocModel.Link,
                SignatureBoxConfig = signatureDocModel.SignatureBoxConfig,
            };
        }
        public static AmazonMwsProductsReOrderSettings MapAmazonReOrderSettingsModelWithEntity(AmazonMwsProductsReOrderSettingsModel amazonMwsProductsReOrderSettingsModel)
        {
            return amazonMwsProductsReOrderSettingsModel == null ? null : new AmazonMwsProductsReOrderSettings
            {
                CreatedDate = DateTime.Now,
                IsActive = true,
                ModifiedDate = DateTime.Now,
                ReOderDaysAlarmColorCode = amazonMwsProductsReOrderSettingsModel.ReOderDaysAlarmColorCode,
                ReOrderDays = amazonMwsProductsReOrderSettingsModel.ReOrderDays,
                UserId = amazonMwsProductsReOrderSettingsModel.UserId
            };
        }
        public static JobEstimatorClient MapJobEstimatorClientModelWithEntity(JobEstimatorClientModel jobEstimatorClientModel) => jobEstimatorClientModel == null ? null : new JobEstimatorClient
        {
            ClientKey = jobEstimatorClientModel.ClientKey,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsActive = true,
            UserId = jobEstimatorClientModel.UserId,
            CategoryId = jobEstimatorClientModel.CategoryId,
            Note = jobEstimatorClientModel.Note,
            Recipients = jobEstimatorClientModel.Recipients,
            Disclaimer = jobEstimatorClientModel.Disclaimer
        };
        public static JobEstimatorClientModel MapJobEstimatorClientWithModel(JobEstimatorClient jobEstimatorClientModel) => jobEstimatorClientModel == null ? null : new JobEstimatorClientModel
        {
            ClientKey = jobEstimatorClientModel.ClientKey,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsActive = true,
            UserId = jobEstimatorClientModel.UserId,
            ClientId = jobEstimatorClientModel.ClientId,
            CategoryId = jobEstimatorClientModel.CategoryId.GetValueOrDefault(),
            Email = jobEstimatorClientModel.User?.Email,
            UserName = jobEstimatorClientModel.User?.UserName,
            Note = jobEstimatorClientModel.Note,
            Category = jobEstimatorClientModel.Category?.Name,
            Recipients = jobEstimatorClientModel.Recipients,
            Disclaimer = jobEstimatorClientModel.Disclaimer
        };
        public static JobEstimatorProductTypeModel MapJobEstimatorProductTypeWithModel(JobEstimatorProductType jobEstimatorProductType)
        {
            return jobEstimatorProductType == null ? null : new JobEstimatorProductTypeModel
            {
                CalculationMethod = jobEstimatorProductType.CalculationMethod,
                CategoryId = jobEstimatorProductType.CategoryId,
                CreatedDate = jobEstimatorProductType.CreatedDate,
                IsActive = jobEstimatorProductType.IsActive,
                ModifiedDate = jobEstimatorProductType.ModifiedDate,
                Name = jobEstimatorProductType.Name,
                ProductTypeId = jobEstimatorProductType.ProductTypeId,
                Image = jobEstimatorProductType.Image,
                Description = jobEstimatorProductType.Description
            };
        }
        public static JobEstimatorProductStyle MapJobEstimatorProductStyleWithModel(JobEstimatorProductStyleModel jobEstimatorProductStyleModel)
        {
            return jobEstimatorProductStyleModel == null ? null : new JobEstimatorProductStyle
            {
                CreatedDate = DateTime.Now,
                GroundCost = jobEstimatorProductStyleModel.GroundCost,
                GroundCostExcavate = jobEstimatorProductStyleModel.GroundCostExcavate,
                GroundCostFlat = jobEstimatorProductStyleModel.GroundCostFlat,
                IsActive = true,
                JobEstimatorClientId = jobEstimatorProductStyleModel.JobEstimatorClientId,
                LabourCost = jobEstimatorProductStyleModel.LabourCost,
                MaterialCost = jobEstimatorProductStyleModel.MaterialCost,
                ModifiedDate = DateTime.Now,
                ProductTypeId = jobEstimatorProductStyleModel.ProductTypeId,
                StyleImage = jobEstimatorProductStyleModel.StyleImage,
                StyleName = jobEstimatorProductStyleModel.StyleName,
                UnitCost = jobEstimatorProductStyleModel.UnitCost,
                IsRollsCalculation = jobEstimatorProductStyleModel.IsRollsCalculation,
                RollSize = jobEstimatorProductStyleModel.RollSize
            };
        }
        public static JobEstimatorProductStyleModel MapJobEstimatorModelWithProductStyle(JobEstimatorProductStyle jobEstimatorProductStyleModel)
        {
            return jobEstimatorProductStyleModel == null ? null : new JobEstimatorProductStyleModel
            {
                CreatedDate = DateTime.Now,
                GroundCost = jobEstimatorProductStyleModel.GroundCost,
                GroundCostExcavate = jobEstimatorProductStyleModel.GroundCostExcavate,
                GroundCostFlat = jobEstimatorProductStyleModel.GroundCostFlat,
                IsActive = true,
                JobEstimatorClientId = jobEstimatorProductStyleModel.JobEstimatorClientId,
                LabourCost = jobEstimatorProductStyleModel.LabourCost,
                MaterialCost = jobEstimatorProductStyleModel.MaterialCost,
                ModifiedDate = DateTime.Now,
                ProductTypeId = jobEstimatorProductStyleModel.ProductTypeId,
                StyleImage = jobEstimatorProductStyleModel.StyleImage,
                StyleName = jobEstimatorProductStyleModel.StyleName,
                ProductType = jobEstimatorProductStyleModel.ProductType.Name,
                ProductStyleId = jobEstimatorProductStyleModel.ProductStyleId,
                UnitCost = jobEstimatorProductStyleModel.UnitCost,
                IsRollsCalculation = jobEstimatorProductStyleModel.IsRollsCalculation,
                RollSize = jobEstimatorProductStyleModel.RollSize
            };
        }

        public static ContactNotesModel MapContactNotesWithContactNotesModel(ContactNotes contactNote)
        {
            return contactNote == null ? null : new ContactNotesModel
            {
                ContactId = contactNote.ContactId,
                CreatedDateTime = contactNote.CreatedDateTime,
                Id = contactNote.Id,
                Text = contactNote.Text,
                Type = contactNote.Type
            };
        }
        public static ContactNotes MapContactNotesModelWithContactNotes(ContactNotesModel contactNote)
        {
            return contactNote == null ? null : new ContactNotes
            {
                ContactId = contactNote.ContactId,
                CreatedDateTime = DateTime.Now,
                Id = contactNote.Id,
                Text = contactNote.Text,
                Type = contactNote.Type
            };
        }
        public static JobNotesModel MapJobNotesWithJobNotesModel(JobNotes jobNote)
        {
            return jobNote == null ? null : new JobNotesModel
            {
                JobId = jobNote.JobId,
                CreatedDateTime = jobNote.CreatedDateTime,
                Id = jobNote.Id,
                Text = jobNote.Text,
                Type = jobNote.Type
            };
        }
        public static JobNotes MapJobNotesModelWithJobNotes(JobNotesModel jobNote)
        {
            return jobNote == null ? null : new JobNotes
            {
                JobId = jobNote.JobId,
                CreatedDateTime = DateTime.Now,
                Id = jobNote.Id,
                Text = jobNote.Text,
                Type = jobNote.Type
            };
        }
        public static JobModel MapJobModelWithJob(Job job)
        {
            return job == null ? null : new JobModel
            {
                ContactId = job.ContactId,
                CreatedDateTime = job.CreatedDateTime,
                Description = job.Description,
                //EndDateTime=job.EndDateTime,
                EstimateDays = job.EstimateDays,
                JobFrequencyId = job.JobFrequencyId,
                JobId = job.JobId,
                JobFrequency = job.JobFrequency?.Frequency,
                JobStatus = job.JobStatus?.Status,
                JobStatusId = job.JobStatusId,
                JobType = job.JobType?.Type,
                JobTypeId = job.JobTypeId,
                ModifiedDateTime = job.ModifiedDateTime,
                Name = job.Name,
                Reference = job.Reference,
                Day = job.Day,
                EndDate = job.EndDate,
                EndTime = job.EndTime,
                StartDate = job.StartDate,
                StartTime = job.StartTime
                //StartDateTime=job.StartDateTime
            };
        }
        public static Job MapJobWithJobModel(JobModel job)
        {
            return job == null ? null : new Job
            {
                ContactId = job.ContactId,
                CreatedDateTime = job.CreatedDateTime,
                Description = job.Description,
                //EndDateTime = job.EndDateTime,
                EndDate = job.EndDate,
                EndTime = job.EndTime,
                EstimateDays = job.EstimateDays,
                JobFrequencyId = job.JobFrequencyId,
                JobId = job.JobId,
                JobStatusId = job.JobStatusId,
                JobTypeId = job.JobTypeId,
                ModifiedDateTime = job.ModifiedDateTime,
                Name = job.Name,
                Reference = job.Reference,
                Day = job.Day,
                StartDate = job.StartDate,
                StartTime = job.StartTime
                //StartDateTime = job.StartDateTime
            };
        }

        public static Valve MapValveModelWithValve(ValveModel valveModel)
        {
            if (valveModel == null)
                return null;
            return new Valve
            {
                AssetId = valveModel.AssetId,
                BvcontrolNumber = valveModel.BvcontrolNumber,
                BvId = valveModel.BvId,
                Comment = valveModel.Comment,
                Direction = valveModel.Direction,
                DmaName = valveModel.DmaName,
                Qrid = valveModel.Qrid,
                Id = Guid.NewGuid(),
                ValveSize = valveModel.ValveSize,
                Longitude = valveModel.Longitude,
                Latitude = valveModel.Latitude,
                CreatedDateTime = DateTime.Now,
                ValveId = valveModel.ValveId
            };
        }
        public static ValveModel MapValveWithValveModel(Valve valveModel)
        {
            if (valveModel == null)
                return null;
            return new ValveModel
            {
                AssetId = valveModel.AssetId,
                BvcontrolNumber = valveModel.BvcontrolNumber,
                BvId = valveModel.BvId,
                Comment = valveModel.Comment,
                Direction = valveModel.Direction,
                DmaName = valveModel.DmaName,
                Qrid = valveModel.Qrid,
                ValveId = valveModel.ValveId,
                ValveSize = valveModel.ValveSize,
                Longitude = valveModel.Longitude,
                Latitude = valveModel.Latitude,
                CreatedDateTime = valveModel.CreatedDateTime,
                ModifedDate = valveModel.ModifedDate,
                Events = valveModel.ValveEvent?.Select(a => new ValveEventModel
                {
                    CreatedDateTime = a.CreatedDateTime,
                    DateTimeStamp = a.DateTimeStamp,
                    EventDescription = a.EventDescription,
                    EventId = a.EventId,
                    EventType = a.EventType.GetValueOrDefault(),
                    ValveId = a.ValveId.GetValueOrDefault(),
                    Type = a.EventTypeNavigation?.Type,
                    EngineerId = a.EngineerId
                }).ToList(),
                Id=valveModel.Id
            };
        }

        public static ValveEvent MapEventWithEventModel(ValveEventModel valveEventModel)
        {
            if (valveEventModel == null)
                return null;
            return new ValveEvent
            {
                CreatedDateTime = DateTime.Now,
                DateTimeStamp = valveEventModel.DateTimeStamp,
                EventDescription = valveEventModel.EventDescription,
                EventType = valveEventModel.EventType,
                ValveId = valveEventModel.ValveId,
                EngineerId = valveEventModel.EngineerId
            };
        }
        public static ValveEventModel MapEventModelWithEvent(ValveEvent valveEventModel)
        {
            if (valveEventModel == null)
                return null;
            return new ValveEventModel
            {
                CreatedDateTime = DateTime.Now,
                DateTimeStamp = valveEventModel.DateTimeStamp,
                EventDescription = valveEventModel.EventDescription,
                EventType = valveEventModel.EventType.GetValueOrDefault(),
                ValveId = valveEventModel.ValveId.GetValueOrDefault(),
                EventId = valveEventModel.EventId,
                QrId = valveEventModel.Valve.Qrid,
                EngineerId = valveEventModel.EngineerId
            };
        }

        private static decimal GetWeeksOfCoverAverage(params string[] items)
        {
            decimal average = 0;
            if (items.Any())
            {
                int length = items.Length;
                decimal[] weeksOfCover = new decimal[length];
                for (int i = 0; i < items.Length; i++)
                {
                    if (!string.IsNullOrEmpty(items[i]) && items[i] != "Infinite")
                    {
                        if (decimal.TryParse(items[i], out decimal result))
                        {
                            weeksOfCover[i] = result;
                        }
                    }
                }
                average = Utility.PullMeanAverage(weeksOfCover);
            }
            return average;
        }
    }
}

