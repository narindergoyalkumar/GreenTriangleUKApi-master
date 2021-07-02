using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface ICustomFieldValueService
    {
        IEnumerable<CustomFieldValue> CustomFieldValuesByContact(int contactId);
        IEnumerable<CustomFieldValue> CustomFieldValuesByCustomFieldId(int customFieldId); 
        IEnumerable<CustomFieldValue> CustomFieldValuesByCustomFieldIdAndContactId(int customFieldId,int contactId);

        void Delete(CustomFieldValue customFieldValues);
        void Update(CustomFieldValueModel customFieldValueModel, int customFieldValueId);
        void Add(CustomFieldValueModel customFieldValueModel);
        IEnumerable<CustomFieldValue> GetSweepDues();
    }
}
