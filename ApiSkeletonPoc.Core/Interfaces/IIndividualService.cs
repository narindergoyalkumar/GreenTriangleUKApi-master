using ApiSkeletonPoc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IIndividualService
    {
        public int Add(IndividualModel individualModel);
        IEnumerable<IndividualModel> GetAll();
        IndividualModel GetById(int id);
        bool Remove(int individualId);
        IndividualModel Update(IndividualModel individualModel, int individualId);
    }
}
