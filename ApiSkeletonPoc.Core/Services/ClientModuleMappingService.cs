using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class ClientModuleMappingService : IClientModuleMappingService
    {
        private readonly IBaseService<ClientModuleMapping> _baseService;
        private readonly IBaseService<Module> _baseModuleService;
        public ClientModuleMappingService(IBaseService<ClientModuleMapping> baseService, IBaseService<Module> baseModuleService)
        {
            _baseService = baseService;
            _baseModuleService = baseModuleService;
        }

        public bool IsClientSubscribedToModule(int moduleId, int clientId)
        {
            return _baseService.Where(a => a.ClientId == clientId && a.ModuleId == moduleId).FirstOrDefault() == null ? false : true;
        }
        public IEnumerable<ModuleModel> GetModules()
        {
            return _baseModuleService.GetAll().Select(a => Mapper.MapModuleModelWithModule(a)).ToList();
        }
    }
}
