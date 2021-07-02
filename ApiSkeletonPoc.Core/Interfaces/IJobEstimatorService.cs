using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Interfaces
{
    public interface IJobEstimatorService
    {
        JobEstimatorClient AddClient(JobEstimatorClientModel jobEstimatorClientModel);
        void DeleteClient(int clientId);
        List<JobEstimatorProductStyleModel> GetClientProductsByKey(string key);
        List<JobEstimatorProductTypeModel> GetProductTypes(int categoryId);
        List<JobEstimatorProductStyle> GetClientProductsByProductType(int productTypeId, string key);
        void RemoveProductStyle(int id);
        void UpdateProductStyle(int id, JobEstimatorProductStyleModel jobEstimatorProductStyleModel);
        int AddProductStyle(JobEstimatorProductStyleModel jobEstimatorProductStyleModel);
        JobEstimatorClientModel  IsClientKeyExists(string key);
        JobEstimatorClient GetClientByKey(string key);
        int UploadBulk(List<JobEstimatorProductStyleModel> jobEstimatorProductStyleModels, int clientId);
        int GetTypeIdByName(string type,int categoryID);
        List<JobEstimatorClientModel> GetClients();
        JobEstimatorClientModel GetClientByUserId(string userId);
        JobEstimatorProductStyle GetProductStyleById(int id);
        void UpdateProductStyleImage(int id, string imageLink);
        void UpdateClient(int id, JobEstimatorClientModel jobEstimatorClientModel);
    }
}
