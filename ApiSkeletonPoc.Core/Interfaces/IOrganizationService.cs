using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IOrganizationService
    {
        int Add(OrganizationModel model);
        int Update(OrganizationModel model,int id);
        OrganizationModel Get(int orgId);
        //OrganizationModel GetByName(string organizationName);
        bool Remove(int orgId);
    }
}
