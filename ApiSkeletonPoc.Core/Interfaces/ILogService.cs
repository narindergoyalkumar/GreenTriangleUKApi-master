using ApiSkeletonPoc.Core.Models;
using System.Collections.Generic;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface ILogService
    {
        void Add(LogEntryModel logEntryModel);
        IEnumerable<LogEntryModel> GetAll();
    }
}
