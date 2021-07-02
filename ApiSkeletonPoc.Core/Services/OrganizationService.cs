using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Core.Repositories;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IBaseService<Organization> _baseOrgService;
        private readonly ILogService _logService;
        public OrganizationService(IBaseService<Organization> baseOrgService, ILogService logService)
        {
            _baseOrgService = baseOrgService;
            _logService = logService;
        }

        public int Add(OrganizationModel model)
        {
            int orgId = 0;

            var addedOrganization = _baseOrgService.AddOrUpdate(Mapper.MapOrganizationWithOrganizationModel(model), 0);

            if (addedOrganization != null)
            {
                orgId = addedOrganization.OrgId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Organization with Organization Id {orgId} added" });
            }
            return orgId;
        }

        public int Update(OrganizationModel model, int id)
        {
            int orgId = id;
            var data = _baseOrgService.GetById(id);
            if (data != null)
            {
                data.OrgName = model.OrgName;
                data.RecordUpdatedDate = DateTime.Now;
                _baseOrgService.AddOrUpdate(data, id);
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"An Organization with Organization Id {orgId} updated" });
            }
            return orgId;
        }

        public OrganizationModel Get(int orgId)
        {
            return Mapper.MapOrganizationModelWithOrganization(_baseOrgService.GetById(orgId));
        }

        //public OrganizationModel GetByName(string organizationName)
        //{
        //    return Mapper.MapOrganizationModelWithOrganization(_baseOrgService
        //        .Where(x => x.Org_name == organizationName).FirstOrDefault());
        //}

        public bool Remove(int orgId)
        {
            var org = _baseOrgService.GetById(orgId);
            if (org != null)
            {
                _baseOrgService.Remove(org.OrgId);
                _logService.Add(new LogEntryModel
                {
                    LoggedDateTime = DateTime.Now.ToString(),
                    LogText = $"A new Organization with Id {org.OrgId} deleted"
                });
                return true;
            }
            return false;
        }
    }
}
