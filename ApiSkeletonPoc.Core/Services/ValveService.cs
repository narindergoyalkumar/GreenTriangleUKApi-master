using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class ValveService : IValveService
    {
        private readonly IBaseService<Valve> _baseValveService;

        public ValveService(IBaseService<Valve> baseValveService)
        {
            _baseValveService = baseValveService;
        }
        public IEnumerable<ValveModel> GetAll(out int count, int pageNum, int pageSize, bool? sortdirection = null, string sortString = null)
        {
            var valveModels = _baseValveService.Get(out count, null, null, pageNum, pageSize).Select(c => Mapper.MapValveWithValveModel(c)).ToList().OrderByDescending(a => a.CreatedDateTime);
            return valveModels;
        }

        public ValveModel GetValveDetailByQR(string qrId)
        {
            string[] navigationProps = { "ValveEvent", "ValveEvent.EventTypeNavigation" };
            var valveDetails = _baseValveService.Where(a => a.Qrid == qrId, navigationProps).FirstOrDefault();
            return Mapper.MapValveWithValveModel(valveDetails);
        }

        public ValveModel InsertValveDtails(ValveModel valveModel)
        {
            var entity = _baseValveService.AddOrUpdate(Mapper.MapValveModelWithValve(valveModel), Guid.Empty);
            return Mapper.MapValveWithValveModel(entity);
        }

        public bool IsQrIdExist(string qrId)
        {
            return _baseValveService.Where(a => a.Qrid == qrId).FirstOrDefault() == null ? false : true;
        }

        public bool IsValveExist(string valveId)
        {
            return _baseValveService.GetById(Guid.Parse(valveId)) == null ? false : true;
        }

        public IEnumerable<ValveModel> SearchById(string valveId)
        {
            var valveModels = _baseValveService.Where(a=>a.ValveId.Contains(valveId)).Select(c => Mapper.MapValveWithValveModel(c)).ToList().OrderByDescending(a => a.CreatedDateTime);
            return valveModels;
        }

        public ValveModel UpdateValveDetails(Guid valveId, ValveModel valveModel)
        {
            var entity = _baseValveService.GetById(valveId);
            if (entity == null)
            {
                return null;
            }
            entity.AssetId = valveModel.AssetId;
            entity.BvcontrolNumber = valveModel.BvcontrolNumber;
            entity.BvId = valveModel.BvId;
            entity.Comment = valveModel.Comment;
            entity.Direction = valveModel.Direction;
            entity.DmaName = valveModel.DmaName;
            entity.ModifedDate = DateTime.Now;
            entity.ValveSize = valveModel.ValveSize;
            entity.Longitude = valveModel.Longitude;
            entity.Latitude = valveModel.Latitude;
            entity.ValveId = valveModel.ValveId;
            var updatedEntity = _baseValveService.AddOrUpdate(entity, valveId);
            return Mapper.MapValveWithValveModel(updatedEntity);
        }
    }
}
