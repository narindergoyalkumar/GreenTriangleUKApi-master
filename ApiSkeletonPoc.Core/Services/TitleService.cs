using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;

namespace ApiSkeletonPoc.Core.Services
{
    public class TitleService : ITitleService
    {
        private readonly IBaseService<Title> _titleBaseService;
        private readonly ILogService _logService;

        public TitleService(IBaseService<Title> titleBaseService, ILogService logService)
        {
            _titleBaseService = titleBaseService;
            _logService = logService;
        }

        public int Add(TitleModel titleModel)
        {
            int titleId = 0;
            var createdTitle = _titleBaseService.AddOrUpdate(Mapper.MapTitleModelWithTitle(titleModel), 0);
            if (createdTitle != null)
            {
                titleId = createdTitle.TitleId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Title with Title Id {createdTitle.TitleId} added" });
            }

            return titleId;
        }

        public int Update(TitleModel titleModel)
        {
            int titleId = titleModel.TitleId;
            var updatedTitle = _titleBaseService.AddOrUpdate(Mapper.MapTitleModelWithTitle(titleModel), titleModel.TitleId);
            if (updatedTitle != null)
            {
                titleId = updatedTitle.TitleId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A Title with Title Id {updatedTitle.TitleId} updated" });
            }

            return titleId;
        }

        public bool Remove(int titleId)
        {
            var title = _titleBaseService.GetById(titleId);
            if (title != null)
            {
                _titleBaseService.Remove(title.TitleId);
                _logService.Add(new LogEntryModel
                {
                    LoggedDateTime = DateTime.Now.ToString(),
                    LogText = $"A new title with Id {title.TitleId} deleted"
                });
                return true;
            }

            return false;
        }
    }
}
