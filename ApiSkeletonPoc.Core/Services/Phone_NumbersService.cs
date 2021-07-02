using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;

namespace ApiSkeletonPoc.Core.Services
{
    public class Phone_NumbersService : IPhone_NumbersService
    {
        private readonly IBaseService<Phone> _basePhoneNumberService;
        private readonly ILogService _logService;

        public Phone_NumbersService(IBaseService<Phone> basePhoneNumberService, ILogService logService)
        {
            _basePhoneNumberService = basePhoneNumberService;
            _logService = logService;
        }

        public int Add(PhoneModel phoneNumber)
        {
            int phoneId = 0;

            var addedPhoneNumber = _basePhoneNumberService.AddOrUpdate(Mapper.MapPhoneNumberModelWithPhoneNumber(phoneNumber), 0);
            if (addedPhoneNumber != null)
            {
                phoneId = addedPhoneNumber.PhoneId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Phone number with Id {phoneNumber.PhoneId} added" });
            }

            return phoneId;
        }

        public int Update(PhoneModel phoneNumber, int id)
        {
            int phoneId = id;
            var data = _basePhoneNumberService.GetById(id);
            if (data != null)
            {
                data.Number = phoneNumber.Number;
                data.PhoneTypeId = phoneNumber.PhoneTypeId;
                data.RecordUpdatedDate =DateTime.Now;
                _basePhoneNumberService.AddOrUpdate(data, id);
                //_logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A Phone number with Id {phoneNumber.PhoneId} updated" });
            }
            return phoneId;
        }
        public bool Remove(int phoneId)
        {
            var phoneNumber = _basePhoneNumberService.GetById(phoneId);
            if (phoneNumber != null)
            {
                _basePhoneNumberService.Remove(phoneNumber.PhoneId);
                _logService.Add(new LogEntryModel
                {
                    LoggedDateTime = DateTime.Now.ToString(),
                    LogText = $"A new Phone number with Id {phoneNumber.PhoneId} deleted"
                });
                return true;
            }
            return false;
        }

        public PhoneModel Get(int id)
        {
            return Mapper.MapPhoneNumberWithPhoneNumberModel(_basePhoneNumberService.GetById(id));
        }

        public PhoneModel GetByNumber(string number)
        {
            throw new NotImplementedException();
        }
    }
}
