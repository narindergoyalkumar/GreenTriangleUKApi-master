using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseService<Employee> _baseEmployeeService;
        private readonly IVisitsService _visitsService;
        private readonly ILogService _logService;

        public EmployeeService(IBaseService<Employee> baseEmployeeService, ILogService logService, IVisitsService visitsService)
        {
            _baseEmployeeService = baseEmployeeService;
            _logService = logService;
            _visitsService = visitsService;
        }

        public BaseResponseModel Add(EmployeeModel employee)
        {
            BaseResponseModel baseResponseModel = null;
            if(employee.ClientId>0)
            {
                var createdEmployee = _baseEmployeeService.AddOrUpdate(Mapper.MapEmployeeModelWithEmployee(employee), 0);

                if (createdEmployee != null)
                {
                    int employeeId = createdEmployee.EmployeeId;
                    _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Employee with Employee Id {employee.EmployeeId} added" });
                    baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Employee added successfully.", Response = employeeId };
                }
            }
            else
            {
                baseResponseModel = new BaseResponseModel { IsSuccess = false, Message = "Client id is required.", Response = null };
            }
            return baseResponseModel;
        }

        public BaseResponseModel Update(EmployeeModel employee)
        {
            int employeeId = employee.EmployeeId;
            BaseResponseModel baseResponseModel = null;
            var data = _baseEmployeeService.GetById(employeeId);
            if (data != null)
            {
                data.FirstName = employee.FirstName;
                data.LastName = employee.LastName;
                data.NiNumber = employee.NiNumber;
                data.RecordUpdatedDate = DateTime.Now;
                var updatedEmployee = _baseEmployeeService.AddOrUpdate(data, employeeId);
                if (updatedEmployee != null)
                {
                    employeeId = updatedEmployee.EmployeeId;
                    _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"An Employee with Employee Id {employee.EmployeeId} updated" });
                    baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Employee updated successfully.", Response = employeeId };
                }
            }
            return baseResponseModel;
        }

        public BaseResponseModel GetAll(int clientId)
        {
            var allEmployee = _baseEmployeeService.Where(a => a.ClientId == clientId, "Visit");
            return new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = allEmployee.Select(Mapper.MapEmployeeWithEmployeeModel).ToList() };
        }

        public BaseResponseModel GetById(int id)
        {
            var employee = _baseEmployeeService.Where(x => x.EmployeeId == id, "Visit").FirstOrDefault();
            return new BaseResponseModel { IsSuccess = true, Message = string.Empty, Response = Mapper.MapEmployeeWithEmployeeModel(employee) };
        }

        public BaseResponseModel Remove(int employeeId)
        {
            BaseResponseModel baseResponseModel = null;
            var employee = _baseEmployeeService.GetById(employeeId);
            if (employee != null)
            {
                var allVisitsByEmployee = (IEnumerable<VisitModel>)_visitsService.GetByEmployeeId(employee.EmployeeId).Response;
                foreach (var visitByEmployee in allVisitsByEmployee)
                {
                    _visitsService.Remove(visitByEmployee.VisitId);
                    _logService.Add(new LogEntryModel
                    {
                        LoggedDateTime = DateTime.Now.ToString(),
                        LogText = $"A Visit with Id {visitByEmployee.VisitId} deleted"
                    });
                }

                _baseEmployeeService.Remove(employee.EmployeeId);
                _logService.Add(new LogEntryModel
                {
                    LoggedDateTime = DateTime.Now.ToString(),
                    LogText = $"An Employee with Id {employee.EmployeeId} deleted"
                });
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Contact deleted successfully.", Response = true };
            }
            return baseResponseModel;
        }
    }
}
