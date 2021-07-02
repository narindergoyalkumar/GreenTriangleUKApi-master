using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class CustomFieldService : ICustomFieldService
    {
        private readonly IBaseService<CustomField> _baseService;
        private readonly ICustomFieldValueService _customFieldValueService;
        public CustomFieldService(IBaseService<CustomField> baseService, ICustomFieldValueService customFieldValueService)
        {
            _baseService = baseService;
            _customFieldValueService = customFieldValueService;
        }
        public BaseResponseModel Add(CustomFieldModel customFieldModel)
        {
            BaseResponseModel baseResponseModel = null;
            var customField = _baseService.AddOrUpdate(Mapper.MapCustomFieldModelWithCustomField(customFieldModel), 0);
            if (customField != null)
            {
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Custom field added successfully.", Response = string.Empty };
            }
            return baseResponseModel;
        }

        public BaseResponseModel Delete(int id)
        {
            BaseResponseModel baseResponseModel = null;
            var customFieldsValueLst = _customFieldValueService.CustomFieldValuesByCustomFieldId(id);
            if (customFieldsValueLst.Any())
            {
                foreach (var customfieldValue in customFieldsValueLst)
                {
                    _customFieldValueService.Delete(customfieldValue);
                }
                _baseService.Remove(id);
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = $"{customFieldsValueLst.Count()} entities removed.", Response = true };
            }
            return baseResponseModel;
        }

        public BaseResponseModel Get()
        {
            var data = _baseService.GetAll("CustomFieldValue", "VisibleTo").Select(a => Mapper.MapCustomFieldWithCustomFieldModel(a)).ToList();
            return new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = data };
        }

        public BaseResponseModel GetByClient(int clientId)
        {
            var customFields = _baseService.Where(a => a.ClientId == clientId, "VisibleTo").Select(a => Mapper.MapCustomFieldWithCustomFieldModel(a)).ToList();
            return new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = customFields };
        }
    }
}
