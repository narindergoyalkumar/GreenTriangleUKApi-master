using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class VisitsService : IVisitsService
    {
        private readonly IBaseService<Visit> _baseVisitService;
        private readonly ILogService _logService;

        public VisitsService(IBaseService<Visit> baseVisitService, ILogService logService)
        {
            _baseVisitService = baseVisitService;
            _logService = logService;
        }

        public BaseResponseModel Add(VisitModel visit)
        {
            BaseResponseModel baseResponseModel = null;
            int visitId = 0;
            var createdVisit = _baseVisitService.AddOrUpdate(Mapper.MapVisitModelWithVisit(visit), 0);
            if (createdVisit != null)
            {
                visitId = createdVisit.VisitId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Visit with Visit Id {createdVisit.VisitId} added" });
                baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Visit added successfully.", Response = visitId };
            }
            return baseResponseModel;
        }

        public BaseResponseModel Update(VisitModel visit)
        {
            int visitId = visit.VisitId;
            BaseResponseModel baseResponseModel = null;
            var data = _baseVisitService.GetById(visit.VisitId);
            if (data != null)
            {
                data.VisitBookedFlg = visit.VisitBookedFlg;
                data.VisitDate = visit.VisitDate;
                data.VisitDue = visit.VisitDue;
                data.RecordUpdatedDate = DateTime.Now;
                data.EmployeeId = visit.EmployeeId;
                var updatedVisit = _baseVisitService.AddOrUpdate(data, visitId);
                if (updatedVisit != null)
                {
                    visitId = updatedVisit.VisitId;
                    _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A Visit with Visit Id {updatedVisit.VisitId} updated" });
                    baseResponseModel = new BaseResponseModel { IsSuccess = true, Message = "Contact updated successfully.", Response = visitId };
                }
            }
            return baseResponseModel;
        }

        public BaseResponseModel GetAll()
        {
            var allVisits = _baseVisitService.GetAll("Employee", "Contact");
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = string.Empty,
                Response = allVisits.Select(Mapper.MapVisitWithVisitModel).ToList()
            };
        }

        public BaseResponseModel GetByEmployeeId(int employeeId)
        {
            var allEmployeeVisits = _baseVisitService.Where(x => x.EmployeeId == employeeId, "Employee", "Contact").ToList();
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = string.Empty,
                Response = allEmployeeVisits.Select(Mapper.MapVisitWithVisitModel).ToList()
            };
        }

        public BaseResponseModel GetByContactId(int contactId)
        {
            var allContactVisit = _baseVisitService.Where(x => x.ContactId == contactId, "Employee", "Contact").ToList();
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = string.Empty,
                Response = allContactVisit.Select(Mapper.MapVisitWithVisitModel).ToList()
            };
        }

        public BaseResponseModel GetById(int id)
        {
            return new BaseResponseModel
            {
                IsSuccess = true,
                Message = string.Empty,
                Response = Mapper.MapVisitWithVisitModel(_baseVisitService.GetById(id))
            };
        }

        public BaseResponseModel Remove(int visitId)
        {
            _baseVisitService.Remove(visitId);
            _logService.Add(new LogEntryModel
            {
                LoggedDateTime = DateTime.Now.ToString(),
                LogText = $"A visit with Id {visitId} deleted"
            });
            return new BaseResponseModel { IsSuccess = true, Message = "Visit deleted successfully.", Response = true };
        }
    }
}
