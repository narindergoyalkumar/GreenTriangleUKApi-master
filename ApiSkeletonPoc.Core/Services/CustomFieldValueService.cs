using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class CustomFieldValueService : ICustomFieldValueService
    {
        private readonly IBaseService<CustomFieldValue> _baseService;
        private readonly ILogService _logService;
        public CustomFieldValueService(IBaseService<CustomFieldValue> baseService, ILogService logService)
        {
            _baseService = baseService;
            _logService = logService;
        }
        public IEnumerable<CustomFieldValue> CustomFieldValuesByContact(int contactId)
        {
            return _baseService.Where(a => a.ContactId == contactId).ToList();
        }

        public IEnumerable<CustomFieldValue> CustomFieldValuesByCustomFieldId(int customFieldId)
        {
            return _baseService.Where(a => a.CustomFieldId == customFieldId).ToList();
        }

        public void Delete(CustomFieldValue customFieldValue)
        {
            _baseService.Remove(customFieldValue.CustomFieldValueId);
        }
        public void Update(CustomFieldValueModel customFieldValueModel, int customFieldValueId)
        {
            var data = _baseService.GetById(customFieldValueId);
            if (data != null)
            {
                data.CustomFieldValue1 = customFieldValueModel.CustomFieldValue;
                _baseService.AddOrUpdate(data, customFieldValueId);
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A custom field value with Id {customFieldValueId} updated to {customFieldValueModel.CustomFieldValue}" });
            }
        }
        public void Add(CustomFieldValueModel customFieldValue)
        {
            _baseService.AddOrUpdate(Mapper.MapCustomFieldValueWithCustomFieldValueModel(customFieldValue), 0);
        }

        public IEnumerable<CustomFieldValue> CustomFieldValuesByCustomFieldIdAndContactId(int customFieldId, int contactId)
        {
            return _baseService.Where(a => a.CustomFieldId == customFieldId&&a.ContactId==contactId).ToList();
        }
        public IEnumerable<CustomFieldValue> GetSweepDues()
        {
            string[] navigationProps = { "CustomField", "Contact" };
            //var data = _baseService.GetAll(navigationProps);
            var customFieldValues= _baseService.Where(a => a.CustomField.CustomFieldKey.ToLower()=="duedate"||a.CustomField.CustomFieldKey.ToLower()=="booked",navigationProps).ToList();
            return customFieldValues;
        }
    }
}
