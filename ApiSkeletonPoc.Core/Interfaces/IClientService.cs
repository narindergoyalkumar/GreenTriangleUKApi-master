using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IClientService
    {
        BaseResponseModel Add(ClientModel clientModel);
        BaseResponseModel Update(int id, ClientModel clientModel);
        BaseResponseModel Remove(int clintId);
        bool IsClientExists(int clientId);
        void UpdateRemainingTexts(int count, int clientId);
        BaseResponseModel Get(int id);
        BaseResponseModel GetAll();
        void UpdateRemainingTextsWithDeleiveredMessage(int count, int clientId);
    }
}
