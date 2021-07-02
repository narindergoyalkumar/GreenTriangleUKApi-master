using System;
using System.Collections.Generic;
using System.Text;
using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;

namespace ApiSkeletonPoc.Core.Services
{
    public class Social_MediaService : ISocial_MediaService
    {
        private readonly IBaseService<SocialMedia> _baseSocialMediaService;
        private readonly ILogService _logService;

        public Social_MediaService(ILogService logService, IBaseService<SocialMedia> baseSocialMediaService)
        {
            _logService = logService;
            _baseSocialMediaService = baseSocialMediaService;
        }

        public int Add(SocialMediaModel model)
        {
            int socialMediaid = 0;

            var addedRow = _baseSocialMediaService.AddOrUpdate(Mapper.MapSocialMediaModelWithSocialMedia(model), 0);
            if (addedRow != null)
            {
                socialMediaid = addedRow.SocialMediaId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Social Media with Id {addedRow.SocialMediaId} added" });
            }

            return socialMediaid;
        }

        public int Update(SocialMediaModel model, int id)
        {
            int socialMediaid = id;
            var data = _baseSocialMediaService.GetById(id);
            if (data != null)
            {
                data.Image = model.Image;
                data.Link = model.Link;
                data.RecordUpdatedDate = DateTime.Now;
                data.SocialMediaTypeId = model.SocialMediaTypeId;
                _baseSocialMediaService.AddOrUpdate(data, id);
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A Social Media with Id {id} updated" });
            }

            return socialMediaid;
        }

        public bool Remove(int id)
        {
            var socialMedia = _baseSocialMediaService.GetById(id);
            if (socialMedia != null)
            {
                _baseSocialMediaService.Remove(socialMedia.SocialMediaId);
                _logService.Add(new LogEntryModel
                {
                    LoggedDateTime = DateTime.Now.ToString(),
                    LogText = $"A new Social Media with Id {socialMedia.SocialMediaId} deleted"
                });
                return true;
            }
            return false;
        }
    }
}
