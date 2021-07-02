using Amazon.mws.service.MWSReportSevices;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using Microsoft.Extensions.Hosting;
using NCrontab;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Amazon.mws.service.Crons
{
    public static class RequestReportJob
    {
        public static void RequestReport()
        {
            string SellerId = "A5MJ4BZH63SVT";
            string MarketplaceId = "A1F83G8C2ARO7P";
            string AccessKeyId = "AKIAJWROP7ZPFA6THKJA";
            string SecretKeyId = "4D8QJMoMOhNiW1ckW4pVqtxodTDGWR5UekiBMrep";
            string ApplicationVersion = "1.0";
            string ApplicationName = "Green Triangle UK";
            string MWSAuthToken = "amzn.mws.02b9d33b-d054-9e4e-65fb-2aba08c5a24d";
            MarketplaceWebServiceConfig config = new MarketplaceWebServiceConfig
            {
                ServiceURL = "https://mws.amazonservices.co.uk"
            };

            MarketplaceWebServiceClient client = new MarketplaceWebServiceClient(
                                                             AccessKeyId,
                                                             SecretKeyId,
                                                             ApplicationName,
                                                             ApplicationVersion,
                                                             config);
            RequestReportRequest request = new RequestReportRequest
            {
                Merchant = SellerId,
                MarketplaceIdList = new IdList()
            };
            request.MarketplaceIdList.Id = new List<string>(new string[] { MarketplaceId });
            request.ReportType = "_GET_FBA_FULFILLMENT_INVENTORY_HEALTH_DATA_";
            request.MWSAuthToken = MWSAuthToken;
            RequestReportResponse response = client.RequestReport(request);
            if (response.IsSetRequestReportResult())
            {
                RequestReportResult requestReportResult = response.RequestReportResult;
                ReportRequestInfo reportRequestInfo = requestReportResult.ReportRequestInfo;
                if (reportRequestInfo.IsSetReportProcessingStatus())
                {
                    string reportId = reportRequestInfo.ReportRequestId;
                    Log.Information($"Report id:{reportId}");
                    ReportService.SaveRequestedReportInfo(reportRequestInfo);
                }
            }
        }
    }
}
