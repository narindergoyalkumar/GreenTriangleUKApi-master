using ApiSkeletonPoc.Core.Models;
using System.Collections.Generic;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IClientModuleMappingService
    {
        bool IsClientSubscribedToModule(int moduleId, int clientId);
        IEnumerable<ModuleModel> GetModules();
    }
}
