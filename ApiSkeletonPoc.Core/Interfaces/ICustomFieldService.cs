using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface ICustomFieldService
    {
        BaseResponseModel Add(CustomFieldModel customFieldModel);
        BaseResponseModel Delete(int id);
        BaseResponseModel GetByClient(int clientId);
        BaseResponseModel Get();
    }
}
