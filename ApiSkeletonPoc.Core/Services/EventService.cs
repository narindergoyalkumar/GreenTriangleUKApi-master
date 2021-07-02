using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class ValveEventService : IValveEventService
    {
        private readonly IBaseService<ValveEvent> _baseValveEventService;
        private readonly IBaseService<ValveEventType> _baseValveEventTypeService;

        public ValveEventService(IBaseService<ValveEvent> baseValveEventService, IBaseService<ValveEventType> baseValveEventTypeService)
        {
            _baseValveEventService = baseValveEventService;
            _baseValveEventTypeService = baseValveEventTypeService;
        }
        public bool AddEvent(ValveEventModel valveEventModel)
        {
            if (valveEventModel == null)
                return false;
            var entity = _baseValveEventService.AddOrUpdate(Mapper.MapEventWithEventModel(valveEventModel), 0);
            if (entity == null)
                return false;
            return true;
        }

        public IEnumerable<ValveEventModel> GetEventByQRId(string qrId, Guid valveId)
        {
            string[] navigationProps = { "Valve" };
            return _baseValveEventService.Where(a => a.ValveId == valveId, navigationProps).Select(a => Mapper.MapEventModelWithEvent(a)).ToList();
        }

        public IEnumerable<object> GetEventTypes()
        {
            return _baseValveEventTypeService.GetAll();
        }
    }
}
