using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IAmazonMwsService
    {
        IEnumerable<MwsProductsModel> GetProducts();
        bool AssignShortName(int id, string shortName);
        bool UpdateImageForProduct(int id, string imageLink);
        bool UpdateInTransitProductsCount(int id, int count);
        MwsProductsModel Get(int id);
        bool ResetProductShortName(int productId);
        bool ResetProductImage(int productId);
        bool SetLeadTime(int productId,int days);
        bool SetReOrderDaysConfiguration(List<AmazonMwsProductsReOrderSettingsModel> configurationList, Guid userId);
        User GetMwsUserWithSettings(Guid id);
    }
}
