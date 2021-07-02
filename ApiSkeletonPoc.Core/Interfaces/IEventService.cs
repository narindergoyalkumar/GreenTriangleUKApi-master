using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IValveEventService
    {
        bool AddEvent(ValveEventModel valveEventModel);
        IEnumerable<ValveEventModel> GetEventByQRId(string qrId, Guid valveId);
        IEnumerable<object> GetEventTypes();
    }
}
