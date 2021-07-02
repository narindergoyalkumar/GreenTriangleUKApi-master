using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiSkeletonPoc.Core.Services
{
    public class JobEstimatorService : IJobEstimatorService
    {
        private readonly IBaseService<JobEstimatorClient> _baseService;
        private readonly IBaseService<JobEstimatorProductStyle> _productStyleBaseService;
        private readonly IBaseService<JobEstimatorProductType> _productTypeBaseService;
        private readonly IUserService _userService;
        public JobEstimatorService(IBaseService<JobEstimatorClient> baseService, IBaseService<JobEstimatorProductStyle> productStyleBaseService, IBaseService<JobEstimatorProductType> productTypeBaseService, IUserService userService)
        {
            _baseService = baseService;
            _productStyleBaseService = productStyleBaseService;
            _productTypeBaseService = productTypeBaseService;
            _userService = userService;
        }
        public void DeleteClient(int clientId)
        {
            var productsStyles = _productStyleBaseService.Where(a => a.JobEstimatorClientId == clientId).ToList();
            if (productsStyles.Any())
            {
                foreach (var productStyle in productsStyles)
                {
                    _productStyleBaseService.Remove(productStyle.ProductStyleId);
                }
               
            }
            var client = _baseService.Where(a => a.ClientId == clientId, "User").FirstOrDefault();
            if (client != null)
            {
                _baseService.Remove(clientId);
                _userService.Delete(client.User.Id);
            }
        }

        public JobEstimatorClient AddClient(JobEstimatorClientModel jobEstimatorClientModel)
        {
            return _baseService.AddOrUpdate(Mapper.MapJobEstimatorClientModelWithEntity(jobEstimatorClientModel), 0);
        }

        public List<JobEstimatorProductStyleModel> GetClientProductsByKey(string key)
        {
            var client = _baseService.Where(a => a.ClientKey == key).FirstOrDefault();
            if (client != null)
            {
                string[] navigationProps = { "ProductType" };
                var productStyles = _productStyleBaseService.Where(a => a.JobEstimatorClientId == client.ClientId, navigationProps).Select(a => Mapper.MapJobEstimatorModelWithProductStyle(a));
                return productStyles.ToList();
            }
            return null;
        }
        public List<JobEstimatorProductTypeModel> GetProductTypes(int categoryId)
        {
            return _productTypeBaseService.Where(a => a.CategoryId == categoryId).Select(x => Mapper.MapJobEstimatorProductTypeWithModel(x)).ToList();
        }
        public List<JobEstimatorProductStyle> GetClientProductsByProductType(int productTypeId, string key)
        {
            var client = _baseService.Where(a => a.ClientKey == key).FirstOrDefault();
            if (client != null)
            {
                string[] navigationProps = { "ProductType", "ProductType.Category" };
                var productStyles = _productStyleBaseService.Where(a => a.ProductTypeId == productTypeId && a.JobEstimatorClientId == client.ClientId);
                return productStyles.ToList();
            }
            return null;
        }
        public void RemoveProductStyle(int id)
        {
            _productStyleBaseService.Remove(id);
        }
        public void UpdateProductStyle(int id, JobEstimatorProductStyleModel jobEstimatorProductStyleModel)
        {
            var productStyle = _productStyleBaseService.GetById(id);
            if (productStyle != null)
            {
                productStyle.GroundCost = jobEstimatorProductStyleModel.GroundCost;
                productStyle.GroundCostExcavate = jobEstimatorProductStyleModel.GroundCostExcavate;
                productStyle.GroundCostFlat = jobEstimatorProductStyleModel.GroundCostFlat;
                productStyle.LabourCost = jobEstimatorProductStyleModel.LabourCost;
                productStyle.MaterialCost = jobEstimatorProductStyleModel.MaterialCost;
                productStyle.ModifiedDate = DateTime.Now;
                productStyle.StyleImage = jobEstimatorProductStyleModel.StyleImage;
                productStyle.StyleName = jobEstimatorProductStyleModel.StyleName;
                productStyle.ProductTypeId = jobEstimatorProductStyleModel.ProductTypeId;
                productStyle.UnitCost = jobEstimatorProductStyleModel.UnitCost;
                productStyle.IsRollsCalculation = jobEstimatorProductStyleModel.IsRollsCalculation;
                productStyle.RollSize = jobEstimatorProductStyleModel.RollSize;
                _productStyleBaseService.AddOrUpdate(productStyle, productStyle.ProductStyleId);
            }
        }
        public int AddProductStyle(JobEstimatorProductStyleModel jobEstimatorProductStyleModel)
        {
            var addedProductStyle = _productStyleBaseService.AddOrUpdate(Mapper.MapJobEstimatorProductStyleWithModel(jobEstimatorProductStyleModel), 0);
            if (addedProductStyle != null)
            {
                return addedProductStyle.ProductStyleId;
            }
            return 0;
        }

        public JobEstimatorClientModel IsClientKeyExists(string key)
        {
            string[] navgationProps = { "Category" };
            return Mapper.MapJobEstimatorClientWithModel(_baseService.Where(a => a.ClientKey == key, "Category").SingleOrDefault());
        }
        public JobEstimatorClient GetClientByKey(string key)
        {
            return _baseService.Where(a => a.ClientKey == key, "User").FirstOrDefault();
        }
        public int UploadBulk(List<JobEstimatorProductStyleModel> jobEstimatorProductStyleModels, int clientId)
        {
            if (jobEstimatorProductStyleModels.Count > 0)
            {
                var productsByClient = _productStyleBaseService.Where(a => a.JobEstimatorClientId == clientId);
                if (productsByClient.Any())
                {
                    _productStyleBaseService.RunRawSql($"delete from JobEstimatorProductStyle where JobEstimatorClient_ID={clientId}");
                }
                IEnumerable<JobEstimatorProductStyle> jobEstimatorProductStyles = jobEstimatorProductStyleModels.Select(a => Mapper.MapJobEstimatorProductStyleWithModel(a)).ToList();
                _productStyleBaseService.InsertBulk(jobEstimatorProductStyles);
                return jobEstimatorProductStyles.Count();
            }
            return 0;
        }
        public int GetTypeIdByName(string type, int categoryID)
        {
            if (!string.IsNullOrEmpty(type))
            {
                var productType = _productTypeBaseService.Where(a => a.Name == type && a.CategoryId == categoryID).SingleOrDefault();
                if (productType != null)
                {
                    return productType.ProductTypeId;
                }
            }
            return 0;
        }
        private void RemoveProductStyleData()
        {
            _productStyleBaseService.TruncateTable("JobEstimatorProductStyle");
        }

        public List<JobEstimatorClientModel> GetClients()
        {
            string[] navgationProps = { "User" };
            return _baseService.GetAll(navgationProps).Select(a => Mapper.MapJobEstimatorClientWithModel(a)).ToList();
        }

        public JobEstimatorClientModel GetClientByUserId(string userId)
        {
            string[] navgationProps = { "User", "Category" };
            return Mapper.MapJobEstimatorClientWithModel(_baseService.Where(a => a.UserId == Guid.Parse(userId), navgationProps).FirstOrDefault());
        }

        public JobEstimatorProductStyle GetProductStyleById(int id)
        {
            return _productStyleBaseService.GetById(id);
        }
        public void UpdateProductStyleImage(int id, string imageLink)
        {
            var productStyle = _productStyleBaseService.GetById(id);
            if (productStyle != null)
            {
                productStyle.StyleImage = imageLink;
                productStyle.ModifiedDate = DateTime.Now;
                _productStyleBaseService.AddOrUpdate(productStyle, productStyle.ProductStyleId);
            }
        }
        public void UpdateClient(int id, JobEstimatorClientModel jobEstimatorClientModel)
        {
            var client = _baseService.GetById(id);
            if (client != null)
            {
                client.Note = jobEstimatorClientModel.Note;
                client.Disclaimer = jobEstimatorClientModel.Disclaimer;
                client.ModifiedDate = DateTime.Now;
                client.Recipients = jobEstimatorClientModel.Recipients;
                _baseService.AddOrUpdate(client, client.ClientId);
            }
        }
    }
}
