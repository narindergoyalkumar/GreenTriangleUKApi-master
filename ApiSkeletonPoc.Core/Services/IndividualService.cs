using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class IndividualService : IIndividualService
    {
        private readonly IBaseService<Individual> _baseService;
        private readonly ILogService _logService;

        public IndividualService(IBaseService<Individual> baseService, ILogService logService)
        {
            _baseService = baseService;
            _logService = logService;
        }
        public int Add(IndividualModel individualModel)
        {
            int individualId = 0;
            var individual = _baseService.AddOrUpdate(Mapper.MapIndividualWithIndividualModel(individualModel), 0);
            if (individual != null)
            {
                individualId = individual.IndividualId;
                _logService.Add(new LogEntryModel { LoggedDateTime = DateTime.Now.ToString(), LogText = $"A new Person with Id {individualId} added" });
            }
            return individualId;
        }

        public IEnumerable<IndividualModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IndividualModel GetById(int id)
        {
            return Mapper.MapIndividualModelWithIndividual(_baseService.GetById(id));
        }

        public bool Remove(int individualId)
        {
            _baseService.Remove(individualId);
            return true;
        }

        public IndividualModel Update(IndividualModel individualModel, int individualId)
        {
            var individual = _baseService.GetById(individualId);
            if (individual != null)
            {
                individual.AffiliateKey = individualModel.AffiliateKey;
                individual.FirstName = individualModel.FirstName;
                individual.JobTitle = individualModel.JobTitle;
                individual.LastName = individualModel.LastName;
                individual.RecordUpdatedDate = DateTime.Now;
                individual.TitleId = individualModel.TitleId;
                individual.OrgId = individualModel.OrgId;
                individualModel = Mapper.MapIndividualModelWithIndividual(_baseService.AddOrUpdate(individual, individualId));
            }
            return individualModel;
        }
    }
}
