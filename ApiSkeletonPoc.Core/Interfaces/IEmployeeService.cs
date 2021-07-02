using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Models;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IEmployeeService
    {
        BaseResponseModel Add(EmployeeModel employee);
        BaseResponseModel Update(EmployeeModel employee);
        BaseResponseModel GetAll(int clientId);
        BaseResponseModel GetById(int id);
        BaseResponseModel Remove(int employeeId);

    }
}
