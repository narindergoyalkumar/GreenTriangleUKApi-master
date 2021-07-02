using ApiSkeletonPoc.Core.Insfrastucture;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.DAL.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiSkeletonPoc.Core.Services
{
    public class AmazonMwsService : IAmazonMwsService
    {
        private readonly IBaseService<AmazonProducts> _baseService;
        private readonly IBaseService<User> _userBaseService;
        private readonly IBaseService<AmazonMwsProductsReOrderSettings> _productsReOrderingConfigBaseService;
        public AmazonMwsService(IBaseService<AmazonProducts> baseService, IBaseService<AmazonMwsProductsReOrderSettings> productsReOrderingConfigBaseService, IBaseService<User> userBaseService)
        {
            _baseService = baseService;
            _productsReOrderingConfigBaseService = productsReOrderingConfigBaseService;
            _userBaseService = userBaseService;
        }

        public bool AssignShortName(int id, string shortName)
        {
            bool isNameAssigned = false;
            var product = _baseService.GetById(id);
            if (product != null)
            {
                product.ShortName = shortName;
                product.ModifiedDate = DateTime.Now;
                var updatedProduct = _baseService.AddOrUpdate(product, id);
                if (updatedProduct != null)
                {
                    isNameAssigned = true;
                }
            }
            return isNameAssigned;
        }

        public MwsProductsModel Get(int id)
        {
            return Mapper.MapAmazonProductsWithMWSProductsModel(_baseService.GetById(id));
        }

        public IEnumerable<MwsProductsModel> GetProducts()
        {
            return _baseService.GetAll().Select(a => Mapper.MapAmazonProductsWithMWSProductsModel(a)).ToList();
        }

        public bool ResetProductImage(int productId)
        {
            bool isProductReset = false;
            var product = _baseService.GetById(productId);
            if (product != null)
            {
                product.CustomizedImage = null;
                product.ModifiedDate = DateTime.Now;
                var updatedProduct = _baseService.AddOrUpdate(product, productId);
                if (updatedProduct != null)
                {
                    isProductReset = true;
                }
            }
            return isProductReset;
        }

        public bool ResetProductShortName(int productId)
        {
            bool isProductReset = false;
            var product = _baseService.GetById(productId);
            if (product != null)
            {
                product.ShortName = null;
                product.ModifiedDate = DateTime.Now;
                var updatedProduct = _baseService.AddOrUpdate(product, productId);
                if (updatedProduct != null)
                {
                    isProductReset = true;
                }
            }
            return isProductReset;
        }

        public bool SetLeadTime(int productId, int days)
        {
            bool isLeadTimeSet = false;
            var product = _baseService.GetById(productId);
            if (product != null)
            {
                product.LeadTimeDays = days;
                product.ModifiedDate = DateTime.Now;
                var updatedProduct = _baseService.AddOrUpdate(product, productId);
                if (updatedProduct != null)
                {
                    isLeadTimeSet = true;
                }
            }
            return isLeadTimeSet;
        }

        public bool UpdateImageForProduct(int id, string imageLink)
        {
            bool isImageUpdated = false;
            var product = _baseService.GetById(id);
            if (product != null)
            {
                product.CustomizedImage = imageLink;
                product.ModifiedDate = DateTime.Now;
                var updatedProduct = _baseService.AddOrUpdate(product, id);
                if (updatedProduct != null)
                {
                    isImageUpdated = true;
                }
            }
            return isImageUpdated;
        }

        public bool UpdateInTransitProductsCount(int id, int inTransitCount)
        {
            bool isTransitCountUpdated = false;
            var product = _baseService.GetById(id);
            if (product != null)
            {
                product.InTransit = inTransitCount;
                product.ModifiedDate = DateTime.Now;
                var updatedProduct = _baseService.AddOrUpdate(product, id);
                if (updatedProduct != null)
                {
                    isTransitCountUpdated = true;
                }
            }
            return isTransitCountUpdated;
        }
        public bool SetReOrderDaysConfiguration(List<AmazonMwsProductsReOrderSettingsModel> configurationList, Guid userId)
        {
            bool isConfigurationSet = false;
            if (configurationList.Any())
            {
                var existingSettings = _productsReOrderingConfigBaseService.Where(a => a.UserId == userId).ToList();
                if(existingSettings.Count>0)
                {
                    foreach (var setting in existingSettings)
                    {
                        _productsReOrderingConfigBaseService.Remove(setting.ReOrderConfigId);
                    }
                }
                configurationList.ForEach(a => a.UserId = userId);
                IEnumerable<AmazonMwsProductsReOrderSettings> amazonMwsProductsReOrderSettings = configurationList.Select(a => Mapper.MapAmazonReOrderSettingsModelWithEntity(a)).ToList();
                isConfigurationSet = _productsReOrderingConfigBaseService.InsertBulk(amazonMwsProductsReOrderSettings);
            }
            return isConfigurationSet;
        }

        public User GetMwsUserWithSettings(Guid id)
        {
            string[] navgationProps = { "AmazonMwsProductsReOrderSettings" };
            var user = _userBaseService.Where(a => a.Id == id, navgationProps).FirstOrDefault();
            if (user == null)
                return null;
            return user;
        }
    }
}
