using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.ResponseModels;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IContactService
    {
        public BaseResponseModel Add(ContactModel contactModel);
        public BaseResponseModel Get(int id, int clientId);
        public IEnumerable<object> GetAll(int clientId, out int count, int pageNum, int pageSize, bool? sortdirection = null, string sortString = null);
        public BaseResponseModel Update(ContactModel contactModel);
        public BaseResponseModel Remove(int id);
        public BaseResponseModel Import(List<ContactModel> contactModels, int clientId);
        public void BulkDelete(List<int> ids);
        public IEnumerable<object> SearchContactByAddress(string address,int clientId);
        public IEnumerable<object> GetCapsuleContacts(int pageNum, int pageSize);
    }
}
