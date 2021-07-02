using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class LogService : ILogService
    {
        private readonly IBaseService<LogEntry> _baseService;
        public LogService(IBaseService<LogEntry> baseService)
        {
            _baseService = baseService;
        }

        public void Add(LogEntryModel logEntryModel)
        {
            _baseService.AddOrUpdate(Mapper.MapTblLogEntryWithLogEntryModel(logEntryModel), 0);
        }

        public IEnumerable<LogEntryModel> GetAll()
        {
            return _baseService.GetAll().Select(l => Mapper.MapLogEntryModelWithTblLogEntry(l));
        }
    }
}
