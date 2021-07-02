using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.mws.service.MWSReportSevices;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using MarketplaceWebServiceProducts;
using MarketplaceWebServiceProducts.Model;

namespace Amazon.mws.service.Crons
{
    public static class GetMatchingProductsJob
    {

        public static void GetMatchingProduct()
        {
            string SellerId = "A5MJ4BZH63SVT";
            string MarketplaceId = "A1F83G8C2ARO7P";
            string AccessKeyId = "AKIAJWROP7ZPFA6THKJA";
            string SecretKeyId = "4D8QJMoMOhNiW1ckW4pVqtxodTDGWR5UekiBMrep";
            string ApplicationVersion = "1.0";
            string ApplicationName = "Green Triangle UK";
            string MWSAuthToken = "amzn.mws.02b9d33b-d054-9e4e-65fb-2aba08c5a24d";
            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig
            {
                ServiceURL = "https://mws.amazonservices.co.uk/Products/2011-10-01"
            };


            var asinDbList = ReportService.GetProductAsins();
            if (asinDbList.Any())
            {
                foreach (var asin in asinDbList)
                {
                    MarketplaceWebServiceProductsClient client = new MarketplaceWebServiceProductsClient(
                                                           ApplicationName,
                                                           ApplicationVersion,
                                                           AccessKeyId,
                                                           SecretKeyId,
                                                           config);
                    ASINListType asinList = new ASINListType();
                    asinList.ASIN.Add(asin);
                    GetMatchingProductRequest request = new GetMatchingProductRequest(SellerId, MarketplaceId, asinList)
                    {
                        MWSAuthToken = MWSAuthToken
                    };
                    GetMatchingProductResponse response = client.GetMatchingProduct(request);
                    if (response.IsSetGetMatchingProductResult())
                    {
                        string imageUrl = string.Empty;
                        GetMatchingProductResult getMatchingProductResult = response.GetMatchingProductResult.Where(a => a.ASIN == asin).FirstOrDefault();
                        Product product = getMatchingProductResult.Product;
                        if (product != null)
                        {
                            if (product.AttributeSets != null)
                            {
                                System.Xml.XmlElement elements = (System.Xml.XmlElement)product.AttributeSets.Any[0];
                                foreach (System.Xml.XmlElement element in elements)
                                {
                                    switch (element.LocalName)
                                    {
                                        case "SmallImage":
                                            imageUrl = element.FirstChild.InnerText.Replace("SL75", "SL500");
                                            ReportService.UpdateProductImageByAsin(imageUrl, asin);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
