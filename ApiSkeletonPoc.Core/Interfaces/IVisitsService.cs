using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IVisitsService
    {
        BaseResponseModel Add(VisitModel visit);
        BaseResponseModel Update(VisitModel visit);
        BaseResponseModel GetAll();
        BaseResponseModel GetByEmployeeId(int employeeId);
        BaseResponseModel GetByContactId(int contactId);
        BaseResponseModel GetById(int id);
        BaseResponseModel Remove(int visitId);
    }
}
