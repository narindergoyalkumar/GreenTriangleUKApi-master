using Amazon.mws.service.Models;
using Amazon.mws.service.MWSReportSevices;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using Microsoft.Extensions.Hosting;
using NCrontab;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Amazon.mws.service.Crons
{
    public static class DownloadRequestedReportJob
    {
        public static void DownloadReport()
        {
            string reportId = ReportService.GetMostRecentAddedSubmittedReport();
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
            IdList idList = new IdList();
            idList.Id.Add(reportId);
            // get report request list
            GetReportRequestListRequest getReportRequestList = new GetReportRequestListRequest
            {
                Merchant = SellerId,
                MWSAuthToken = MWSAuthToken,
                ReportRequestIdList = idList,

            };
            GetReportRequestListResponse getReportRequestListResponse = client.GetReportRequestList(getReportRequestList);
            if (getReportRequestListResponse.IsSetGetReportRequestListResult())
            {
                GetReportRequestListResult getReportRequestListResult = getReportRequestListResponse.GetReportRequestListResult;
                List<ReportRequestInfo> reportRequestInfoList = getReportRequestListResult.ReportRequestInfo;
                foreach (ReportRequestInfo reportRequestInfo in reportRequestInfoList)
                {
                    if (reportRequestInfo.ReportType == "_GET_FBA_FULFILLMENT_INVENTORY_HEALTH_DATA_" && reportRequestInfo.ReportProcessingStatus == "_DONE_")
                    {
                        Log.Information("Report ID：" + reportRequestInfo.ReportRequestId + " Result：" + reportRequestInfo.ReportProcessingStatus + System.Environment.NewLine);

                        Log.Information("ReportRequestInfo");
                        ReportService.UpdateReportStatus(reportRequestInfo.ReportRequestId, reportRequestInfo.ReportProcessingStatus, reportRequestInfo.GeneratedReportId);
                        string path = Download_Report(reportRequestInfo.GeneratedReportId, SellerId, System.IO.Directory.GetCurrentDirectory(), client, MWSAuthToken);
                        List<Products> list = new List<Products>();
                        list.AddRange(Utility.Utility.Convert_Open_Listings_As_List(path));
                        if (list.Any())
                        {
                           // ReportService.TruncateProducts();
                            foreach (var item in list)
                            {
                                ReportService.InsertOrUpdateProducts(item);
                            }
                        }
                    }
                }
            }
        }
        private static string Download_Report(string _report_ID, string sellerId, string BasePath, MarketplaceWebServiceClient ReportClient, string mwsToken)
        {
            string path = _report_ID + "_" + Guid.NewGuid();
            if (!Directory.Exists(BasePath + "\\Reports" + "\\"))
            {
                Directory.CreateDirectory(BasePath + "\\Reports" + "\\");
            }
            string thePath = BasePath + "\\Reports" + "\\" + string.Format("{0}.txt", path);
            GetReportRequest get_Report = new GetReportRequest
            {
                Merchant = sellerId,
                ReportId = _report_ID,
                Report = File.Open(thePath, FileMode.OpenOrCreate, FileAccess.ReadWrite),
                MWSAuthToken = mwsToken
            };
            GetReportResponse report_Response = ReportClient.GetReport(get_Report);
            get_Report.Report.Close();
            return thePath;
        }
    }
}
