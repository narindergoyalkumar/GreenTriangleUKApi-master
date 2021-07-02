using Amazon.mws.service.Models;
using MarketplaceWebService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Amazon.mws.service.MWSReportSevices
{
    public static class ReportService
    {
        private static readonly string connectionString = "Server=EC2AMAZ-EDKVP4D;Database=GreenTriangledb-dev;Integrated Security=true";
        //private static readonly string connectionString = "server=.\\SQLEXPRESS;database=GreenTriangledb;user=sa;password=ut-300r2udata;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void SaveRequestedReportInfo(ReportRequestInfo reportRequestInfo)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("insert into [dbo].[MWSReportRequestInfo] (ReportType,ReportProcessingStatus,StartDate,EndDate,IsScheduled,ReportRequestId,SubmittedDate) values(@ReportType,@ReportProcessingStatus,@StartDate,@EndDate,@IsScheduled,@ReportRequestId,@SubmittedDate)", con)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@ReportType", reportRequestInfo.ReportType);
            cmd.Parameters.AddWithValue("@ReportProcessingStatus", reportRequestInfo.ReportProcessingStatus);
            cmd.Parameters.AddWithValue("@StartDate", reportRequestInfo.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", reportRequestInfo.EndDate);
            cmd.Parameters.AddWithValue("@IsScheduled", reportRequestInfo.Scheduled);
            cmd.Parameters.AddWithValue("@ReportRequestId", reportRequestInfo.ReportRequestId);
            cmd.Parameters.AddWithValue("@SubmittedDate", reportRequestInfo.SubmittedDate);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static string GetMostRecentAddedSubmittedReport()
        {
            string ReportRequestId = null;
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("select top 1 * from [dbo].[MWSReportRequestInfo] where ReportProcessingStatus='_SUBMITTED_' order by ReportRequestId desc", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ReportRequestId = Convert.ToString(reader["ReportRequestId"]);
                }
            }
            reader.Close();
            cmd.Dispose();
            con.Close();
            return ReportRequestId;
        }

        public static void UpdateReportStatus(string reportId, string status, string generatedReportId)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("update [dbo].[MWSReportRequestInfo] set GeneratedReportId=@GeneratedReportId , ReportProcessingStatus=@ReportProcessingStatus , CompletedDate=@CompletedDate where ReportRequestId=@reportId", con)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@GeneratedReportId", generatedReportId);
            cmd.Parameters.AddWithValue("@ReportProcessingStatus", status);
            cmd.Parameters.AddWithValue("@reportId", reportId);
            cmd.Parameters.AddWithValue("@CompletedDate", DateTime.UtcNow);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void InsertOrUpdateProducts(Products products)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmdSelect = new SqlCommand("select * from [dbo].[AmazonProducts] where Asin=@Asin", con)
            {
                CommandType = CommandType.Text
            };
            cmdSelect.Parameters.AddWithValue("@Asin", products.Asin);
            con.Open();
            var result = cmdSelect.ExecuteScalar();
            int i = Convert.ToInt32(result);
            if (i > 0)
            {
                using SqlCommand cmd = new SqlCommand("update [dbo].[AmazonProducts] set Name=@Name,SellableQuantity=@SellableQuantity,InvAge0To90Days=@InvAge0To90Days,InvAge91to181Days=@InvAge91to181Days,InvAge181To270Days=@InvAge181To270Days,InvAge271To365Days=@InvAge271To365Days,InvAge365PlusDays=@InvAge365PlusDays,WeeksOfCoverT7=@WeeksOfCoverT7,WeeksOfCoverT30=@WeeksOfCoverT30,WeeksOfCoverT90=@WeeksOfCoverT90,WeeksOfCoverT180=@WeeksOfCoverT180,WeeksOfCoverT365=@WeeksOfCoverT365,ModifiedDate=@ModifiedDate,UnitsShippedLast24Hrs=@UnitsShippedLast24Hrs,UnitsShippedLast7Days=@UnitsShippedLast7Days,UnitsShippedLast30Days=@UnitsShippedLast30Days,UnitsShippedLast90Days=@UnitsShippedLast90Days,UnitsShippedLast180Days=@UnitsShippedLast180Days,UnitsShippedLast365Days=@UnitsShippedLast365Days where Asin=@Asin", con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Name", products.ProductName);
                cmd.Parameters.AddWithValue("@Asin", products.Asin);
                cmd.Parameters.AddWithValue("@SellableQuantity", products.SellableQuanity);
                cmd.Parameters.AddWithValue("@InvAge0To90Days", products.InvAge0To90Days);
                cmd.Parameters.AddWithValue("@InvAge91to181Days", products.InvAge91to181Days);
                cmd.Parameters.AddWithValue("@InvAge181To270Days", products.InvAge181To270Days);
                cmd.Parameters.AddWithValue("@InvAge271To365Days", products.InvAge271To365Days);
                cmd.Parameters.AddWithValue("@InvAge365PlusDays", products.InvAge365PlusDays);
                cmd.Parameters.AddWithValue("@UnitsShippedLast24Hrs", products.UnitsShippedLast24Hrs);
                cmd.Parameters.AddWithValue("@UnitsShippedLast7Days", products.UnitsShippedLast7Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast30Days", products.UnitsShippedLast30Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast90Days", products.UnitsShippedLast90Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast180Days", products.UnitsShippedLast180Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast365Days", products.UnitsShippedLast365Days);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT7", products.WeeksOfCoverT7);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT30", products.WeeksOfCoverT30);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT90", products.WeeksOfCoverT90);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT180", products.WeeksOfCoverT180);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT365", products.WeeksOfCoverT365);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
               
                cmd.ExecuteNonQuery();
                //con.Close();
            }
            else
            {
                using SqlCommand cmd = new SqlCommand("insert into [dbo].[AmazonProducts] (Name,Asin,SellableQuantity,InvAge0To90Days,InvAge91to181Days,InvAge181To270Days,InvAge271To365Days,InvAge365PlusDays,UnitsShippedLast24Hrs,UnitsShippedLast7Days,UnitsShippedLast30Days,UnitsShippedLast90Days,UnitsShippedLast180Days,UnitsShippedLast365Days,WeeksOfCoverT7,WeeksOfCoverT30,WeeksOfCoverT90,WeeksOfCoverT180,WeeksOfCoverT365,CreatedDate) values(@Name,@Asin,@SellableQuantity,@InvAge0To90Days,@InvAge91to181Days,@InvAge181To270Days,@InvAge271To365Days,@InvAge365PlusDays,@UnitsShippedLast24Hrs,@UnitsShippedLast7Days,@UnitsShippedLast30Days,@UnitsShippedLast90Days,@UnitsShippedLast180Days,@UnitsShippedLast365Days,@WeeksOfCoverT7,@WeeksOfCoverT30,@WeeksOfCoverT90,@WeeksOfCoverT180,@WeeksOfCoverT365,@CreatedDate)", con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Name", products.ProductName);
                cmd.Parameters.AddWithValue("@Asin", products.Asin);
                cmd.Parameters.AddWithValue("@SellableQuantity", products.SellableQuanity);
                cmd.Parameters.AddWithValue("@InvAge0To90Days", products.InvAge0To90Days);
                cmd.Parameters.AddWithValue("@InvAge91to181Days", products.InvAge91to181Days);
                cmd.Parameters.AddWithValue("@InvAge181To270Days", products.InvAge181To270Days);
                cmd.Parameters.AddWithValue("@InvAge271To365Days", products.InvAge271To365Days);
                cmd.Parameters.AddWithValue("@InvAge365PlusDays", products.InvAge365PlusDays);
                cmd.Parameters.AddWithValue("@UnitsShippedLast24Hrs", products.UnitsShippedLast24Hrs);
                cmd.Parameters.AddWithValue("@UnitsShippedLast7Days", products.UnitsShippedLast7Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast30Days", products.UnitsShippedLast30Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast90Days", products.UnitsShippedLast90Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast180Days", products.UnitsShippedLast180Days);
                cmd.Parameters.AddWithValue("@UnitsShippedLast365Days", products.UnitsShippedLast365Days);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT7", products.WeeksOfCoverT7);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT30", products.WeeksOfCoverT30);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT90", products.WeeksOfCoverT90);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT180", products.WeeksOfCoverT180);
                cmd.Parameters.AddWithValue("@WeeksOfCoverT365", products.WeeksOfCoverT365);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                //con.Open();
                cmd.ExecuteNonQuery();
               // con.Close();
            }
            con.Close();

        }
        public static void TruncateProducts()
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("truncate table [dbo].[AmazonProducts];", con)
            {
                CommandType = CommandType.Text
            };
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static List<string> GetProductAsins()
        {
            List<string> asinList = new List<string>();
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("select Asin from [dbo].[AmazonProducts]", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    asinList.Add(Convert.ToString(reader["Asin"]));
                }
            }
            reader.Close();
            cmd.Dispose();
            con.Close();
            return asinList;
        }
        public static void UpdateProductImageByAsin(string imageUrl,string asin)
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("update [dbo].[AmazonProducts] set OriginalImage=@image where asin=@asin", con)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.AddWithValue("@image", imageUrl);
            cmd.Parameters.AddWithValue("@asin", asin);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
